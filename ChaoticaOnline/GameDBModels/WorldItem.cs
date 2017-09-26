using ChaoticaOnline.lib;
using ChaoticaOnline.TemplateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.GameDBModels
{
    public class WorldItem
    {
        public int ID { get; set; }
        public int BaseItemID { get; set; }
        public bool Wearing { get; set; }
        public ItemCategory Category { get; set; }
        public int Count { get; set; }
        public string TypeName { get; set; }
        public virtual ICollection<Bonus> Bonuses { get; set; } = new List<Bonus>();

        public int GetBonus(BonusType bt)
        {
            foreach (Bonus b in this.Bonuses)
            {
                if (b.BonusType == bt) { return b.IntValue(); }
            }
            return 0;
        }

        public WorldItem()
        {
        }
        public WorldItem(TDBWorldItem it, int count = 1, bool wearing = false)
        {
            this.BaseItemID = it.ID;
            this.Category = it.Category;
            this.Count = count;
            this.Wearing = wearing;
            this.TypeName = it.TypeName;
            foreach (Bonus b in it.Bonuses)
            {
                this.Bonuses.Add(b);
            }
        }
        public bool IsTwoHanded()
        {
            foreach (Bonus b in this.Bonuses)
            {
                if (b.BonusType == BonusType.TwoHanded) { return true; }
            }
            return false;
        }
    }
}