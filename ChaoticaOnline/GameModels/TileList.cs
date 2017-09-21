using ChaoticaOnline.GameDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.GameModels
{
    public class TileList
    {
        public static Tile GetTile(List<Tile> lst, int iID)
        {
            foreach (Tile t in lst)
            {
                if (t.ID == iID)
                {
                    return t;
                }
            }
            return null;
        }
        public static Tile GetTileByListIndex(List<Tile> lst, int iIndex)
        {
            foreach (Tile t in lst)
            {
                if (t.ListIndex == iIndex)
                {
                    return t;
                }
            }
            return null;
        }
        public static Tile GetTile(List<Tile> lst, int iX, int iY)
        {
            foreach (Tile t in lst)
            {
                if (t.XCoord == iX && t.YCoord == iY)
                {
                    return t;
                }
            }
            return null;
        }
        public static List<Tile> GetSurroundingTiles(List<Tile> lst, int iX, int iY)
        {
            List<Tile> tList = new List<Tile>();
            foreach (Tile t in lst)
            {
                if (t.IsNeighbour(iX, iY))
                {
                    tList.Add(t);
                }
                if (tList.Count == 4) { break; }
            }
            return tList;
        }
    }
}