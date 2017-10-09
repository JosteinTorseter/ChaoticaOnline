using ChaoticaOnline.DAL;
using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.lib;
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
        public static Party CreateParty(TemplateContext dbT, int raceID, int difficulty, Calc calc = null, 
                int leaderID = 0, int leaderLevel = 0, string name = "", 
                Alignment align = Alignment.Inherited, 
                int maxUnits = 0, List<int> lstUnitTypes = null)
        {
            Party res = new Party();
            if (calc == null) { calc = new Calc(); }
            if (maxUnits == 0) { maxUnits = Statics.DefaultMaxPartyUnits; }
            int nrOfUnits = calc.GetRandom(Statics.DefaultMinPartyUnits, maxUnits);
            List<TDBUnit> lst = null;
            if (lstUnitTypes == null)
            {
                lst = dbT.TDBUnits.Where(u => (u.RaceID == raceID && u.CanRandomSpawn) || u.ID == leaderID).ToList();
            } else
            {
                lst = dbT.TDBUnits.Where(u => lstUnitTypes.Contains(u.ID)).ToList();
            }
            Unit unit = null;
            int iIndex = 0;
            if (leaderID > 0)
            {
                unit = CreateUnit(dbT, leaderID, leaderLevel, dbT.TDBUnits.Find(leaderID), align);
                unit.PartyListIndex = iIndex;
                iIndex++;
                res.Units.Add(unit);
                nrOfUnits--;
                if (unit.Takes2Slots) { nrOfUnits--; }
            }

            int diffPerUnit = Calc.Round(difficulty / nrOfUnits, 1);
            int randomness = Calc.Round(diffPerUnit / 3, 1);
            int iMinUnitDiff = lst.OrderBy(u => u.BaseDifficulty).First().BaseDifficulty;
            if (diffPerUnit < iMinUnitDiff) { diffPerUnit = iMinUnitDiff; }
            int iDiffForThis = 0;
            int iDiffUsed = 0;

            int iHTHUnitsAdded = 2;
            Choice oCh = new Choice();
            //foreach (TDBUnit baseUnit in lst.Where(u => u.ISHTHUNIT))
            while (difficulty > 0)
            {
                if (iHTHUnitsAdded < 2)
                {
                    // Add HTH unit
                    iHTHUnitsAdded++;
                }
                if (iHTHUnitsAdded == 2)
                {
                    // Add only non HTH
                    foreach (TDBUnit baseUnit in lst)
                    {
                        oCh.Add(baseUnit.ID, 1);
                    }
                    iHTHUnitsAdded++;
                }

                iDiffForThis = calc.GetRandom(diffPerUnit - randomness, diffPerUnit + randomness);
                int iUnitID = oCh.Make(calc).ID;
                TDBUnit chosenUnit = lst.Find(u => u.ID == iUnitID);
                if (chosenUnit.Takes2Slots)
                {
                    if (UnitFactory.ActualUnitCount(res.Units.ToList()) == 9) { break; }
                    iDiffForThis = Calc.Round(iDiffForThis * Statics.DiffIncreaseOn2SlotUnit, 1);
                }
                iDiffForThis -= chosenUnit.BaseDifficulty;
                iDiffUsed += chosenUnit.BaseDifficulty;
                int iLevel = 1;
                while (iDiffForThis > 0)
                {
                    if (iLevel == chosenUnit.MaxLevel) { break; }
                    iLevel += 1;
                    iDiffUsed += chosenUnit.DifficultyPrLvl;
                    iDiffForThis -= chosenUnit.DifficultyPrLvl;
                }

                unit = CreateUnit(dbT, iUnitID, iLevel, chosenUnit, align);
                unit.PartyListIndex = iIndex;
                iIndex++;
                res.Units.Add(unit);
                nrOfUnits--;
                if (unit.Takes2Slots) { nrOfUnits--; }
                difficulty -= iDiffUsed;
                if (UnitFactory.ActualUnitCount(res.Units.ToList()) >= 10) { break; }
            }
            res.Alignment = res.Units.ElementAt(0).Alignment;
            res.Image = UnitFactory.GetBestUnit(res.Units.ToList()).Image;
            res.Name = name;
            if (res.Name == "")
            {
                res.Name = "A party of " + dbT.TDBRaces.Find(raceID).Plural;
            }
            return res;
        }

        public static Unit CreateRandomUnit(TemplateContext dbT, int iMaxBaseDiff = 0, int iRaceID = 0, 
                int iClassID = 0, int iLevel = 1,
                Alignment overrideAlign = Alignment.Inherited, Calc calc = null)
        {
            List<TDBUnit> lstPotential = null;
            if (calc == null) { calc = new Calc(); }
            if (iRaceID == 0 && iClassID == 0)
            {
                lstPotential = dbT.TDBUnits.Where(u => u.CanRandomSpawn == true).ToList();
            } else if (iRaceID == 0)
            {
                lstPotential = dbT.TDBUnits.Where(u => u.CanRandomSpawn == true && u.ClassID == iClassID).ToList();
            } else if (iClassID == 0)
            {
                lstPotential = dbT.TDBUnits.Where(u => u.CanRandomSpawn == true && u.RaceID == iRaceID).ToList();
            } else
            {
                lstPotential = dbT.TDBUnits.Where(u => u.CanRandomSpawn == true && u.RaceID == iRaceID && u.ClassID == iClassID).ToList();
            }
            Choice choice = new Choice();
            foreach (TDBUnit u in lstPotential)
            {
                if (iMaxBaseDiff == 0 || u.BaseDifficulty <= iMaxBaseDiff)
                {
                    choice.Add(u.ID, 1);
                }
            }
            int iRes = choice.Make(calc).ID;
            return CreateUnit(dbT, iRes, iLevel, lstPotential.Find(u => u.ID == iRes), overrideAlign);
        }

        public static Unit CreateUnit(TemplateContext dbT, int iUnit, int iLevel, 
            TDBUnit unit = null, Alignment overrideAlign = Alignment.Inherited)
        {
            if (unit == null) { unit = dbT.TDBUnits.Find(iUnit); }
            Unit res = CreateUnit(unit, dbT.TDBClasses.Find(unit.ClassID), dbT.TDBRaces.Find(unit.RaceID), iLevel);
            if (overrideAlign != Alignment.Inherited) { res.Alignment = overrideAlign; }
            return CreateUnit(unit, dbT.TDBClasses.Find(unit.ClassID), dbT.TDBRaces.Find(unit.RaceID), iLevel);
        }
        public static Unit CreateUnit(TDBUnit u, TDBClass c, TDBRace r, int iLevel, Alignment overrideAlign = Alignment.Inherited)
        {
            Unit unit = new Unit(u, r, c, 0, "", overrideAlign);
            unit.LevelUp(u.LevelUpBonuses(c, r), u.Level2XP, u.DifficultyPrLvl, iLevel);
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
        public static Unit GetBestUnit(List<Unit> lstUnits)
        {
            Unit res = null;
            int max = 0;
            foreach (Unit u in lstUnits)
            {
                if (u.Difficulty > max)
                {
                    max = u.Difficulty;
                    res = u;
                }
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