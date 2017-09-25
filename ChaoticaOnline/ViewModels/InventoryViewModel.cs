using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.ViewModels
{
    public class InventoryViewModel
    {
        public List<SmallWorldItemViewModel> Items { get; set; } = new List<SmallWorldItemViewModel>();
        public string AlignmentColor { get; set; }
        public InventoryViewModel()
        {
        }
        public InventoryViewModel(List<SmallWorldItemViewModel> items, string strColor)
        {
            this.Items = items;
            this.AlignmentColor = strColor;
        }
    }
}