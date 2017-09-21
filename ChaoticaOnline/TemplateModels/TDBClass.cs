using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.TemplateModels
{
    public class TDBClass
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool HeroAllowed { get; set; }
        public double HPBonus { get; set; }
        public double AttackBonus { get; set; }
        public double DefenceBonus { get; set; }
        public double DamageBonus { get; set; }
        public double MaxDmgBonus { get; set; }
        public double MoveBonus { get; set; }
        public double ManaBonus { get; set; }
        public string RangeBonusesString { get; set; }
        [NotMapped]
        public int[] RangeBonuses
        {
            get
            {
                if (RangeBonusesString == null || RangeBonusesString == "") { return new int[0]; }
                return Array.ConvertAll(RangeBonusesString.Split(';'), Int32.Parse);
            }
            set
            {
                RangeBonusesString = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }

    }
}