using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.GameDBModels
{
    public class Special
    {
        public int BaseID { get; set; }
        public int Count { get; set; }
        public int Used { get; set; }
        public Special ()
        {
        }
        public Special(string input)
        {
            string[] s = input.Split(':');
            this.BaseID = Int32.Parse(s[0]);
            this.Count = Int32.Parse(s[1]);
            if (s.Length > 2) { this.Used = Int32.Parse(s[2]); }
        }
        public Special(int iBase, int iCount)
        {
            this.BaseID = iBase;
            this.Count = iCount;
        }
        public override string ToString()
        {
            return this.BaseID.ToString() + ":" + this.Count.ToString() + ":" + this.Used.ToString();
        }
        public static string ListToString(List<Special> input)
        {
            string res = "";
            foreach (Special spec in input)
            {
                if (res != "") { res += "#"; }
                res += spec.ToString();
            }
            return res;
        }
        public static List<Special> ListFromString(string input)
        {
            List<Special> res = new List<Special>();
            if (String.IsNullOrEmpty(input)) { return res; }
            foreach (string sFull in input.Split('#'))
            {
                res.Add(new Special(sFull));
            }
            return res;
        }
    }
}