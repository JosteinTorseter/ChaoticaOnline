using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.GameDBModels
{
    public class Bonus
    {
        public int ID { get; set; }
        public BonusType BonusType { get; set; }
        public double Value { get; set; }
        public int Trigger { get; set; }

        [ForeignKey("WorldItem")]
        public int WorldItemId { get; set; }
        public virtual WorldItem WorldItem { get; set; }

        public Bonus()
        {
        }
        public Bonus(BonusType b, double v, int t)
        {
            this.BonusType = b;
            this.Value = v;
            this.Trigger = t;
        }
        public int IntValue()
        {
            return Calc.Round(this.Value, -1);
        }
    }
}