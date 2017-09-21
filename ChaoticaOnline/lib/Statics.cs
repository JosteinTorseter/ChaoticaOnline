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
    }
}