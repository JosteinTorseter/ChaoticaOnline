using ChaoticaOnline.GameModels;
using ChaoticaOnline.lib;
using ChaoticaOnline.TemplateModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.GameDBModels
{
    public class Unit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string RaceClassName { get; set; }
        public int BaseUnitID { get; set; }
        public Alignment Alignment { get; set; }
        public int Level { get; set; }
        public int MaxLevel { get; set; }
        public int XP { get; set; }
        public double Attack { get; set; }
        public double Defence { get; set; }
        public double MagicPower { get; set; }
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public int MaxMana { get; set; }
        public int Mana { get; set; }
        public double DamageBase { get; set; }
        public double DamageRoll { get; set; }
        public double Speed { get; set; }
        public int AttackRange { get; set; }
        public int MagicRange { get; set; }
        public bool Takes2Slots { get; set; }
        public int Line { get; set; }
        public bool IsHero { get; set; }
        public int PartyListIndex { get; set; }
        public string Image { get; set; }
        public int Difficulty { get; set; }
        public int NextLevelXP { get; set; }
        public double Resistance { get; set; }
        public double MagicResistance { get; set; }
        public double NrOfAttacks { get; set; }
        public double NrOfTargets { get; set; }
        public int CompanionItem { get; set; }

        [ForeignKey("Party")]
        public int PartyId { get; set; }
        public virtual Party Party { get; set; }

        public Unit()
        {
        }
        public Unit(TDBUnit u, TDBRace r, TDBClass c, int iMaxLvlOverride = 0, string sReplaceName = "", Alignment aOverride = Alignment.Inherited)
        {
            this.BaseUnitID = u.ID;
            this.Name = u.Name;
            if (sReplaceName != "") { this.Name = sReplaceName; }
            this.RaceClassName = r.Name;
            if (c.Name != "Normal")
            {
                this.RaceClassName += " " + c.Name;
            }
            this.Image = u.Image;
            this.Alignment = u.Alignment;
            if (aOverride != Alignment.Inherited)
            {
                this.Alignment = aOverride;
            }
            else
            {
                if (this.Alignment == Alignment.Inherited)
                {
                    this.Alignment = r.Alignment;
                }
            }
            this.Difficulty = u.BaseDifficulty;
            this.MaxLevel = u.MaxLevel;
            if (iMaxLvlOverride > 0) { this.MaxLevel = iMaxLvlOverride; }
            this.NextLevelXP = u.Level2XP;
            this.AttackRange = 1;
            this.MagicRange = 1;
            this.NrOfAttacks = 1;
            this.NrOfTargets = 1;
            this.Takes2Slots = u.Takes2Slots;
            this.Line = 1;
            this.IsHero = c.HeroAllowed;
        }

        public void LevelUp(List<Bonus> LevelUpBonuses, int iLevel2XP, int iLevels = 1)
        {
            int iTotalLvl = this.Level + iLevels;

            foreach (Bonus b in LevelUpBonuses.Where(bn => (bn.Trigger > this.Level && bn.Trigger <= iTotalLvl) || bn.Trigger == 0).ToList())
            {
                switch (b.BonusType)
                {
                    case BonusType.AttackBonus:
                        {
                            this.Attack += b.Value;
                            break;
                        }
                    case BonusType.DefenceBonus:
                        {
                            this.Defence += b.Value;
                            break;
                        }
                    case BonusType.DMGBonus:
                        {
                            this.DamageBase += b.Value;
                            break;
                        }
                    case BonusType.MaxDMGBonus:
                        {
                            this.DamageRoll += b.Value;
                            break;
                        }
                    case BonusType.MaxHPBonus:
                        {
                            this.MaxHP += (int)b.Value;
                            this.HP += (int)b.Value;
                            break;
                        }
                    case BonusType.MaxManaBonus:
                        {
                            this.MaxMana += (int)b.Value;
                            this.Mana += (int)b.Value;
                            break;
                        }
                    case BonusType.RangeBonus:
                        {
                            this.AttackRange += (int)b.Value;
                            break;
                        }
                    case BonusType.SpeedBonus:
                        {
                            this.Speed += b.Value;
                            break;
                        }
                    case BonusType.MagResistance:
                        {
                            this.MagicResistance += b.Value;
                            break;
                        }
                    case BonusType.Resistance:
                        {
                            this.Resistance += b.Value;
                            break;
                        }
                    case BonusType.MagicRange:
                        {
                            this.MagicRange += (int)b.Value;
                            break;
                        }
                    case BonusType.MagicPower:
                        {
                            this.MagicPower += b.Value;
                            break;
                        }
                    case BonusType.NrOfAttacks:
                        {
                            this.NrOfAttacks += b.Value;
                            break;
                        }
                    case BonusType.NrOfTargets:
                        {
                            this.NrOfTargets += b.Value;
                            break;
                        }

                }
            }
            this.Level += iLevels;
            this.NextLevelXP = iLevel2XP;
            if (this.Level > 1)
            {
                this.NextLevelXP = (int)Math.Round(this.NextLevelXP * Math.Pow(1.6, this.Level - 1));
            }
        }

    }
}