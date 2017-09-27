using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.TemplateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.ViewModels
{
    public class InsideDwellingViewModel
    {
        public SmallUnitViewModel Leader { get; set; }

        public List<SmallUnitViewModel> TradeUnits { get; set; }
        public List<SmallWorldItemViewModel> TradeItems { get; set; }
        public List<SmallSpecialViewModel> Training { get; set; }
        public DwellingViewModel Dwelling;

        public InsideDwellingViewModel()
        {
        }
        public InsideDwellingViewModel(Dwelling dw, Player player, TDBUnit leader, 
            List<SmallUnitViewModel> tradeUnits,
            List<SmallWorldItemViewModel> tradeItems,
            List<SmallSpecialViewModel> training)
        {
            this.Dwelling = new DwellingViewModel(dw, player);
            this.Leader = new SmallUnitViewModel(leader, false, 0, true);
            this.TradeUnits = tradeUnits;
            this.TradeItems = tradeItems;
            this.Training = training;
        }
    }
}