using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.GameModels;
using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.ViewModels
{
    public class DwellingViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Alignment Alignment { get; set; }
        public bool CanBeRaided { get; set; }
        public bool CanBeAttacked { get; set; }
        public int Happiness { get; set; }
        public int Power { get; set; }
        public List<ActionButtonViewModel> Buttons { get; set; }
        public DwellingViewModel()
        {
        }
        public DwellingViewModel(Dwelling dw, Player p = null)
        {
            this.ID = dw.ID;
            this.Name = dw.Name;
            this.Description = dw.Description;
            this.Image = dw.Image;
            this.Alignment = dw.Alignment;
            this.Power = dw.Power;

            if (p != null)
            {
                this.Buttons = new List<ActionButtonViewModel>();

                // !!! Base these on player
                if (p.TileID == dw.TileId)
                {
                    this.Buttons.Add(new ActionButtonViewModel("Enter", p.Color, ButtonAction.Enter, EntityType.Dwelling, this.ID));
                    this.Buttons.Add(new ActionButtonViewModel("Attack (2)", p.Color, ButtonAction.Attack, EntityType.Dwelling, this.ID));
                    this.Buttons.Add(new ActionButtonViewModel("Raid (2)", p.Color, ButtonAction.Raid, EntityType.Dwelling, this.ID));
                } 
                this.CanBeRaided = dw.CanBeRaided;
                this.CanBeAttacked = dw.CanBeAttacked;
                this.Happiness = 50;

            }
        }
    }
}