using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.GameModels;
using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.ViewModels
{
    public class GameViewModel
    {
        public int ID { get; set; }
        public MapViewModel Map { get; set; }
        public PlayerViewModel Player { get; set; }
        public TileViewModel Tile { get; set; }
        public List<SmallUnitViewModel> Party { get; set; }
        public GameViewModel()
        {
        }
        public GameViewModel(int iID, Map map, Player player, Tile tile, Party party, Dictionary<int, string> dicPlayerColors)
        {
            this.ID = iID;
            this.Map = new MapViewModel(map, player, dicPlayerColors, tile.ID);
            this.Player = new PlayerViewModel(player);
            this.Tile = new TileViewModel(tile, TileSelectionType.None, dicPlayerColors, player);
            this.Party = new List<SmallUnitViewModel>();
            foreach (Unit u in party.Units)
            {
                this.Party.Add(new SmallUnitViewModel(u));
            }
        }
    }
}