using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.GameModels;
using ChaoticaOnline.lib;
using ChaoticaOnline.TemplateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.ViewModels
{
    public class UnitViewModel
    {
        private static string sSeperator = " / ";
        private static string sSeperator2 = " - ";

        public int ID { get; set; }
        public int BaseUnitID { get; set; }
        public string Name { get; set; }
        public string RaceClassName { get; set; }
        public string Image { get; set; }
        public Alignment Alignment { get; set; }
        public string Level { get; set; }
        public int MaxLevel { get; set; }
        public int XP { get; set; }
        public int NextLevelXP { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public int MaxMana { get; set; }
        public int Mana { get; set; }
        public string Damage { get; set; }
        public int Speed { get; set; }
        public int AttackRange { get; set; }
        public int MagicRange { get; set; }
        public int Resistance { get; set; }
        public int MagicResistance { get; set; }
        public int MagicPower { get; set; }
        public int MagicDamage { get; set; }
        public bool Takes2Slots { get; set; }
        public string Targets { get; set; }
        public int Line { get; set; }
        public List<ActionButtonViewModel> Buttons { get; set; }
        public List<SmallSpecialViewModel> Specials { get; set; }
        public UnitViewModel()
        {
        }
        public UnitViewModel(Unit unit, bool isBuy, bool canBuy, int buyPriceOrPowerReq, string pColor = "", Dictionary<int, TDBSpecial> specs = null)
        {
            this.ID = unit.ID;
            this.Name = unit.Name;
            this.RaceClassName = unit.RaceClassName;
            this.Image = unit.Image;
            this.Alignment = unit.Alignment;
            this.XP = unit.XP;
            this.NextLevelXP = unit.NextLevelXP;
            this.MaxLevel = unit.MaxLevel;

            if (unit.Level == unit.MaxLevel)
            {
                this.Level = "";
            } 
            else
            {
                this.Level = unit.Level.ToString() + sSeperator + unit.MaxLevel.ToString();
            }
            
            this.MaxHP = unit.MaxHP;
            this.HP = unit.HP;
            this.MaxMana = unit.MaxMana;
            this.Mana = unit.Mana;

            this.Attack = Calc.Round(unit.Attack, -1);
            this.Defence = Calc.Round(unit.Defence, -1);
            this.Speed = Calc.Round(unit.Speed, -1);
            this.Resistance = Calc.Round(unit.Resistance, -1);
            this.MagicResistance = Calc.Round(unit.MagicResistance, -1);
            this.MagicPower = Calc.Round(unit.MagicPower, -1);
            this.MagicDamage = this.MagicPower;

            this.Damage = Calc.Round((unit.DamageBase + 1), -1).ToString() + sSeperator2 + Calc.Round((unit.DamageBase + unit.DamageRoll), -1).ToString();
            this.AttackRange = unit.AttackRange;
            this.MagicRange = unit.MagicRange;
            this.Takes2Slots = unit.Takes2Slots;
            this.Line = unit.Line;

            string strTargets = Calc.Round(unit.NrOfTargets, -1).ToString() + ", ";
            switch (Calc.Round(unit.NrOfAttacks, -1))
            {
                case 2: { strTargets += "twice"; break; }
                case 3: { strTargets += "three times"; break; }
                case 4: { strTargets += "four times"; break; }
                case 5: { strTargets += "five times"; break; }
                default: { strTargets += "once"; break; }
            }
            this.Targets = strTargets;

            this.Buttons = new List<ActionButtonViewModel>();
            if (isBuy && canBuy && pColor != "")
            {
                this.Buttons.Add(new ActionButtonViewModel("Buy (" + buyPriceOrPowerReq + " Gold)", pColor, ButtonAction.Buy, EntityType.Unit, this.ID));
            }

            if (specs != null)
            {
                this.Specials = new List<SmallSpecialViewModel>();
                foreach (Special spec in unit.Specials)
                {
                    this.Specials.Add(new SmallSpecialViewModel(specs[spec.BaseID], false, false, spec.Count));
                }

            }
        }

        public string HPString()
        {
            return this.HP.ToString() + sSeperator + this.MaxHP.ToString();
        }
        public string ManaString()
        {
            return this.Mana.ToString() + sSeperator + this.MaxMana.ToString();
        }
        public string XPString()
        {
            return this.XP.ToString() + sSeperator + this.NextLevelXP.ToString();
        }
        public int ManaBar(int iMaxWidth)
        {
            return Calc.Round(iMaxWidth * (this.Mana / this.MaxMana), -1);
        }
        public int HPBar(int iMaxWidth)
        {
            return Calc.Round(iMaxWidth * (this.HP / this.MaxHP), -1);
        }
        public int XPBar(int iMaxWidth)
        {
            return 70;
            //if (this.Level == "") { return iMaxWidth; }
            //return Calc.Round(iMaxWidth * (this.XP / this.NextLevelXP), -1);
        }
    }
}