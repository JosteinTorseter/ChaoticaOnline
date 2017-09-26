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
        public string Text()
        {
            string res = "";
            int iVal = Calc.Round(this.Value, -1);
            bool bNeg = (iVal < 0);
            bool AddPlussMinus = true;
            switch (this.BonusType)
            {
                case BonusType.AttackBonus:
                    {
                        res += " Attack";
                        break;
                    }
                case BonusType.DefenceBonus:
                    {
                        res += " Defence";
                        break;
                    }
                case BonusType.Command:
                    {
                        res += " Command";
                        break;
                    }
                case BonusType.ConBonus:
                    {
                        res += " Constitution";
                        break;
                    }
                case BonusType.CunBonus:
                    {
                        res += " Cunning";
                        break;
                    }
                case BonusType.DexBonus:
                    {
                        res += " Dexterity";
                        break;
                    }
                case BonusType.DMGBonus:
                    {
                        res += " Damage";
                        break;
                    }
                case BonusType.InstantKillChance:
                    {
                        AddPlussMinus = false;
                        res += "% Killchance";
                        break;
                    }
                case BonusType.IntBonus:
                    {
                        res += " Intelligence";
                        break;
                    }
                case BonusType.MagicDamage:
                    {
                        res += " Magic damage";
                        break;
                    }
                case BonusType.MagicPower:
                    {
                        res += " Magic power";
                        break;
                    }
                case BonusType.MagicRange:
                    {
                        res += " Magic range";
                        break;
                    }
                case BonusType.MagResistance:
                    {
                        res += " Magic res.";
                        break;
                    }
                case BonusType.ManaDot:
                    {
                        res += " Mana pr.turn";
                        break;
                    }
                case BonusType.MaxDMGBonus:
                    {
                        res += " Max damage";
                        break;
                    }
                case BonusType.MaxHPBonus:
                    {
                        res += " HP";
                        break;
                    }
                case BonusType.MaxManaBonus:
                    {
                        res += " Mana";
                        break;
                    }
                case BonusType.MoveBonus:
                    {
                        res += " Movement";
                        break;
                    }
                case BonusType.NrOfAttacks:
                    {
                        res += " #Attacks";
                        break;
                    }
                case BonusType.NrOfTargets:
                    {
                        res += " #Targets";
                        break;
                    }
                case BonusType.RangeBonus:
                    {
                        res += " Range";
                        break;
                    }
                case BonusType.Resistance:
                    {
                        res += " Resist";
                        break;
                    }
                case BonusType.SpeedBonus:
                    {
                        res += " Speed";
                        break;
                    }
                case BonusType.StrBonus:
                    {
                        res += " Strength";
                        break;
                    }
                case BonusType.WisBonus:
                    {
                        res += " Wisdom";
                        break;
                    }
                case BonusType.TwoHanded:
                    {
                        return "Two-handed";
                    }
            }

            if (res == "") { return res; }
            res = iVal.ToString() + res;
            if (AddPlussMinus)
            {
                if (bNeg) { res = "-" + res; }
                else { res = "+" + res; }
            }
            return res;
        }
    }
}