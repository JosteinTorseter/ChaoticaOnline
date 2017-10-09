using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChaoticaOnline.lib;
using ChaoticaOnline.GameDBModels;

namespace ChaoticaOnline.ViewModels
{
    public class DrawResultViewModel
    {
        public DwellingViewModel Dwelling { get; set; }
        public DungeonViewModel Dungeon { get; set; }
        public ArmyViewModel Army { get; set; }
        public UnitViewModel Unit { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }
        public List<ActionButtonViewModel> Buttons { get; set; }
        public DrawResultViewModel()
        {
        }
        public DrawResultViewModel(TileCardType type, Object obj, Player p, string sHeader = "")
        {
            this.Buttons = new List<ActionButtonViewModel>();
            switch (type)
            {
                case TileCardType.Dungeon:
                    {
                        this.Header = "You have found a dungeon!";
                        this.Buttons.Add(new ActionButtonViewModel("OK", p.Color, ButtonAction.Close, EntityType.None, 0));
                        this.Dungeon = new DungeonViewModel((Dungeon)obj);
                        break;
                    }
                case TileCardType.Dwelling:
                    {
                        this.Header = "Someone lives here!";
                        this.Buttons.Add(new ActionButtonViewModel("OK", p.Color, ButtonAction.Close, EntityType.None, 0));
                        this.Dwelling = new DwellingViewModel((Dwelling)obj);
                        break;
                    }
                case TileCardType.Army:
                    {
                        this.Header = "You have encountered a party!";
                        this.Buttons.Add(new ActionButtonViewModel("OK", p.Color, ButtonAction.Close, EntityType.None, 0));
                        this.Army = new ArmyViewModel((Party)obj);
                        break;
                    }
                case TileCardType.Unit:
                    {
                        this.Header = "A likeminded individual asks to join you army.";
                        this.Footer = "Do you accept?";
                        this.Buttons.Add(new ActionButtonViewModel("Yes", p.Color, ButtonAction.AcceptUnit, EntityType.Unit, ((Unit)obj).BaseUnitID));
                        this.Unit = new UnitViewModel((Unit)obj, false, false, 0, p.Color);
                        break;
                    }
            }
            if (sHeader != "") { this.Header = sHeader; }
        }
    }
}