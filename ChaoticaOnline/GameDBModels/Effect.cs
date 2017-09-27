using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.GameDBModels
{
    public class Effect
    {
        public EffectCategory Category { get; set; }
        public int Duration { get; set; }
        public int DurationPerPower { get; set; }
        public List<Bonus> Bonuses { get; set; } = new List<Bonus>();
        public int Value { get; set; }
        public int ValuePerPower { get; set; }
    }
}