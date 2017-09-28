using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.lib;
using ChaoticaOnline.TemplateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.ViewModels
{
    public class SmallWorldItemViewModel
    {
        public int ID { get; set; }
        public int BaseItemID { get; set; }
        public int GoldValue { get; set; }
        public string Image { get; set; }
        public string TypeName { get; set; }
        public ItemCategory Category { get; set; }
        public string ToolTip { get; set; }

        public SmallWorldItemViewModel()
        {
        }
        public SmallWorldItemViewModel(TDBWorldItem it, bool isBuy, bool canBuy, int buyPriceOrPowerReqOrCount, int iID = 0)
        {
            this.ID = iID;
            this.BaseItemID = it.ID;
            this.Image = it.Image;
            this.TypeName = it.TypeName;
            this.Category = it.Category;
            this.GoldValue = it.GoldValue;
        }
        public static SmallWorldItemViewModel GetOffhandPlaceholder(string image)
        {
            SmallWorldItemViewModel it = new SmallWorldItemViewModel();
            it.Category = ItemCategory.HeroItem;
            it.TypeName = "Offhand";
            it.Image = image;
            it.ToolTip = "Offhand";
            return it;
        }
    }
}