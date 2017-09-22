using ChaoticaOnline.DAL;
using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.GameModels;
using ChaoticaOnline.lib;
using ChaoticaOnline.TemplateModels;
using ChaoticaOnline.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChaoticaOnline.Controllers
{
    public class MainController : BaseController
    {
        private TemplateContext dbT = new TemplateContext();
        private GameContext dbG = new GameContext();

        public ActionResult NewGame()
        {
            Calc calc = new Calc();
            Game game = GameGenerator.GenerateGame(calc, dbT, dbG);
            return DisplayGame(game);
        }

        public ActionResult FindGame(int id)
        {
            Game game = dbG.Games.Find(id);
            return DisplayGame(game);
        }

        private Dictionary<int, string> SetSessionDic(Game game, bool booSet = true)
        {
            string res = "";
            Dictionary<int, string> dic = new Dictionary<int, string>();
            foreach (Player pl in game.Players)
            {
                if (booSet)
                {
                    if (res != "") { res += "#"; }
                    res += pl.ID.ToString() + ":" + pl.Color;
                }
                dic.Add(pl.ID, pl.Color);
            }
            if (booSet) { Session["PlayerColorsDic"] = res; }
            return dic;
        }
        private Dictionary<int, string> GetSessionDic()
        {
            string res = Session["PlayerColorsDic"].ToString();
            Dictionary<int, string> dic = new Dictionary<int, string>();
            if (res == "") { return dic; }
            foreach (string sFull in res.Split('#'))
            {
                string[] s = sFull.Split(':');
                dic.Add(Int32.Parse(s[0]), s[1]);
            }
            return dic;
        }

        private ActionResult DisplayGame(Game game)
        {
            Player player = game.Players.ElementAt(0);
            Dictionary<int, string> dicPlayerColors = SetSessionDic(game);
            GameViewModel model = new GameViewModel(game.ID, game.Map, player,
                                        game.Map.Tiles.Where(t => t.ID == player.TileID).First(),
                                        game.Parties.Where(p => p.ID == player.PartyID).First(),
                                        dicPlayerColors);

            Session["PlayerID"] = player.ID;
            Session["PlayerColor"] = player.Color;

            return View("Index", model);
        }


        public ActionResult GetTilePanelInfo(int id)
        {
            Tile tile = null;
            int iPlayerID = (int)Session["PlayerID"];
            Player player = null;
            if (id > 0)
            {
                tile = dbG.Tiles.Find(id); 
                player = dbG.Players.Find(iPlayerID);
            }
            else
            {
                tile = new Tile();
                tile.Image = "tile.jpg";
                tile.BGColor = "grey";
                tile.Name = "Unknown terretory";
                tile.Description = "Untold dangers lie beyond...";
            }
            return PartialView("Panels/_TilePanel", new TileViewModel(tile, TileSelectionType.None, GetSessionDic(), player));
        }

        public ActionResult GetDwellingPanelInfo(int id)
        {
            int iPlayerID = (int)Session["PlayerID"];
            if (id > 0)
            {
                Dwelling d = dbG.Dwellings.Find(id);
                Player p = dbG.Players.Find(iPlayerID);
                return PartialView("Panels/_DwellingPanel", new DwellingViewModel(d, p));
            }
            else
            {
                return new EmptyResult();
            }
        }

        public ActionResult GetDungeonPanelInfo(int id)
        {
            int iPlayerID = (int)Session["PlayerID"];
            if (id > 0)
            {
                Dungeon d = dbG.Dungeons.Find(id);
                Player p = dbG.Players.Find(iPlayerID);
                return PartialView("Panels/_DungeonPanel", new DungeonViewModel(d, p));
            }
            else
            {
                return new EmptyResult();
            }
        }

        public ActionResult GetUnitPanelInfo(int id)
        {
            if (id > 0)
            {
                Unit u = dbG.Units.Find(id);
                return PartialView("Panels/_UnitPanel", new UnitViewModel(u));
            }
            else
            {
                return new EmptyResult();
            }
        }

        public ActionResult TryMoveToTile(int id)
        {
            int iPlayerID = (int)Session["PlayerID"];

            Player p = dbG.Players.Find(iPlayerID);
            p.SetTileListsString();
            if (!p.MovableTiles.Contains(id)) { return new EmptyResult(); }

            Map map = GameActions.TryMoveToTile(p, id, dbG);
            if (map == null) { return new EmptyResult(); }

            return PartialView("_Map", new MapViewModel(map, GetSessionDic()));
        }

        public ActionResult RearrangeUnits(string sf, int fid, string st, int tid)
        {
            int iPlayerID = (int)Session["PlayerID"];
            int iFromArmy = 0;
            int iToArmy = 0;
            if (sf == "roster") { iFromArmy = 1; }
            if (st == "roster") { iToArmy = 1; }
            if (iFromArmy + iToArmy == 2) { return new EmptyResult(); }
            if (iFromArmy == 0 && fid == 0) { return new EmptyResult(); }
            if (iToArmy == 0 && tid == 0) { return new EmptyResult(); }

            Player p = dbG.Players.Find(iPlayerID);
            Party party = null;
            Party roster = null;
            party = dbG.Parties.Find(p.PartyID);
            List<Unit> lstParty = party.OrderedUnits();
            List<Unit> lstRoster = null;
            if (iFromArmy + iToArmy > 0)
            {
                roster = dbG.Parties.Find(p.RosterID);
                lstRoster = roster.OrderedUnits();
            }

            Unit moveUnit = null;
            if (iFromArmy == 0)
            {
                moveUnit = lstParty[fid];
                lstParty.Remove(moveUnit);
            }
            else
            {
                moveUnit = lstRoster[fid];
                lstRoster.Remove(moveUnit);
            }

            if (iToArmy == 0)
            {
                lstParty.Insert(tid, moveUnit);
                while (UnitFactory.ActualUnitCount(lstParty) > 7)
                {
                    moveUnit = lstParty[lstParty.Count - 1];
                    lstParty.RemoveAt(lstParty.Count - 1);
                    lstRoster.Add(moveUnit);
                }
            }
            else
            {
                lstRoster.Add(moveUnit);
            }

            int iIndex = 0;
            foreach (Unit u in lstParty)
            {
                u.PartyListIndex = iIndex;
                iIndex += 1;
            }

            party.Units = lstParty;
            dbG.Entry(party).State = EntityState.Modified;
            if (iFromArmy + iToArmy > 0)
            {
                iIndex = 0;
                foreach (Unit u in lstRoster)
                {
                    u.PartyListIndex = iIndex;
                    iIndex += 1;
                }
                roster.Units = lstRoster;
                dbG.Entry(roster).State = EntityState.Modified;
            }
            dbG.SaveChanges();

            List<UnitViewModel> res = new List<UnitViewModel>();
            foreach (Unit u in lstParty) { res.Add(new UnitViewModel(u)); }

            return PartialView("Subs/_PartyWindow", res);
        }

        public ActionResult ClickedAction(int id, int id2)
        {
            int iPlayerID = (int)Session["PlayerID"];
            
            return new EmptyResult();
        }

        public ActionResult GetRoster()
        {
            int iPlayerID = (int)Session["PlayerID"];
            Player p = dbG.Players.Find(iPlayerID);
            Party party = dbG.Parties.Find(p.RosterID);
            List<UnitViewModel> lstRes = new List<UnitViewModel>();
            foreach (Unit u in party.OrderedUnits())
            {
                lstRes.Add(new UnitViewModel(u));
            }
            return PartialView("Panels/_RosterPanel", lstRes);
        }

        public ActionResult PopDown()
        {
            Success(string.Format("Yippi Ka Ye Motherfucker!", "Argument2", "Argument3"), true);
            return PartialView("_Alerts");
        }
    }
}