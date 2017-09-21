﻿using ChaoticaOnline.DAL;
using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.TemplateModels;
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

    }
}