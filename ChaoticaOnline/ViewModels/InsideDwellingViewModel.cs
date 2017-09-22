using ChaoticaOnline.GameDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.ViewModels
{
    public class InsideDwellingViewModel
    {
        public string LeaderName { get; set; }
        public string LeaderImage { get; set; }

        public List<UnitViewModel> TradeUnits { get; set; }
        public DwellingViewModel Dwelling;

        public InsideDwellingViewModel()
        {
        }
        public InsideDwellingViewModel(Dwelling dw, Player player, string leaderImage, List<Unit> tradeUnits)
        {
            this.Dwelling = new DwellingViewModel(dw, player);
            this.LeaderImage = leaderImage;
            this.LeaderName = dw.LeaderName;
            foreach (Unit u in tradeUnits)
            {
                this.TradeUnits.Add(new UnitViewModel(u));
            }
        }
    }
}