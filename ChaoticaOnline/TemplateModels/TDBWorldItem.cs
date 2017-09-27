using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.TemplateModels
{
    public class TDBWorldItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public bool IsUnique { get; set; }
        public bool CanRandomDrop { get; set; }
        public bool IsStartOption { get; set; }
        public ItemCategory Category { get; set; }
        public int Rarity { get; set; }
        public int GoldValue { get; set; }
        public Alignment RequiredAlignment { get; set; }

        public string BonusesString { get; set; }
        [NotMapped]
        public List<Bonus> Bonuses
        {
            get
            {
                return DictionaryHack.GetBonusesByString(BonusesString);
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
        public string SpecialsString { get; set; }
        [NotMapped]
        public Dictionary<int, int> Specials
        {
            get
            {
                return DictionaryHack.GetIntsByString(SpecialsString);
            }
        }
    }
}