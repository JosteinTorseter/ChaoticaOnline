using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.GameModels;
using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.ViewModels
{
    public class TileViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int XDisplay { get; set; }
        public int YDisplay { get; set; }
        public int Difficulty { get; set; }
        public string Image { get; set; }
        public string BGColor { get; set; }
        public string SelectionClass { get; set; }
        public int MovePoints { get; set; }
        public int TaxPoints { get; set; }
        public int SearchPoints { get; set; }
        public int ExplorePoints { get; set; }
        public int CardCount { get; set; }
        public bool CanSearch { get; set; }
        public bool CanTax { get; set; }
        public bool CanExplore { get; set; }
        public bool CanMove { get; set; }
        public bool IsHere { get; set; }
        public List<PlayerViewModel> Players { get; set; } = new List<PlayerViewModel>();
        public List<DwellingViewModel> Dwellings { get; set; } = new List<DwellingViewModel>();
        public List<DungeonViewModel> Dungeons { get; set; } = new List<DungeonViewModel>();
        public List<ArmyViewModel> Armies { get; set; } = new List<ArmyViewModel>();
        public List<ActionButtonViewModel> Buttons { get; set; }
        public TileViewModel()
        {
        }
        public TileViewModel(Tile tile, TileSelectionType eSelect, Dictionary<int, string> dicPlayerColors, Player player = null)
        {
            this.ID = tile.ID;
            this.Name = tile.Name;
            this.Description = tile.Description;
            this.XDisplay = tile.XCoord;
            this.YDisplay = tile.YCoord;
            this.Difficulty = tile.Difficulty;
            this.Image = tile.Image;
            this.BGColor = tile.BGColor;
            this.SelectionClass = "tile-div";
            if (player != null) { this.CardCount = tile.GetCards(player).Count; }
            switch (eSelect)
            {
                case TileSelectionType.Selected:
                    {
                        this.SelectionClass += " terrain-div-active";
                        break;
                    }
                case TileSelectionType.QuestOrigin:
                    {
                        this.SelectionClass += " terrain-div-questorigin";
                        break;
                    }
                case TileSelectionType.QuestTarget:
                    {
                        this.SelectionClass += " terrain-div-questtarget";
                        break;
                    }
            }
            if (player != null)
            {
                if (player.MovableTiles == null)
                {
                    player.ArrangeStringToTileLists();
                }
                this.Buttons = new List<ActionButtonViewModel>();
                // !!! Set from player
                if (player.TileID == tile.ID)
                {
                    if (this.CardCount > 0) { this.Buttons.Add(new ActionButtonViewModel("Explore (2)", player.Color, ButtonAction.Explore, EntityType.Tile, this.ID)); }
                    this.Buttons.Add(new ActionButtonViewModel("Search (3)", player.Color, ButtonAction.Search, EntityType.Tile, this.ID));
                    this.Buttons.Add(new ActionButtonViewModel("Tax (12)", player.Color, ButtonAction.Tax, EntityType.Tile, this.ID));
                    this.Buttons.Add(new ActionButtonViewModel("Rest (1)", player.Color, ButtonAction.Rest, EntityType.Tile, this.ID));
                } else if (player.MovableTiles.Contains(tile.ID))
                {
                    this.Buttons.Add(new ActionButtonViewModel("Travel (4)", player.Color, ButtonAction.Move, EntityType.Tile, this.ID));
                }
                //public int MovePoints { get; set; }
                //public int TaxPoints { get; set; }
                //public int SearchPoints { get; set; }
                //public int ExplorePoints { get; set; }
                //public bool CanSearch { get; set; }
                //public bool CanTax { get; set; }
                //public bool CanExplore { get; set; }
                //public bool CanMove { get; set; }
                //public bool IsHere { get; set; }
            }
            foreach (int playerID in tile.Players)
            {
                this.Players.Add(new PlayerViewModel(playerID, dicPlayerColors[playerID]));
            }
            foreach (Dwelling dw in tile.Dwellings)
            {
                this.Dwellings.Add(new DwellingViewModel(dw));
            }
            foreach (Dungeon du in tile.Dungeons)
            {
                this.Dungeons.Add(new DungeonViewModel(du));
            }
        }
    }
}