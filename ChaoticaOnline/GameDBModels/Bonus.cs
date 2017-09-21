using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.GameDBModels
{
    public class Bonus
    {
        public BonusType BonusType { get; set; }
        public double Value { get; set; }
        public int Trigger { get; set; }
        public Bonus()
        {
        }
        public Bonus(BonusType b, double v, int t)
        {
            this.BonusType = b;
            this.Value = v;
            this.Trigger = t;
        }
    }
}