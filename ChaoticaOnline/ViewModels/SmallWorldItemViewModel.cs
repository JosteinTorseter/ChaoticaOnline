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
        public string Name { get; set; }
        public string Image { get; set; }
        public string TypeName { get; set; }
        public ItemCategory Category { get; set; }
        public int Count { get; set; }
        public int GoldValue { get; set; }
        public SmallWorldItemViewModel()
        {
        }
        public SmallWorldItemViewModel(TDBWorldItem it, int iCount, int iID = 0)
        {
            this.ID = iID;
            this.BaseItemID = it.ID;
            this.Name = it.Name;
            this.Image = it.Image;
            this.TypeName = it.TypeName;
            this.Category = it.Category;
            this.Count = iCount;
            this.GoldValue = it.GoldValue;
        }
        public static SmallWorldItemViewModel GetOffhandPlaceholder(string image)
        {
            SmallWorldItemViewModel it = new SmallWorldItemViewModel();
            it.Category = ItemCategory.HeroItem;
            it.TypeName = "Offhand";
            it.Image = image;
            it.Count = 1;
            return it;
        }
    }
}