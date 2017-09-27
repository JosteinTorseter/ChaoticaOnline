using ChaoticaOnline.DAL;
using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.TemplateModels;
using ChaoticaOnline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.GameModels
{
    public class UnitFactory
    {
        public static Unit CreateUnit(TemplateContext dbT, int iUnit, int iLevel)
        {
            TDBUnit u = dbT.TDBUnits.Find(iUnit);
            return CreateUnit(u, dbT.TDBClasses.Find(u.ClassID), dbT.TDBRaces.Find(u.RaceID), iLevel);
        }
        public static Unit CreateUnit(TDBUnit u, TDBClass c, TDBRace r, int iLevel)
        {
            Unit unit = new Unit(u, r, c);
            unit.LevelUp(u.LevelUpBonuses(c, r), u.Level2XP, iLevel);
            return unit;
        }
        public static int ActualUnitCount(List<Unit> lstUnits)
        {
            int res = 0;
            foreach (Unit u in lstUnits)
            {
                res += 1;
                if (u.Takes2Slots) { res += 1; }
            }
            return res;
        }
        public static void RearrangeUnits(List<Unit> lstUnits)
        {
            int iIndex = 0;
            foreach (Unit u in lstUnits)
            {
                u.PartyListIndex = iIndex;
                iIndex++;
            }
        }

        public static List<SmallUnitViewModel> GetViewUnitsFromArray(List<int> ids, TemplateContext dbT)
        {
            List<SmallUnitViewModel> res = new List<SmallUnitViewModel>();
            List<TDBUnit> baseUnits = dbT.TDBUnits.Where(u => ids.Contains(u.ID)).ToList();
            foreach (int i in ids)
            {
                foreach (TDBUnit bu in baseUnits)
                {
                    if (bu.ID == i) { res.Add(new SmallUnitViewModel(bu, true, bu.GoldValue)); }
                }
            }
            return res;
        }
        public static List<SmallSpecialViewModel> GetViewSpecsFromArray(List<int> ids, TemplateContext dbT)
        {
            List<SmallSpecialViewModel> res = new List<SmallSpecialViewModel>();
            List<TDBSpecial> baseSpecs = dbT.TDBSpecials.Where(u => ids.Contains(u.ID)).ToList();
            foreach (int i in ids)
            {
                foreach (TDBSpecial bs in baseSpecs)
                {
                    if (bs.ID == i) { res.Add(new SmallSpecialViewModel(bs, true, true, bs.GoldValue)); }
                }
            }
            return res;
        }
        public static List<SmallSpecialViewModel> GetViewSpecsFromArray(List<KeyValuePair<int, int>> ids, TemplateContext dbT)
        {
            List<SmallSpecialViewModel> res = new List<SmallSpecialViewModel>();
            List<int> idTrim = new List<int>();
            foreach (KeyValuePair<int, int> kv in ids)
            {
                idTrim.Add(kv.Key);
            }
            List<TDBSpecial> baseSpecs = dbT.TDBSpecials.Where(u => idTrim.Contains(u.ID)).ToList();
            foreach (KeyValuePair<int, int> kv in ids)
            {
                foreach (TDBSpecial bs in baseSpecs)
                {
                    if (bs.ID == kv.Key) { res.Add(new SmallSpecialViewModel(bs, true, true, bs.GoldValue)); }
                }
            }
            return res;
        }
        public static List<SmallWorldItemViewModel> GetViewItemsFromArray(List<KeyValuePair<int, int>> ids, TemplateContext dbT)
        {
            List<SmallWorldItemViewModel> res = new List<SmallWorldItemViewModel>();
            List<int> idTrim = new List<int>();
            foreach (KeyValuePair<int, int> kv in ids)
            {
                idTrim.Add(kv.Key);
            }
            List<TDBWorldItem> baseItems = dbT.TDBWorldItems.Where(u => idTrim.Contains(u.ID)).ToList();
            foreach (KeyValuePair<int, int> kv in ids)
            {
                foreach (TDBWorldItem bi in baseItems)
                {
                    if (bi.ID == kv.Key) { res.Add(new SmallWorldItemViewModel(bi, true, true, bi.GoldValue)); }
                }
            }
            return res;
        }
    }
}