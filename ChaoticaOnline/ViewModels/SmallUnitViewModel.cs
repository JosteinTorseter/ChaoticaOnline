using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.lib;
using ChaoticaOnline.TemplateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.ViewModels
{
    public class SmallUnitViewModel
    {
        public int ID { get; set; }
        public int BaseUnitID { get; set; }
        public string Tooltip { get; set; }
        public string Image { get; set; }
        public string LevelImage { get; set; }
        public string LevelTooltip { get; set; }
        public string ItemImage { get; set; }
        public int HPPercent { get; set; }
        public int ManaPercent { get; set; }
        public bool Takes2Slots { get; set; }
        public int GoldValue { get; set; }

        public SmallUnitViewModel()
        {
        }
        public SmallUnitViewModel(TDBUnit baseUnit, bool canBuy, int buyPriceOrPowerReq, bool isLeader = false, int iLevel = 1)
        {
            this.BaseUnitID = baseUnit.ID;
            this.Image = baseUnit.Image;
            this.Takes2Slots = baseUnit.Takes2Slots;
            this.GoldValue = baseUnit.GoldValue;
            if (canBuy) { this.Tooltip = baseUnit.Name + " - " + buyPriceOrPowerReq.ToString() + " gold"; }
            else if (isLeader) { this.Tooltip = baseUnit.Name + " - Level " + iLevel; }
            else { this.Tooltip = baseUnit.Name + " - at " + buyPriceOrPowerReq.ToString(); }
            if (baseUnit.MaxLevel > 5 && isLeader) {
                LevelImage = "~/Data/lvl10.png";
                int iMaxLvl = 10;
                if (!isLeader) { iMaxLvl = baseUnit.MaxLevel; }
                LevelTooltip = "This unit can become level " + iMaxLvl.ToString();
            }
        }
        public SmallUnitViewModel(Unit unit)
        {
            this.ID = unit.ID;
            this.BaseUnitID = unit.BaseUnitID;
            this.Image = unit.Image;
            this.Takes2Slots = unit.Takes2Slots;
            this.Tooltip = unit.Name + " Level " + unit.Level;
            if (unit.MaxLevel > 5) { LevelImage = "~/Data/lvl10.png"; }
            this.HPPercent = 70; //Calc.Round((unit.HP / unit.MaxHP) * 100, -1);
            this.ManaPercent = 100; //Calc.Round((unit.Mana / unit.MaxMana) * 100, -1);
        }

        public int ManaBar(int iMaxWidth)
        {
            return Calc.Round(iMaxWidth * (this.ManaPercent / 100), -1);
        }
        public int HPBar(int iMaxWidth)
        {
            return Calc.Round(iMaxWidth * (this.HPPercent / 100), -1);
        }
    }
}