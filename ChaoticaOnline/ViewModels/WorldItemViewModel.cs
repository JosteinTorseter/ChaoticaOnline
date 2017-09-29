using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.lib;
using ChaoticaOnline.TemplateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.ViewModels
{
    public class WorldItemViewModel
    {
        public SmallWorldItemViewModel SubItem { get; set; }
        public Dictionary<string, string> Restrictions { get; set; } = new Dictionary<string, string>();
        public string RarityColor { get; set; }
        public string RarityInverseColor { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Bonus> Bonuses { get; set; }
        public List<ActionButtonViewModel> Buttons { get; set; }
        public WorldItemViewModel()
        {
        }
        public WorldItemViewModel(Player p, TDBWorldItem bit, int iCount, bool isBuy, bool canBuy, int buyPriceOrPowerReq, int iID = 0)
        {
            this.SubItem = new SmallWorldItemViewModel(bit, false, false, iCount, iID);
            this.RarityColor = Statics.RarityColor(bit.Rarity, bit.IsUnique);
            this.RarityInverseColor = Statics.InverseRarityColor(bit.Rarity, bit.IsUnique);
            this.Description = bit.Description;
            this.Name = bit.Name;
            this.Bonuses = bit.Bonuses;
            string sRes = "";
            string sCol = "";
            if ((int)bit.RequiredAlignment < 2)
            {
                sRes = bit.RequiredAlignment.ToString() + " only";
                sCol = "black";
                if (bit.RequiredAlignment != p.Alignment) { sCol = "red"; }
                this.Restrictions.Add(sRes, sCol);
            }
            foreach (KeyValuePair<int, int> kv in bit.RequiredStats)
            {
                sRes = "Minimum " + kv.Value.ToString() + " " + ((HeroStat)kv.Key).ToString();
                sCol = "black";
                if (!p.MeetsAttribReq(kv.Key, kv.Value)) { sCol = "red"; }
                this.Restrictions.Add(sRes, sCol);
            }
            this.Buttons = new List<ActionButtonViewModel>();
            if (isBuy && canBuy)
            {
                this.Buttons.Add(new ActionButtonViewModel("Buy (" + buyPriceOrPowerReq + " Gold)", p.Color, ButtonAction.Buy, EntityType.WorldItem, this.SubItem.ID));
            }
        }
        
}
}