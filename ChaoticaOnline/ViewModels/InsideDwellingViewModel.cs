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
        public DwellingViewModel Dwelling;

        public InsideDwellingViewModel()
        {
        }
        public InsideDwellingViewModel(Dwelling dw, Player player, TDBUnit leader, List<SmallUnitViewModel> tradeUnits)
        {
            this.Dwelling = new DwellingViewModel(dw, player);
            this.Leader = new SmallUnitViewModel(leader, false, 0);
            this.TradeUnits = tradeUnits;
        }
    }
}