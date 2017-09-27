using ChaoticaOnline.lib;
using ChaoticaOnline.TemplateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.ViewModels
{
    public class SmallSpecialViewModel
    {
        public int BaseID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public SpecialCategory Category { get; set; }
        public int GoldValue { get; set; }
        public string Tooltip { get; set; }
        public SmallSpecialViewModel()
        {
        }
        public SmallSpecialViewModel(TDBSpecial spec, bool isBuy, bool canBuy, int buyPriceOrPowerReqOrCount)
        {
            this.BaseID = spec.ID;
            this.Image = spec.Image;
            this.Category = spec.Category;
            if (!isBuy)
            {
                this.Tooltip = spec.Name + " (" + buyPriceOrPowerReqOrCount.ToString() + ")";
            } else
            {
                if (canBuy)
                {
                    this.Tooltip = spec.Name + " - " + buyPriceOrPowerReqOrCount.ToString() + " Gold";
                } else
                {
                    this.Tooltip = spec.Name + " - at " + buyPriceOrPowerReqOrCount.ToString();
                }
            }
        }
    }
}