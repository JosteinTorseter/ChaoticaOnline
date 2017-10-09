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
            Session["GameID"] = game.ID;

            return View("Index", model);
        }

        private Player GetPlayer()
        {
            int iPlayerID = (int)Session["PlayerID"];
            Player player = dbG.Players.Find(iPlayerID);
            player.ArrangeStringToTileLists();
            player.ArrangeStringToVisObjects();
            return player;
        }

        private Game GetGame()
        {
            int iGameID = (int)Session["GameID"];
            Game game = dbG.Games.Find(iGameID);
            return game;
        }

        public ActionResult GetTilePanelInfo(int id)
        {
            Tile tile = null;
            Player player = null;
            if (id > 0)
            {
                tile = dbG.Tiles.Find(id); 
                player = GetPlayer();
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

        public ActionResult RefreshTile(int id)
        {
            if (id == 0) { return new EmptyResult(); }
            Player p = GetPlayer();
            Tile tile = dbG.Tiles.Find(id);
            return PartialView("Subs/_Tile", new TileViewModel(tile, TileSelectionType.None, GetSessionDic(), p));
        }

        public ActionResult GetDwellingPanelInfo(int id)
        {
            if (id > 0)
            {
                Dwelling d = dbG.Dwellings.Find(id);
                Player p = GetPlayer();
                return PartialView("Panels/_DwellingPanel", new DwellingViewModel(d, p));
            }
            else
            {
                return new EmptyResult();
            }
        }

        public ActionResult GetDungeonPanelInfo(int id)
        {
            if (id > 0)
            {
                Dungeon d = dbG.Dungeons.Find(id);
                Player p = GetPlayer();
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
                Player p = GetPlayer();
                Unit u = dbG.Units.Find(id);
                return PartialView("Panels/_UnitPanel", new UnitViewModel(u, false, false, 0, p.Color));
            }
            else
            {
                //Player p = dbG.Players.Find(iPlayerID);
                return new EmptyResult();
            }
        }

        public ActionResult GetItemPanelInfo(int id)
        {
            if (id > 0)
            {
                Player p = GetPlayer();
                WorldItem it = dbG.WorldItems.Find(id);
                TDBWorldItem bit = dbT.TDBWorldItems.Find(it.BaseItemID);
                int iPrice = bit.GoldValue;
                double dbl = (100 + p.GetAttributeValue(BonusType.SalePrice)) / 100;
                dbl = dbl * Statics.BaseSellPercent;
                iPrice = Calc.Round(iPrice * dbl, -1);
                return PartialView("Panels/_WorldItemPanel", new WorldItemViewModel(p, bit, 
                    it.Count, false, false, bit.GetPrice(false, p.GetAttributeValue(BonusType.SalePrice)), 
                    it.ID));
            }
            else
            {
                //Player p = dbG.Players.Find(iPlayerID);
                return new EmptyResult();
            }
        }

        public ActionResult GetBaseItemPanelInfo(int id)
        {
            if (id > 0)
            {
                Player p = GetPlayer();
                TDBWorldItem bit = dbT.TDBWorldItems.Find(id);
                return PartialView("Panels/_WorldItemPanel", new WorldItemViewModel(p, bit, 1, true, true, bit.GoldValue, 0));
            }
            else
            {
                //Player p = dbG.Players.Find(iPlayerID);
                return new EmptyResult();
            }
        }

        public ActionResult GetBaseUnitPanelInfo(int id)
        {
            if (id > 0)
            {
                Player p = GetPlayer();
                return PartialView("Panels/_UnitPanel", new UnitViewModel(UnitFactory.CreateUnit(dbT, id, 1), true, true, 0, p.Color));
            }
            else
            {
                //Player p = dbG.Players.Find(iPlayerID);
                return new EmptyResult();
            }
        }

        public ActionResult GetBaseSpecPanelInfo(int id)
        {
            if (id > 0)
            {
                Player p = GetPlayer();
                TDBSpecial spec = dbT.TDBSpecials.Find(id);
                return PartialView("Panels/_SpecPanel", new SpecialViewModel(spec, true, true, spec.GoldValue, p.Color));
            }
            else
            {
                //Player p = dbG.Players.Find(iPlayerID);
                return new EmptyResult();
            }
        }

        public ActionResult GetSpecPanelInfo(int id)
        {
            if (id > 0)
            {
                Player p = GetPlayer();
                TDBSpecial spec = dbT.TDBSpecials.Find(id);
                return PartialView("Panels/_SpecPanel", new SpecialViewModel(spec, false, false, 0, p.Color));
            }
            else
            {
                //Player p = dbG.Players.Find(iPlayerID);
                return new EmptyResult();
            }
        }

        public ActionResult TryMoveToTile(int id)
        {
            Player p = GetPlayer();
            p.ArrangeStringToTileLists();
            if (!p.MovableTiles.Contains(id)) { return new EmptyResult(); }

            Map map = GameActions.TryMoveToTile(p, id, dbG);
            if (map == null) { return new EmptyResult(); }

            return PartialView("_Map", new MapViewModel(map, p, GetSessionDic(), id));
        }

        public ActionResult RearrangeUnits(string sf, int fid, string st, int tid)
        {
            int iFromArmy = 0;
            int iToArmy = 0;
            if (sf == "roster") { iFromArmy = 1; }
            if (st == "roster") { iToArmy = 1; }
            if (iFromArmy + iToArmy == 2) { return new EmptyResult(); }
            if (iFromArmy == 0 && fid == 0) { return new EmptyResult(); }
            if (iToArmy == 0 && tid == 0) { return new EmptyResult(); }

            Player p = GetPlayer();
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
                while (UnitFactory.ActualUnitCount(lstParty) > 6)
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
            
            UnitFactory.RearrangeUnits(lstParty);

            party.Units = lstParty;
            dbG.Entry(party).State = EntityState.Modified;
            if (iFromArmy + iToArmy > 0)
            {
                UnitFactory.RearrangeUnits(lstRoster);
                roster.Units = lstRoster;
                dbG.Entry(roster).State = EntityState.Modified;
            }
            dbG.SaveChanges();

            List<SmallUnitViewModel> res = new List<SmallUnitViewModel>();
            foreach (Unit u in lstParty) { res.Add(new SmallUnitViewModel(u)); }

            ViewBag.HeroImage = p.HeroImage;
            return PartialView("Subs/_PartyWindow", res);
        }

        public ActionResult AddUnitToParty(int id)
        {
            Player p = GetPlayer();
            Party party = dbG.Parties.Find(p.PartyID);
            Party roster = dbG.Parties.Find(p.RosterID);

            List<Unit> lstParty = party.OrderedUnits();
            if (UnitFactory.ActualUnitCount(lstParty) >= 6) { return new EmptyResult(); }

            List<Unit> lstRoster = roster.OrderedUnits();
            Unit moveUnit = lstRoster[id];
            if (moveUnit.Takes2Slots && UnitFactory.ActualUnitCount(lstParty) >= 5) { return new EmptyResult(); }

            lstRoster.Remove(moveUnit);
            lstParty.Add(moveUnit);

            UnitFactory.RearrangeUnits(lstParty);
            UnitFactory.RearrangeUnits(lstRoster);
            party.Units = lstParty;
            roster.Units = lstRoster;
            dbG.Entry(party).State = EntityState.Modified;
            dbG.Entry(roster).State = EntityState.Modified;
            dbG.SaveChanges();

            List<SmallUnitViewModel> res = new List<SmallUnitViewModel>();
            foreach (Unit u in lstParty) { res.Add(new SmallUnitViewModel(u)); }

            ViewBag.HeroImage = p.HeroImage;
            return PartialView("Subs/_PartyWindow", res);
        }

        public ActionResult RemoveUnitFromParty(int id)
        {
            Player p = GetPlayer();
            Party party = dbG.Parties.Find(p.PartyID);
            Party roster = dbG.Parties.Find(p.RosterID);

            List<Unit> lstParty = party.OrderedUnits();
            List<Unit> lstRoster = roster.OrderedUnits();
            Unit moveUnit = lstParty[id];

            lstParty.Remove(moveUnit);
            lstRoster.Add(moveUnit);

            UnitFactory.RearrangeUnits(lstParty);
            UnitFactory.RearrangeUnits(lstRoster);
            party.Units = lstParty;
            roster.Units = lstRoster;
            dbG.Entry(party).State = EntityState.Modified;
            dbG.Entry(roster).State = EntityState.Modified;
            dbG.SaveChanges();

            List<SmallUnitViewModel> res = new List<SmallUnitViewModel>();
            foreach (Unit u in lstParty) { res.Add(new SmallUnitViewModel(u)); }

            ViewBag.HeroImage = p.HeroImage;
            return PartialView("Subs/_PartyWindow", res);
        }

        public ActionResult ClickedAction(int id, int act, int obj)
        {
            Player p = GetPlayer();
            ButtonAction action = (ButtonAction)act;
            EntityType entity = (EntityType)obj;
            Object res = GameActions.ApplyAction(p, action, entity, id, dbG, dbT);
            if (res == null) { return new EmptyResult(); }

            switch (action)
            {
                case ButtonAction.Enter:
                    {
                        if (entity == EntityType.Dwelling) { return PartialView("Subs/_InsideDwellingWindow", (InsideDwellingViewModel)res); }
                        break;
                    }
                case ButtonAction.Explore:
                    {
                        //MulipleViewReturnViewModel model = new MulipleViewReturnViewModel();
                        //model.CardResult = (DrawResultViewModel)res;
                        //model.Tile = new TileViewModel(dbG.Tiles.Find(p.TileID), TileSelectionType.None, GetSessionDic(), p);
                        //if (model.CardResult.Dwelling != null) { model.Dwelling = model.CardResult.Dwelling; }
                        //if (model.CardResult.Dungeon != null) { model.Dungeon = model.CardResult.Dungeon; }
                        //return PartialView("_MultipleViewReturn", model);
                        return PartialView("PopUps/_DrawResultPopUp", (DrawResultViewModel)res);
                    }
                case ButtonAction.Close:
                    {
                        MulipleViewReturnViewModel model = new MulipleViewReturnViewModel();
                        model.Map = new MapViewModel(GetGame().Map, p, GetSessionDic(), p.TileID);
                        return PartialView("_MultipleViewReturn", model);
                    }
            }

            return new EmptyResult();

        }

        public ActionResult GetRoster()
        {
            Player p = GetPlayer();
            Party party = dbG.Parties.Find(p.RosterID);
            List<SmallUnitViewModel> lstRes = new List<SmallUnitViewModel>();
            foreach (Unit u in party.OrderedUnits())
            {
                lstRes.Add(new SmallUnitViewModel(u));
            }
            return PartialView("Panels/_RosterPanel", lstRes);
        }
        public ActionResult GetInventory()
        {
            Player p = GetPlayer();
            List<SmallWorldItemViewModel> lstRes = p.GetInventoryItems(dbT);
            return PartialView("Panels/_InventoryPanel", new InventoryViewModel(lstRes, 
                Statics.AlignmentColor(p.Alignment)));
        }

        public ActionResult GetCharSheet()
        {
            Player p = GetPlayer();
            List<List<SmallWorldItemViewModel>> lstRes = p.GetHeroItems(dbT);
            return PartialView("Panels/_CharSheetPanel", new CharSheetViewModel(p, lstRes[0], lstRes[1]));
        }
        public ActionResult RefreshInfoPanel()
        {
            Player p = GetPlayer();
            return PartialView("Panels/_InfoPanel", new PlayerViewModel(p));
        }

        public ActionResult GetCharSheet2()
        {
            Player p = GetPlayer();
            List<List<SmallWorldItemViewModel>> lstRes = p.GetHeroItems(dbT);
            MulipleViewReturnViewModel model = new MulipleViewReturnViewModel();
            model.CharSheet = new CharSheetViewModel(p, lstRes[0], lstRes[1]);
            model.Unit = new UnitViewModel(p.GetHeroUnit(), false, false, 0, p.Color);
            return PartialView("_MultipleViewReturn", model);
        }


        public ActionResult GetHeroUnit()
        {
            Player p = GetPlayer();
            return PartialView("Panels/_UnitPanel", new UnitViewModel(p.GetHeroUnit(), false, false, 0, p.Color));
        }

        public ActionResult TakeOff(int id)
        {
            Player p = GetPlayer();
            WorldItem it = p.WorldItems.Where(wit => wit.ID == id).First();
            it.Wearing = false;
            dbG.Entry(it).State = EntityState.Modified;
            dbG.SaveChanges();
            return GetCharSheet();
        }

        public ActionResult PutOn(int id)
        {
            Player p = GetPlayer();
            WorldItem it = p.WorldItems.Where(wit => wit.ID == id).First();

            if (p.AlreadyWearingThis(it)) { return new EmptyResult(); }

            TDBWorldItem bit = dbT.TDBWorldItems.Find(it.BaseItemID);
            if (!p.CanWearItem(bit)) { return new EmptyResult(); }

            List<WorldItem> lstItems = null;
            WorldItem wornItem = null;

            if (it.TypeName == "Scroll" || it.TypeName == "Potion")
            {
                lstItems = p.WornItemsByType(it.TypeName);
                if (lstItems.Count > 1)
                {
                    lstItems[1].Wearing = false;
                    dbG.Entry(lstItems[1]).State = EntityState.Modified;
                }

            } else if (it.TypeName == "Accessory")
            {
                lstItems = p.WornItemsByType(it.TypeName);
                if (lstItems.Count > 3)
                {
                    lstItems[3].Wearing = false;
                    dbG.Entry(lstItems[3]).State = EntityState.Modified;
                }
            }
            else if (it.TypeName == "Offhand")
            {
                wornItem = p.WornItemByType("Weapon");
                if (!(wornItem == null))
                {
                    if (wornItem.IsTwoHanded())
                    {
                        wornItem.Wearing = false;
                        dbG.Entry(wornItem).State = EntityState.Modified;
                    }
                }
            } else
            {
                wornItem = p.WornItemByType(it.TypeName);
                if (!(wornItem == null))
                {
                    wornItem.Wearing = false;
                    dbG.Entry(wornItem).State = EntityState.Modified;
                }
                if (it.IsTwoHanded())
                {
                    wornItem = p.WornItemByType("Offhand");
                    if (!(wornItem == null))
                    {
                        wornItem.Wearing = false;
                        dbG.Entry(wornItem).State = EntityState.Modified;
                    }
                }
            }
            it.Wearing = true;
            dbG.Entry(it).State = EntityState.Modified;
            dbG.SaveChanges();
            return GetCharSheet();
        }

        public ActionResult PopDown()
        {
            Success(string.Format("Yippi Ka Ye Motherfucker!", "Argument2", "Argument3"), true);
            return PartialView("_Alerts");
        }
    }
}