using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.GameModels;
using ChaoticaOnline.lib;
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
        public string Name { get; set; }
        public string RaceClassName { get; set; }
        public string Image { get; set; }
        public Alignment Alignment { get; set; }
        public string Level { get; set; }
        public string XP { get; set; }
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
        public int Line { get; set; }
        public UnitViewModel()
        {
        }
        public UnitViewModel(Unit unit)
        {
            this.ID = unit.ID;
            this.Name = unit.Name;
            this.RaceClassName = unit.RaceClassName;
            this.Image = unit.Image;
            this.Alignment = unit.Alignment;

            if (unit.Level == unit.MaxLevel)
            {
                this.Level = "";
                this.XP = "";
            } 
            else
            {
                this.Level = unit.Level.ToString() + sSeperator + unit.MaxLevel.ToString();
                this.XP = unit.XP + sSeperator + unit.NextLevelXP;
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
        }
    }
}