using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.GameModels;
using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.ViewModels
{
    public class DungeonViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsOpen { get; set; }
        public int Difficulty { get; set; }
        public bool IsHere { get; set; }
        public List<ActionButtonViewModel> Buttons { get; set; }
        public DungeonViewModel()
        {
        }
        public DungeonViewModel(Dungeon du, Player player = null)
        {
            this.ID = du.ID;
            this.Name = du.Name;
            this.Description = du.Description;
            this.Image = du.Image;
            this.IsOpen = du.IsOpen;
            this.Difficulty = du.Difficulty;
            this.Name = du.Name;
            if (player != null)
            {
                this.Buttons = new List<ActionButtonViewModel>();
                if (player.TileID == du.TileId)
                {
                    this.Buttons.Add(new ActionButtonViewModel("Enter", player.Color, ButtonAction.Enter, EntityType.Dungeon, this.ID));
                    this.Buttons.Add(new ActionButtonViewModel("Unlock", player.Color, ButtonAction.Unlock, EntityType.Dungeon, this.ID));
                    this.Buttons.Add(new ActionButtonViewModel("Crush (1)", player.Color, ButtonAction.Crush, EntityType.Dungeon, this.ID));
                    this.Buttons.Add(new ActionButtonViewModel("Lockpick (2)", player.Color, ButtonAction.Lockpick, EntityType.Dungeon, this.ID));
                }
            }
        }
    }
}