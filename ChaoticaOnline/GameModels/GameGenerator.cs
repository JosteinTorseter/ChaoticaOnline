using ChaoticaOnline.DAL;
using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.lib;
using ChaoticaOnline.TemplateModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.GameModels
{
    public class GameGenerator
    {

        public static Game GenerateGame(Calc calc = null, TemplateContext dbT = null, GameContext dbG = null)
        {
            Game game = new Game();
            if (calc == null) { calc = new Calc(); }
            if (dbT == null) { dbT = new TemplateContext(); }
            if (dbG == null) { dbG = new GameContext(); }

            Dictionary<int, int> TilesPerDifficulty = new Dictionary<int, int>();
            
            TilesPerDifficulty.Add(1, 11);
            TilesPerDifficulty.Add(2, 14);
            TilesPerDifficulty.Add(3, 14);
            TilesPerDifficulty.Add(4, 9);
            TilesPerDifficulty.Add(5, 5);
            TilesPerDifficulty.Add(6, 3);
            game.Map = GenerateMap(10, 10, true, TilesPerDifficulty, calc);

            dbG.Games.Add(game);
            dbG.SaveChanges();

            List<TDBTerrain> lstTerrain = dbT.TDBTerrain.ToList();
            FillTerrain(game.Map, lstTerrain, calc);
            GeneratePlayers(game, 3, calc);
            
            dbG.Entry(game).State = EntityState.Modified;
            dbG.SaveChanges();

            List<Tile> lst = new List<Tile>();
            Dictionary<int, TDBWorldItem> baseItems = new Dictionary<int, TDBWorldItem>();
            foreach (TDBWorldItem bit in dbT.TDBWorldItems)
            {
                baseItems.Add(bit.ID, bit);
            }
            foreach (Player p in game.Players)
            {
                game.Parties.Add(Test.GenerateTestParty(0, p, dbT));
                game.Parties.Add(Test.GenerateTestParty(1, p, dbT));
                p.UsedCommand = 32;
                Test.GenerateInventory(p, baseItems);
                lst.Add(PlacePlayerStartPosition(game.Map, p, lst, calc));
                SetInitialVisibleTiles(game.Map, p);
            }

            dbG.Entry(game).State = EntityState.Modified;
            dbG.SaveChanges();

            foreach (Party party in game.Parties)
            {
                Player player = game.Players.ElementAt(party.PlayerIndex);
                if (party.Ident == 0)
                {
                    player.PartyID = party.ID;
                } else
                {
                    player.RosterID = party.ID;
                }
            }

            FillDwellings(game.Map, dbT.TDBDwellings.ToList(), lstTerrain, calc);
            FillDungeons(game.Map, dbT.TDBDungeons.ToList(), lstTerrain, calc);

            dbG.Entry(game).State = EntityState.Modified;
            dbG.SaveChanges();

            return game;
        }

        private static void GeneratePlayers(Game game, int iCount, Calc calc)
        {
            for (int i = 0; i < iCount; i++)
            {
                Player p = new Player();
                p.Name = "Player " + (i + 1).ToString();
                p.CurrentGold = 200;
                p.MaxMovePoints = 10;
                p.MovePointsLeft = 7;
                p.PlayerNumber = i;
                p.Alignment = (Alignment)(calc.GetRandom(0, 2) - 1);
                p.HeroImage = "thedevil.png";
                p.Strength = 16;
                p.Dexterity = 15;
                p.Constitution = 17;
                p.Wisdom = 12;
                p.Intelligence = 11;
                p.Cunning = 13;
                switch (i)
                {
                    case 0:
                        {
                            p.Color = "336B87";
                            break;
                        }
                    case 1:
                        {
                            p.Color = "763626";
                            break;
                        }
                    case 2:
                        {
                            p.Color = "598234";
                            break;
                        }
                    case 3:
                        {
                            p.Color = "BA5536";
                            break;
                        }
                }
                game.Players.Add(p);
            }
        }

        private static void GetNextAlternativeList(List<Tile> Tiles, List<Tile> OrigTiles, ref Choice choice)
        {
            foreach (Tile t in OrigTiles)
            {
                int iNeighbours = TileList.GetSurroundingTiles(Tiles, t.XCoord, t.YCoord).Count;
                if (iNeighbours > 0)
                {
                    choice.Add(t.ListIndex, iNeighbours);
                }
            }
        }

        private static Tile PlacePlayerStartPosition(Map map, Player player, List<Tile> OtherPlayerPositions, Calc calc = null)
        {
            Choice choice = new Choice();
            if (calc == null) { calc = new Calc(); }
            foreach (Tile t in map.Tiles.Where(tl => tl.Difficulty == 1))
            {
                if (!OtherPlayerPositions.Contains(t))
                {
                    Alternative alt = new Alternative(t.ListIndex, 100);
                    int iNeighbours = TileList.GetSurroundingTiles(OtherPlayerPositions, t.XCoord, t.YCoord).Count;
                    switch (iNeighbours)
                    {
                        case 1:
                            {
                                alt.Chance = 10;
                                break;
                            }
                        case 2:
                            {
                                alt.Chance = 5;
                                break;
                            }
                        case 3:
                            {
                                alt.Chance = 1;
                                break;
                            }
                    }
                    choice.Alternatives.Add(alt);
                }
            }
            int iChosen = choice.Make(calc).ID;
            Tile res = map.Tiles.Where(t => t.ListIndex == iChosen).First();
            player.X = res.XCoord;
            player.Y = res.YCoord;
            player.StartTileID = res.ID;
            player.TileID = res.ID;

            List<int> lstPlayers = res.Players;
            lstPlayers.Add(player.ID);
            res.Players = lstPlayers;

            return res;
        }

        private static void FillTerrain(Map map, List<TDBTerrain> lstTerrain, Calc calc = null)
        {
            if (calc == null) { calc = new Calc(); }
            Choice choice = new Choice();
            for (int i = 1; i < 5; i++)
            {
                choice.Clear();
                foreach (TDBTerrain terr in lstTerrain.Where(terr => (terr.Difficulty == i && terr.TileType == 0)).ToList())
                {
                    choice.Add(terr.ID, 1);
                }
                foreach (Tile t in map.Tiles.Where(tl => tl.Difficulty == i).ToList())
                {
                    int iRes = choice.Make(calc).ID;
                    TDBTerrain terrain = lstTerrain.Find(terr => terr.ID == iRes);
                    t.SetTerrain(terrain);
                }
            }

            choice.Clear();
            choice.Add(25, 1);
            choice.Add(26, 1);
            foreach (Tile t in map.Tiles.Where(tl => tl.Difficulty == 6).ToList())
            {
                if (t.TerrainID == 0)
                {
                    int iRes = choice.Make(calc).ID;
                    TDBTerrain terrain = lstTerrain.Find(terr => terr.ID == iRes);
                    if (terrain == null)
                    {
                        terrain = null;
                    }
                    t.SetTerrain(terrain);
                    choice.Remove(t.TerrainID);
                }
                else
                {
                    TDBTerrain terrain = lstTerrain.Find(terr => terr.ID == t.TerrainID);
                    if (terrain == null)
                    {
                        terrain = null;
                    }
                    t.SetTerrain(terrain);
                }
            }

            choice.Clear();
            foreach (TDBTerrain terr in lstTerrain.Where(terr => terr.TileType == TileType.Special).ToList())
            {
                choice.Add(terr.ID, 1);
            }

            foreach (Tile t in map.Tiles.Where(tl => tl.Difficulty == 5).ToList())
            {
                int iRes = choice.Make(calc).ID;
                TDBTerrain terrain = lstTerrain.Find(terr => terr.ID == iRes);
                if (terrain == null)
                {
                    terrain = null;
                }
                t.SetTerrain(terrain);
                choice.Remove(t.TerrainID);
            }

        }

        private static void FillDwellings(Map map, List<TDBDwelling> lstDwellings, List<TDBTerrain> lstTerrain, Calc calc = null)
        {
            if (calc == null) { calc = new Calc(); }
            Choice choice = new Choice();
            foreach (Tile t in map.Tiles)
            {
                TDBTerrain terr = lstTerrain.Where(te => te.ID == t.TerrainID).First();
                choice.Clear();
                if (terr.SetDwelling > 0)
                {
                    t.Dwellings.Add(new Dwelling(lstDwellings.Where(d => d.ID == terr.SetDwelling).First()));
                }
                foreach (KeyValuePair<int, int> kv in terr.DwellingTypes)
                {
                    choice.Add(kv.Key, kv.Value);
                }
                for (int i = 0; i < Statics.MaxDwellingsPerTile; i++)
                {
                    if (t.Dwellings.Count <= i && choice.Alternatives.Count > 0)
                    {
                        int iChance = 0;
                        switch (i)
                        {
                            case 0:
                                {
                                    iChance = Statics.Dwelling1Chance;
                                    break;
                                }
                            case 1:
                                {
                                    iChance = Statics.Dwelling2Chance;
                                    break;
                                }
                            case 2:
                                {
                                    iChance = Statics.Dwelling3Chance;
                                    break;
                                }
                        }
                        if (calc.PercentChance(iChance))
                        {
                            int iChosen = choice.Make(calc).ID;
                            t.Dwellings.Add(new Dwelling(lstDwellings.Where(d => d.ID == iChosen).First()));
                            choice.Remove(iChosen);
                        }
                    }
                }
            }
        }

        private static void FillDungeons(Map map, List<TDBDungeon> lstDungeons, List<TDBTerrain> lstTerrain, Calc calc = null)
        {
            if (calc == null) { calc = new Calc(); }
            Choice choice = new Choice();
            foreach (Tile t in map.Tiles)
            {
                Dungeon du = null;
                TDBTerrain terr = lstTerrain.Where(te => te.ID == t.TerrainID).First();
                TDBDungeon baseDungeon = null;
                choice.Clear();
                if (terr.SetDungeon > 0)
                {
                    baseDungeon = lstDungeons.Where(d => d.ID == terr.SetDungeon).First();
                    du = new Dungeon(baseDungeon);
                    //GenerateDungeonTiles(du, baseDungeon, lstTerrain, calc);
                    t.Dungeons.Add(du);
                }
                foreach (KeyValuePair<int, int> kv in terr.DungeonTypes)
                {
                    choice.Add(kv.Key, kv.Value);
                }
                for (int i = 0; i < Statics.MaxDungeonsPerTile; i++)
                {
                    if (t.Dungeons.Count <= i && choice.Alternatives.Count > 0)
                    {
                        int iChance = 0;
                        switch (i)
                        {
                            case 0:
                                {
                                    iChance = Statics.Dungeon1Chance;
                                    break;
                                }
                            case 1:
                                {
                                    iChance = Statics.Dungeon2Chance;
                                    break;
                                }
                            case 2:
                                {
                                    iChance = Statics.Dungeon3Chance;
                                    break;
                                }
                        }
                        if (calc.PercentChance(iChance))
                        {
                            int iChosen = choice.Make(calc).ID;
                            baseDungeon = lstDungeons.Where(d => d.ID == iChosen).First();
                            du = new Dungeon(baseDungeon);
                            //GenerateDungeonTiles(du, baseDungeon, lstTerrain, calc);
                            t.Dungeons.Add(du);
                            choice.Remove(iChosen);
                        }
                    }
                }
            }
        }

        private static void GenerateDungeonTiles(Dungeon du, TDBDungeon duT, List<TDBTerrain> lstTerrain, Calc calc = null)
        {
            List<Tile> lstOrig = new List<Tile>();
            List<Tile> lstPotential = new List<Tile>();
            if (calc == null) { calc = new Calc(); }

            int idCountOrig = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Tile tile = new Tile(i, j);
                    tile.ListIndex = idCountOrig;
                    lstOrig.Add(tile);
                    idCountOrig++;
                }
            }

            Choice choice = new Choice();
            Choice choice2 = new Choice();
            int iCount = 0;
            Tile t = null;
            int iLastTileTerrainID = 0;
            foreach (KeyValuePair<int, KeyValuePair<int, int>> int_kv in duT.TileTypes)
            {
                iCount += 1;
                if (iCount == duT.TileTypes.Count)
                {
                    iLastTileTerrainID = int_kv.Key;
                }
                else
                {
                    choice.Add(int_kv.Key, calc.GetRandom(int_kv.Value.Key, int_kv.Value.Value));
                }
                if (iCount == 1)
                {
                    t = TileList.GetTile(lstOrig, 4, 4);
                    t.TerrainID = int_kv.Key;
                    du.Tiles.Add(t);
                    lstOrig.Remove(t);
                    choice.Alternatives[0].Chance -= 1;
                    if (choice.Alternatives[0].Chance == 0) { choice.Remove(int_kv.Key); }
                }
            }
            lstPotential = TileList.GetSurroundingTiles(lstOrig, t.XCoord, t.YCoord);
            bool bCompleted = false;
            while (choice.Alternatives.Count > 0)
            {
                choice2.Clear();
                foreach (Tile tl in lstPotential)
                {
                    if (TileList.GetSurroundingTiles(du.Tiles.ToList(), tl.XCoord, tl.YCoord).Count == 1)
                    {
                        int iChance = 1;
                        int iTemp = 0;
                        iTemp = tl.XCoord - 4;
                        if (iTemp < 0) { iTemp = -iTemp; }
                        iChance += iTemp;
                        iTemp = tl.YCoord - 4;
                        if (iTemp < 0) { iTemp = -iTemp; }
                        iChance += iTemp;
                        choice2.Add(tl.ListIndex, iChance);
                    }
                }
                int iRes = choice2.Make(calc).ID;
                if (iRes == 0)
                {
                    iRes = 0;
                }
                Tile newTile = TileList.GetTileByListIndex(lstOrig, iRes);
                lstPotential.Remove(newTile);
                lstOrig.Remove(newTile);
                newTile.TerrainID = choice.Make(calc).ID;
                choice.GetAlternative(newTile.TerrainID).Chance -= 1;
                if (choice.GetAlternative(newTile.TerrainID).Chance == 0) { choice.Remove(newTile.TerrainID); }

                foreach (Tile tl in TileList.GetSurroundingTiles(lstOrig, newTile.XCoord, newTile.YCoord))
                {
                    lstPotential.Add(tl);
                }

                du.Tiles.Add(newTile);
                if (choice.Alternatives.Count > 0 && !bCompleted)
                {
                    choice.Add(iLastTileTerrainID, 1);
                    bCompleted = true;
                }
            }
        }

        private static Map GenerateMap(int iX, int iY, bool isKingOfTheHill, Dictionary<int, int> TilesPerDifficulty, Calc calc = null)
        {

            List<Tile> OrigTiles = new List<Tile>();
            List<Tile> TempTiles;
            Map map = new Map(iX, iY);
            Choice choice = new Choice();
            if (calc == null) { calc = new Calc(); }

            int idCountOrig = 0;
            for (int i = 0; i < map.XCount; i++)
            {
                for (int j = 0; j < map.YCount; j++)
                {
                    Tile tile = new Tile(i, j);
                    tile.ListIndex = idCountOrig;
                    OrigTiles.Add(tile);
                    idCountOrig++;
                }
            }

            Tile t;
            int iCurX = Calc.Div(map.XCount, 2);
            int iCurY = Calc.Div(map.YCount, 2);
            t = TileList.GetTile(OrigTiles, iCurX, iCurY);
            if (isKingOfTheHill)
            {
                t.Difficulty = 6;
                t.TerrainID = 27;
                TilesPerDifficulty[6] -= 1;
            }
            else
            {
                t.Difficulty = 1;
                TilesPerDifficulty[1] -= 1;
            }
            map.Tiles.Add(t);
            OrigTiles.Remove(t);
            //if (isKingOfTheHill)
            //{
            //    TempTiles = TileList.GetSurroundingTiles(OrigTiles, iCurX, iCurY);
            //    foreach (Tile tl in TempTiles)
            //    {
            //        tl.Difficulty = 6;
            //        tl.TerrainID = 28;
            //        TilesPerDifficulty[6] -= 1;
            //        map.Tiles.Add(tl);
            //        OrigTiles.Remove(tl);
            //    }
            //}

            int iCurrentDifficulty = 1;
            while (TilesPerDifficulty[6] > 0)
            {
                if (TilesPerDifficulty[iCurrentDifficulty] == 0) { iCurrentDifficulty += 1; }

                choice.Clear();
                GetNextAlternativeList(map.Tiles.ToList(), OrigTiles, ref choice);
                foreach (Alternative alt in choice.Alternatives)
                {
                    switch (iCurrentDifficulty)
                    {
                        case 1:
                            {
                                switch (alt.Chance)
                                {
                                    case 1: { alt.Chance = 1; break; }
                                    case 2: { alt.Chance = 5; break; }
                                    case 3: { alt.Chance = 10; break; }
                                    case 4: { alt.Chance = 20; break; }
                                }
                                break;
                            }
                        case 2:
                            {
                                switch (alt.Chance)
                                {
                                    case 1: { alt.Chance = 5; break; }
                                    case 2: { alt.Chance = 5; break; }
                                    case 3: { alt.Chance = 5; break; }
                                    case 4: { alt.Chance = 5; break; }
                                }
                                break;
                            }
                        case 3:
                            {
                                switch (alt.Chance)
                                {
                                    case 1: { alt.Chance = 10; break; }
                                    case 2: { alt.Chance = 25; break; }
                                    case 3: { alt.Chance = 5; break; }
                                    case 4: { alt.Chance = 1; break; }
                                }
                                break;
                            }
                        case 4:
                            {
                                switch (alt.Chance)
                                {
                                    case 1: { alt.Chance = 25; break; }
                                    case 2: { alt.Chance = 10; break; }
                                    case 3: { alt.Chance = 5; break; }
                                    case 4: { alt.Chance = 1; break; }
                                }
                                break;
                            }
                        case 5:
                            {
                                switch (alt.Chance)
                                {
                                    case 1: { alt.Chance = 50; break; }
                                    case 2: { alt.Chance = 20; break; }
                                    case 3: { alt.Chance = 3; break; }
                                    case 4: { alt.Chance = 1; break; }
                                }
                                break;
                            }
                        case 6:
                            {
                                switch (alt.Chance)
                                {
                                    case 1: { alt.Chance = 100; break; }
                                    case 2: { alt.Chance = 20; break; }
                                    case 3: { alt.Chance = 2; break; }
                                    case 4: { alt.Chance = 1; break; }
                                }
                                break;
                            }
                    }
                }

                int iChosen = choice.Make(calc).ID;
                t = OrigTiles.Where(ot => ot.ListIndex == iChosen).First();
                t.Difficulty = iCurrentDifficulty;
                map.Tiles.Add(t);
                OrigTiles.Remove(t);

                TilesPerDifficulty[iCurrentDifficulty] -= 1;

            }

            return map;
        }

        private static void SetInitialVisibleTiles(Map m, Player p)
        {
            p.VisibleTiles = new List<int>();
            p.FoggedTiles = new List<int>();
            p.VisitedTiles = new List<int>();
            p.MovableTiles = new List<int>();
            Tile t = TileList.GetTile(m.Tiles.ToList(), p.TileID);
            p.VisibleTiles.Add(t.ID);
            p.VisitedTiles.Add(t.ID);
            foreach (Tile tl in TileList.GetSurroundingTiles(m.Tiles.ToList(), p.X, p.Y))
            {
                p.VisibleTiles.Add(tl.ID);
                p.MovableTiles.Add(tl.ID);
                foreach (Tile tl2 in TileList.GetSurroundingTiles(m.Tiles.ToList(), tl.XCoord, tl.YCoord))
                {
                    if ((tl2 != t) && (!p.FoggedTiles.Contains(tl2.ID)))
                    {
                        p.FoggedTiles.Add(tl2.ID);
                    }
                }
            }
            p.SetTileListsString();
        }

    }
}