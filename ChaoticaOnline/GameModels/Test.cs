using ChaoticaOnline.DAL;
using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.TemplateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.GameModels
{
    public class Test
    {
        public static void GenerateInventory(Player p, Dictionary<int, TDBWorldItem> baseItems)
        {
            //p.WorldItems.Add(new WorldItem(baseItems[1], 1, true));
            //p.WorldItems.Add(new WorldItem(baseItems[2], 1, true));
            //p.WorldItems.Add(new WorldItem(baseItems[4], 1, true));
            //p.WorldItems.Add(new WorldItem(baseItems[8], 1, true));
            //p.WorldItems.Add(new WorldItem(baseItems[16], 1, true));
            //p.WorldItems.Add(new WorldItem(baseItems[19], 1, true));
            //p.WorldItems.Add(new WorldItem(baseItems[24], 1, true));
            //p.WorldItems.Add(new WorldItem(baseItems[95], 1, true));
            //p.WorldItems.Add(new WorldItem(baseItems[150], 1, true));
            //p.WorldItems.Add(new WorldItem(baseItems[161], 1, true));
            //p.WorldItems.Add(new WorldItem(baseItems[158], 1, true));
            //p.WorldItems.Add(new WorldItem(baseItems[110], 1, true));
            Dictionary<int, int> res = new Dictionary<int, int>();
            for (int i = 13; i < 44; i++)
            {
                p.WorldItems.Add(new WorldItem(baseItems[i], 1, false));
            }
        }

        public static Party GenerateTestParty(int PartyIndex, Player p, TemplateContext dbT)
        {
            Party pRes = new Party();
            pRes.PlayerIndex = p.PlayerNumber;
            pRes.Ident = PartyIndex;

            Unit u = null;

            if (PartyIndex == 0)
            {
                u = UnitFactory.CreateUnit(dbT, 4, 1);
                u.PartyListIndex = 0;
                pRes.Units.Add(u);

                u = UnitFactory.CreateUnit(dbT, 47, 1);
                u.PartyListIndex = 1;
                pRes.Units.Add(u);

                u = UnitFactory.CreateUnit(dbT, 13, 1);
                u.PartyListIndex = 2;
                pRes.Units.Add(u);

                u = UnitFactory.CreateUnit(dbT, 31, 1);
                u.PartyListIndex = 3;
                pRes.Units.Add(u);

                u = UnitFactory.CreateUnit(dbT, 42, 1);
                u.PartyListIndex = 4;
                pRes.Units.Add(u);
            } else
            {
                for (int i = 50; i < 77; i++)
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