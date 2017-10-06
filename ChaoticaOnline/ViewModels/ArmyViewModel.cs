using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.ViewModels
{
    public class ArmyViewModel
    {
        public Alignment Alignment { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Difficulty { get; set; }
        public ArmyViewModel()
        {
        }
        public ArmyViewModel(Party p)
        {
            this.Alignment = p.Alignment;
            this.Name = p.Name;
            this.Image = p.Image;
            this.Difficulty = 5;
        }
    }
}