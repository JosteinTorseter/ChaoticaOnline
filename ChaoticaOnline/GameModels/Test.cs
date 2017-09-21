using ChaoticaOnline.DAL;
using ChaoticaOnline.GameDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.GameModels
{
    public class Test
    {
        public static Party GenerateTestParty(int PartyIndex, Player p, TemplateContext dbT)
        {
            Party pRes = new Party();
            pRes.PlayerIndex = p.PlayerNumber;
            pRes.Ident = PartyIndex;

            Unit u = null;

            if (PartyIndex == 0)
            {
                u = UnitFactory.CreateUnit(dbT, 1, 1);
                u.PartyListIndex = 0;
                pRes.Units.Add(u);

                u = UnitFactory.CreateUnit(dbT, 4, 1);
                u.PartyListIndex = 0;
                pRes.Units.Add(u);

                u = UnitFactory.CreateUnit(dbT, 47, 1);
                u.PartyListIndex = 0;
                pRes.Units.Add(u);

                u = UnitFactory.CreateUnit(dbT, 13, 1);
                u.PartyListIndex = 0;
                pRes.Units.Add(u);

                u = UnitFactory.CreateUnit(dbT, 31, 1);
                u.PartyListIndex = 0;
                pRes.Units.Add(u);

                u = UnitFactory.CreateUnit(dbT, 42, 1);
                u.PartyListIndex = 0;
                pRes.Units.Add(u);
            } else
            {
                for (int i = 1; i < 33; i++)
                {
                    u = UnitFactory.CreateUnit(dbT, i, 1);
                    u.PartyListIndex = i - 1;
                    pRes.Units.Add(u);
                }
            }
            return pRes;

        }
    }
}