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

        public static int DrawChance_Army = 10;
        public static int DrawChance_Reward = 10;
        public static int DrawChance_Unit = 10;
        public static int DrawChance_Object = 10;
        public static int DrawChance_Dwelling = 10;
        public static int DrawChance_Dungeon = 10;

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