using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.ViewModels
{
    public class MulipleViewReturnViewModel
    {
        public List<TileViewModel> Tiles { get; set; } = new List<TileViewModel>();
        public List<SmallUnitViewModel> Party { get; set; } = new List<SmallUnitViewModel>();
        public List<SmallUnitViewModel> Roster { get; set; } = new List<SmallUnitViewModel>();
        public List<SmallWorldItemViewModel> Inventory { get; set; } = new List<SmallWorldItemViewModel>();
        public MapViewModel Map { get; set; }
        public TileViewModel Tile { get; set; }
        public PlayerViewModel InfoPlayer { get; set; }
        public CharSheetViewModel CharSheet { get; set; }
        public UnitViewModel Unit { get; set; }
        public DrawResultViewModel CardResult { get; set; }
        public DungeonViewModel Dungeon { get; set; }
        public DwellingViewModel Dwelling { get; set; }
    }
}