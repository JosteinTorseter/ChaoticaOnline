using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.GameModels;
using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.ViewModels
{
    public class MapViewModel
    {
        public string Name { get; set; }
        public int XCount { get; set; }
        public int YCount { get; set; }
        public int SelectedTileID { get; set; }
        public int QuestOriginTileID { get; set; }
        public int QuestTargetTileID { get; set; }
        public List<TileViewModel> Tiles { get; set; } = new List<TileViewModel>();
        public MapViewModel()
        {
        }
        public MapViewModel(Map m, Dictionary<int, string> dicPlayerColors, int sTileID = 0, int qoTileID = 0, int qtTileID = 0)
        {
            this.Name = "Chaotica";
            SelectedTileID = sTileID;
            QuestOriginTileID = qoTileID;
            QuestTargetTileID = qtTileID;
            this.SetTiles(m.Tiles.ToList(), dicPlayerColors);
        }
        public MapViewModel(Dungeon d, Dictionary<int, string> dicPlayerColors, int sTileID = 0)
        {
            this.Name = d.Name;
            SelectedTileID = sTileID;
            this.SetTiles(d.Tiles.ToList(), dicPlayerColors);
        }
        private void SetTiles(List<Tile> lst, Dictionary<int, string> dicPlayerColors)
        {
            foreach (Tile t in lst)
            {
                if (t.XCoord + 1 > this.XCount) { this.XCount = t.XCoord + 1; }
                if (t.YCoord + 1 > this.YCount) { this.YCount = t.YCoord + 1; }
                TileSelectionType sSelect = TileSelectionType.None;
                if (SelectedTileID == t.ID) { sSelect = TileSelectionType.Selected; }
                if (QuestOriginTileID == t.ID) { sSelect = TileSelectionType.QuestOrigin; }
                if (QuestTargetTileID == t.ID) { sSelect = TileSelectionType.QuestTarget; }
                this.Tiles.Add(new TileViewModel(t, sSelect, dicPlayerColors));
            }
        }
        public TileViewModel GetTile(int X, int Y)
        {
            foreach (TileViewModel t in this.Tiles)
            {
                if (t.XDisplay == X && t.YDisplay == Y)
                {
                    return t;
                }
            }
            return null;
        }
    }
}