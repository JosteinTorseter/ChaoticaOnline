using ChaoticaOnline.DAL;
using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.GameModels;
using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.TemplateModels
{
    public class TDBUnit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public Alignment Alignment { get; set; }
        public int BaseDifficulty { get; set; }
        public int DifficultyPrLvl { get; set; }
        public double BaseAttack { get; set; }
        public double BaseDefence { get; set; }
        public double BaseMinDamage { get; set; }
        public double BaseMaxDamage { get; set; }
        public double BaseHP { get; set; }
        public double BaseMana { get; set; }
        public double ManaBonus { get; set; }
        public double HPBonus { get; set; }
        public double AttackBonus { get; set; }
        public double DefenceBonus { get; set; }
        public double DamageBonus { get; set; }
        public double MaxDmgBonus { get; set; }
        public int MaxLevel { get; set; }
        public int RaceID { get; set; }
        public int ClassID { get; set; }
        public bool Takes2Slots { get; set; }
        public bool CanRandomSpawn { get; set; }
        public bool IsUnique { get; set; }
        public int GoldValue { get; set; }
        public int Level2XP { get; set; }
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

        public List<Bonus> LevelUpBonuses (TDBClass c, TDBRace r)
        {
            List<Bonus> res = new List<Bonus>();
            res.Add(new Bonus(BonusType.AttackBonus, this.BaseAttack, 1));
            res.Add(new Bonus(BonusType.DefenceBonus, this.BaseDefence, 1));
            res.Add(new Bonus(BonusType.MaxHPBonus, this.BaseHP, 1));
            res.Add(new Bonus(BonusType.MaxHPBonus, this.HPBonus, 0));
            res.Add(new Bonus(BonusType.AttackBonus, this.AttackBonus, 0));
            res.Add(new Bonus(BonusType.DefenceBonus, this.DefenceBonus, 0));
            res.Add(new Bonus(BonusType.DMGBonus, this.BaseMinDamage, 1));
            res.Add(new Bonus(BonusType.DMGBonus, this.DamageBonus, 0));
            res.Add(new Bonus(BonusType.MaxDMGBonus, this.MaxDmgBonus, 0));
            res.Add(new Bonus(BonusType.MaxDMGBonus, this.BaseMaxDamage, 1));
            res.Add(new Bonus(BonusType.MaxManaBonus, this.ManaBonus, 0));
            res.Add(new Bonus(BonusType.MaxManaBonus, this.BaseMana, 1));
            foreach (int i in this.RangeBonuses)
            {
                res.Add(new Bonus(BonusType.AttackBonus, 1, i));
            }

            res.Add(new Bonus(BonusType.AttackBonus, c.AttackBonus, 0));
            res.Add(new Bonus(BonusType.DMGBonus, c.DamageBonus, 0));
            res.Add(new Bonus(BonusType.DefenceBonus, c.DefenceBonus, 0));
            res.Add(new Bonus(BonusType.MaxHPBonus, c.HPBonus, 0));
            res.Add(new Bonus(BonusType.MaxManaBonus, c.ManaBonus, 0));
            res.Add(new Bonus(BonusType.MaxDMGBonus, c.MaxDmgBonus, 0));
            res.Add(new Bonus(BonusType.MoveBonus, c.MoveBonus, 0));
            foreach (int i in c.RangeBonuses)
            {
                res.Add(new Bonus(BonusType.AttackBonus, 1, i));
            }

            res.Add(new Bonus(BonusType.AttackBonus, r.BaseHP, 1));
            res.Add(new Bonus(BonusType.AttackBonus, r.HPBonus, 0));

            return res;
        }
        
    }
}