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
        public DrawResultViewModel()
        {
        }
        public DrawResultViewModel(EntityType type, Object obj, Player p)
        {
            switch (type)
            {
                case EntityType.Dungeon:
                    {
                        this.Dungeon = new DungeonViewModel((Dungeon)obj);
                        break;
                    }
                case EntityType.Dwelling:
                    {
                        this.Dwelling = new DwellingViewModel((Dwelling)obj);
                        break;
                    }
                case EntityType.Army:
                    {
                        this.Army = new ArmyViewModel((Party)obj);
                        break;
                    }
                case EntityType.Unit:
                    {
                        this.Unit = new UnitViewModel((Unit)obj, false, false, 0, p.Color);
                        break;
                    }
            }
        }
    }
}