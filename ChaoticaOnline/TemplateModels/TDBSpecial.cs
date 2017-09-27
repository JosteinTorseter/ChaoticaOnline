using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.TemplateModels
{
    public class TDBSpecial
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public SpecialCategory Category { get; set; }
        public TargetType Target { get; set; }
        public string EffectsString { get; set; }
        public bool LevelUpEnabled { get; set; }
        public int GoldValue { get; set; }
        public Alignment RequiredAlignment { get; set; }
        public ActionRangeType RangeType { get; set; }
        public int ManaCost { get; set; }

        [NotMapped]
        public List<Effect> Bonuses
        {
            get
            {
                return DictionaryHack.GetEffectsByString(EffectsString);
            }
        }
        public string RequiredStatsString { get; set; }
        [NotMapped]
        public Dictionary<int, int> RequiredStats
        {
            get
            {
                return DictionaryHack.GetIntsByString(RequiredStatsString);
            }
        }
    }
}