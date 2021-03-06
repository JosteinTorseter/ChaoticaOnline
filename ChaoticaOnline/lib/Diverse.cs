﻿using ChaoticaOnline.GameDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.lib
{
    public class Calc
    {
        public System.Random Generator = new System.Random();

        public int GetRandom(int iMin, int iMax)
        {
            return Generator.Next(iMin, iMax + 1);
        }

        public bool CoinToss()
        {
            int i = Generator.Next(0, 2);
            if (i == 0) { return false; }
            return true;
        }

        public static int Div(int a, int b)
        {
            return (int)Math.Round(Convert.ToDouble(a / b), 0);
        }
        public bool PercentChance(int iChance)
        {
            if (this.GetRandom(1, 100) <= iChance) { return true; }
            return false;
        }
        public static int Round(double dbl, int iRoundType = 0)
        {
            if (iRoundType == 1) { return (int)Math.Ceiling(dbl); }
            else if (iRoundType == -1) { return (int)Math.Floor(dbl); }
            else { return (int)Math.Round(dbl, 0); }
        }
    }

    public class Alternative
    {
        public int ID;
        public int Chance;
        public Alternative()
        {
        }
        public Alternative(int iID, int iChance)
        {
            ID = iID;
            Chance = iChance;
        }
    }

    public class DictionaryHack
    {
        public static Dictionary<int, KeyValuePair<int, int>> GetKVsByString(string input)
        {
            Dictionary<int, KeyValuePair<int, int>> res = new Dictionary<int, KeyValuePair<int, int>>();
            if (String.IsNullOrEmpty(input)) { return res; }
            foreach (string sFull in input.Split('#'))
            {
                string[] s = sFull.Split(':');
                string[] s2 = s[1].Split('-');
                KeyValuePair<int, int> kv = new KeyValuePair<int, int>(Int32.Parse(s2[0]), Int32.Parse(s2[1]));
                res.Add(Int32.Parse(s[0]), kv);
            }
            return res;
        }
        public static List<Bonus> GetBonusesByString(string input)
        {
            List<Bonus> res = new List<Bonus>();
            if (String.IsNullOrEmpty(input)) { return res; }
            foreach (string sFull in input.Split('#'))
            {
                string[] s = sFull.Split(':');
                int iLast = 0;
                if (s.Length > 2) { iLast = Int32.Parse(s[2]); }
                res.Add(new Bonus((BonusType)Int32.Parse(s[0]), Int32.Parse(s[1]), iLast));
            }
            return res;
        }
        public static Dictionary<int, int> GetIntsByString(string input)
        {
            Dictionary<int, int> res = new Dictionary<int, int>();
            if (String.IsNullOrEmpty(input)) { return res; }
            foreach (string sFull in input.Split('#'))
            {
                string[] s = sFull.Split(':');
                res.Add(Int32.Parse(s[0]), Int32.Parse(s[1]));
            }
            return res;
        }
        public static List<KeyValuePair<int, int>> GetIntListByString(string input)
        {
            List<KeyValuePair<int, int>> res = new List<KeyValuePair<int, int>>();
            if (String.IsNullOrEmpty(input)) { return res; }
            foreach (string sFull in input.Split('#'))
            {
                string[] s = sFull.Split(':');
                res.Add(new KeyValuePair<int, int>(Int32.Parse(s[0]), Int32.Parse(s[1])));
            }
            return res;
        }
        public static List<Effect> GetEffectsByString(string input)
        {
            if (String.IsNullOrEmpty(input)) { return new List<Effect>(); }
            List<Effect> res = new List<Effect>();
            foreach (string sFull in input.Split('#'))
            {
                Effect e = new Effect();
                string[] sPart = sFull.Split('@');
                string[] sTime = sPart[0].Split(':');
                e.Category = (EffectCategory)Int32.Parse(sTime[0]);
                if (sTime.Length > 1) {
                    string[] sDur = sTime[1].Split('?');
                    e.Duration = Int32.Parse(sDur[0]);
                    if (sDur.Length > 1)
                    {
                        e.DurationPerPower = Int32.Parse(sDur[1]);
                    }
                }
                
                foreach (string s in sPart[1].Split(','))
                {
                    Bonus b = new Bonus();
                    string[] sBon = s.Split(':');
                    b.BonusType = (BonusType)Int32.Parse(sBon[0]);
                    string[] sVal = sBon[1].Split('?');
                    b.Value = Int32.Parse(sVal[0]);
                    if (sVal.Length > 1)
                    {
                        b.Trigger = Int32.Parse(sVal[1]);
                    }
                    e.Bonuses.Add(b);
                }
                res.Add(e);
            }
            return res;
        }
        public static string GetStringByInts(Dictionary<int, int> input)
        {
            string res = "";
            if (input == null) { return res; }
            foreach (int key in input.Keys)
            {
                if (res != "")
                {
                    res += "#";
                }
                res += key.ToString() + ":" + input[key].ToString();
            }
            return res;
        }
        public static string GetStringByIntList(List<KeyValuePair<int, int>> input)
        {
            string res = "";
            if (input == null) { return res; }
            foreach (KeyValuePair<int, int> kv in input)
            {
                if (res != "")
                {
                    res += "#";
                }
                res += kv.Key.ToString() + ":" + kv.Value.ToString();
            }
            return res;
        }
        public static string GetStringByKVs(Dictionary<int, KeyValuePair<int, int>> input)
        {
            string res = "";
            if (input == null) { return res; }
            foreach (int key in input.Keys)
            {
                if (res != "")
                {
                    res += "#";
                }
                res += key.ToString() + ":" + input[key].Key.ToString() + "-" + input[key].Value.ToString();
            }
            return res;
        }
    }

    public class Choice
    {
        public List<Alternative> Alternatives { get; set; } = new List<Alternative>();
        public void Add(int iID, int iChance)
        {
            this.Alternatives.Add(new Alternative(iID, iChance));
        }
        public void Remove(int iID)
        {
            Alternative alt = this.GetAlternative(iID);
            if (alt != null) { this.Alternatives.Remove(alt); }
        }
        public void Clear()
        {
            Alternatives = new List<Alternative>();
        }
        public Alternative GetAlternative(int iID)
        {
            foreach (Alternative alt in this.Alternatives)
            {
                if (alt.ID == iID)
                {
                    return alt;
                }
            }
            return null;
        }
        public Alternative Make(Calc calc = null)
        {
            int iMax = 0;
            int iRoll = 0;
            int iTotal = 0;
            Alternative resAlt = null;
            if (Alternatives.Count == 0) { return null; }
            if (Alternatives.Count == 1) { return this.Alternatives[0]; }
            if (calc == null) { calc = new Calc(); }
            foreach (Alternative alt in this.Alternatives)
            {
                iMax += alt.Chance;
            }
            iRoll = calc.GetRandom(1, iMax);
            foreach (Alternative alt in this.Alternatives)
            {
                if ((alt.Chance + iTotal) >= iRoll)
                {
                    resAlt = alt;
                    break;
                }
                iTotal += alt.Chance;
            }
            return resAlt;
        }
    }

}