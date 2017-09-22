using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.TemplateModels
{
    public class TDBRace
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int StartUnitID { get; set; }
        public int StartDwellingID { get; set; }
        public string Plural { get; set; }
        public string Singular { get; set; }
        public bool HeroAllowed { get; set; }
        public double BaseHP { get; set; }
        public double HPBonus { get; set; }
        public double MagicResistBonus { get; set; }
        public double ResistBonus { get; set; }
        public Alignment Alignment { get; set; }

        //Public Property Bonuses As List(Of LevelUpBonus)
        //Public Property HeroStatBonuses As List(Of LevelUpBonus)
        //Public Property TeamBonus As Bonus

    }
}