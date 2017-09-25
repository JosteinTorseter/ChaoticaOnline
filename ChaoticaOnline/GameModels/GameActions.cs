using ChaoticaOnline.DAL;
using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.lib;
using ChaoticaOnline.TemplateModels;
using ChaoticaOnline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChaoticaOnline.GameModels
{
    public class GameActions
    {
        public static Map TryMoveToTile(Player p, int tileID, GameContext dbG)
        {
            if (!p.VisibleTiles.Contains(tileID)) { return null; }
            Tile tile = dbG.Tiles.Find(tileID);
            if (!(tile.IsNeighbour(p.X, p.Y))) { return null; }
            Map map = dbG.Maps.Find(tile.MapId);
            Tile t = TileList.GetTile(map.Tiles.ToList(), tile.ID);
            SetVisibleTiles(map, p, t);

            Tile origTile = TileList.GetTile(map.Tiles.ToList(), p.TileID);
            p.TileID = tile.ID;
            p.X = tile.XCoord;
            p.Y = tile.YCoord;
            t.Players.Add(p.ID);
            origTile.Players.Remove(p.ID);

            dbG.Entry(p).State = System.Data.Entity.EntityState.Modified;
            dbG.Entry(t).State = System.Data.Entity.EntityState.Modified;
            dbG.Entry(origTile).State = System.Data.Entity.EntityState.Modified;
            dbG.SaveChanges();

            return map;
        }
        
        private static void SetVisibleTiles(Map m, Player p, Tile t)
        {
            List<int> lstVisible = new List<int>();
            List<int> lstFogged = new List<int>();
            List<int> lstVisited = new List<int>();
            List<int> lstMovable = new List<int>();
            foreach (int i in p.VisibleTiles)
            {
                lstVisible.Add(i);
            }
            foreach (int i in p.VisitedTiles)
            {
                lstVisited.Add(i);
            }
            foreach (int i in p.FoggedTiles)
            {
                lstFogged.Add(i);
            }
            if (!lstVisited.Contains(t.ID)) { lstVisited.Add(t.ID); }
            foreach (Tile tl in TileList.GetSurroundingTiles(m.Tiles.ToList(), t.XCoord, t.YCoord))
            {
                lstMovable.Add(tl.ID);
                if (!lstVisible.Contains(tl.ID))
                {
                    lstVisible.Add(tl.ID);
                    if (lstFogged.Contains(tl.ID)) { lstFogged.Remove(tl.ID); }
                }
                foreach (Tile tl2 in TileList.GetSurroundingTiles(m.Tiles.ToList(), tl.XCoord, tl.YCoord))
                {
                    if ((tl2 != t) && (!lstFogged.Contains(tl2.ID)) && (!lstVisible.Contains(tl2.ID)))
                    {
                        lstFogged.Add(tl2.ID);
                    }
                }
            }
            p.VisibleTiles = lstVisible;
            p.FoggedTiles = lstFogged;
            p.VisitedTiles = lstVisited;
            p.MovableTiles = lstMovable;
            p.SetTileListsString();
        }

        public static Object ApplyAction(Player p, ButtonAction action, EntityType entity, int iID, GameContext dbG, TemplateContext dbT)
        {
            switch (action)
            {
                case ButtonAction.Enter:
                    {
                        if (entity == EntityType.Dwelling) { return EnterDwelling(p, iID, dbG, dbT); }
                        else { return null; }
                    }
                default: { return null; }
            }
        }

        public static Object EnterDwelling(Player p, int iID, GameContext dbG, TemplateContext dbT)
        {
            Dwelling dw = dbG.Dwellings.Find(iID);
            TDBDwelling dwBase = dbT.TDBDwellings.Find(dw.BaseDwellingID);
            TDBUnit leader = dbT.TDBUnits.Find(dw.LeaderID);
            return new InsideDwellingViewModel(dw, p, leader, UnitFactory.GetViewUnitsFromArray(dwBase.TradeUnits, dbT));
        }
       
    }
}