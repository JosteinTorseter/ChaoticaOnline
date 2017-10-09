using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.lib
{
    public class Statics
    {
        public static double GoldGainIndex = 1.0;
        public static int MaxDwellingsPerTile = 3;
        public static int Dwelling1Chance = 100;
        public static int Dwelling2Chance = 50;
        public static int Dwelling3Chance = 15;
        public static int MaxDungeonsPerTile = 3;
        public static int Dungeon1Chance = 90;
        public static int Dungeon2Chance = 30;
        public static int Dungeon3Chance = 10;
        public static int HeroMaxLevel = 15;
        public static double BaseSellPercent = 0.3;
        public static int BaseBuyPercent = 275;

        public static int DrawChance_Army = 100;
        public static int DrawChance_Reward = 10;
        public static int DrawChance_Unit = 5;
        public static int DrawChance_Object = 20;
        public static int DrawChance_Dwelling = 105;
        public static int DrawChance_Dungeon = 105;

        public static int DrawReduction_Army = 8;
        public static int DrawReduction_Reward = 2;
        public static int DrawReduction_Unit = 1;
        public static int DrawReduction_Object = 2;
        public static int DrawReduction_Dwelling = 50;
        public static int DrawReduction_Dungeon = 50;

        public static int DefaultMaxPartyUnits = 10;
        public static int DefaultMinPartyUnits = 4;

        public static double DiffIncreaseOn2SlotUnit = 1.6;
        public static double RewardUnitDifficultyMultiplier = 3;

        public static int Diff_MinAt1 = 10;
        public static int Diff_MaxAt1 = 25;
        public static int Diff_MinAt2 = 30;
        public static int Diff_MaxAt2 = 60;
        public static int Diff_MinAt3 = 50;
        public static int Diff_MaxAt3 = 100;
        public static int Diff_MinAt4 = 100;
        public static int Diff_MaxAt4 = 200;
        public static int Diff_MinAt5 = 200;
        public static int Diff_MaxAt5 = 300;
        public static int Diff_MinAt6 = 300;
        public static int Diff_MaxAt6 = 400;
        public static int Diff_MinAt7 = 375;
        public static int Diff_MaxAt7 = 500;


        public static int CardsPerTile = 7;

        public static string AlignmentColor(Alignment a)
        {
            string res = "#595959";
            switch (a)
            {
                case Alignment.Evil:
                    {
                        res = "#0d0d0d";
                        break;
                    }
                case Alignment.Good:
                    {
                        res = "#F1F1F1";
                        break;
                    }
            }
            return res;
        }
        public static string AlignmentInverseColor(Alignment a)
        {
            string res = "black";
            switch (a)
            {
                case Alignment.Evil:
                    {
                        res = "white";
                        break;
                    }
                case Alignment.Good:
                    {
                        res = "black";
                        break;
                    }
            }
            return res;
        }
        public static int GetDifficulty(int baseDiff, Calc calc = null)
        {
            if (calc == null) { calc = new Calc(); }
            int iMax = 0;
            int iMin = 0;
            switch (baseDiff)
            {
                case 1: { iMin = Diff_MinAt1; iMax = Diff_MaxAt1; break; }
                case 2: { iMin = Diff_MinAt2; iMax = Diff_MaxAt2; break; }
                case 3: { iMin = Diff_MinAt3; iMax = Diff_MaxAt3; break; }
                case 4: { iMin = Diff_MinAt4; iMax = Diff_MaxAt4; break; }
                case 5: { iMin = Diff_MinAt5; iMax = Diff_MaxAt5; break; }
                case 6: { iMin = Diff_MinAt6; iMax = Diff_MaxAt6; break; }
                case 7: { iMin = Diff_MinAt7; iMax = Diff_MaxAt7; break; }
            }
            return calc.GetRandom(iMin, iMax);
        }

        public static string RarityColor(int rarity, bool isUnique)
        {
            if (isUnique) { return "#FFBB00"; }
            if (rarity > 75)
            {
                return "#595959";
            } else if (rarity > 49)
            {
                return "#3F6813";
            } else if (rarity > 29)
            {
                return "#375E97";
            } else
            {
                return "#800080";
            }
        }
        public static string InverseRarityColor(int rarity, bool isUnique)
        {
            return "black";
        }
    }
}