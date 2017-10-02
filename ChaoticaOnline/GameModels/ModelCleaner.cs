using ChaoticaOnline.DAL;
using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.lib;
using ChaoticaOnline.TemplateModels;
using ChaoticaOnline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.GameModels
{
    public class ModelCleaner
    {
        public static List<TileViewModel> GetCleanedTiles(List<Tile> tiles, Player player)
        {
            List<TileViewModel> res = new List<TileViewModel>();

            int MinY = 1000;
            int MinX = 1000;
            for (int i = tiles.Count - 1; i > -1; i--)
            {
                TileVisibility tv = player.TileVisible(tiles[i].ID);
                switch (tv)
                {
                    case TileVisibility.Fogged:
                    case TileVisibility.Visible:
                        {
                            res.Add(new TileViewModel());
                            if (tiles[i].XCoord < MinX) { MinX = tiles[i].XCoord; }
                            if (tiles[i].YCoord < MinY) { MinY = tiles[i].YCoord; }
                            if (tv == TileVisibility.Fogged) { tiles[i].Image = "tile.jpg"; }
                            break;
                        }
                }
            }
            foreach (TileViewModel t in res)
            {
                t.XDisplay = t.XDisplay - MinX;
                t.YDisplay = t.YDisplay - MinY;
            }

            return res;
        }

        public static GetSpecialsDictionary(TemplateContext dbT, List<Special> specs)
        {
            List<int> lst = new List<int>();
            foreach (Special s in specs)
            {
                lst.Add(s.BaseID);
            }
            List<TDBSpecial> res = dbT.TDBSpecials.Where(sp => lst.co)
        }
    }
}