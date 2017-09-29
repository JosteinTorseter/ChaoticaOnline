using ChaoticaOnline.lib;
using ChaoticaOnline.TemplateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.ViewModels
{
    public class SpecialViewModel
    {
        public SmallSpecialViewModel SubItem { get; set; }
        public string Description { get; set; }
        public int GoldValue { get; set; }
        public int ManaCost { get; set; }

        public string Effects { get; set; }
        public string TargetType { get; set; }
        public string RequiredAlignment { get; set; }
        public string RangeType { get; set; }
        public string ActionType { get; set; }
        public Dictionary<string, string> Restrictions { get; set; } = new Dictionary<string, string>();

        public List<ActionButtonViewModel> Buttons { get; set; }

        public SpecialViewModel()
        {
        }
        public SpecialViewModel(TDBSpecial spec, bool isBuy, bool canBuy, int buyPriceOrPowerReq, string pColor)
        {
            this.SubItem = new SmallSpecialViewModel(spec, isBuy, canBuy, buyPriceOrPowerReq);
            this.Description = spec.Description;
            this.GoldValue = spec.GoldValue;
            this.ManaCost = spec.ManaCost;
            this.Buttons = new List<ActionButtonViewModel>();
            if (isBuy && canBuy)
            {
                this.Buttons.Add(new ActionButtonViewModel("Buy (" + buyPriceOrPowerReq + " Gold)", pColor, ButtonAction.Buy, EntityType.SpecialAction, this.SubItem.BaseID));
            }
        }
    }
}