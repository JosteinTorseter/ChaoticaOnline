using ChaoticaOnline.GameModels;
using ChaoticaOnline.lib;
using ChaoticaOnline.TemplateModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.GameDBModels
{
    public class Tile
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public int TerrainID { get; set; }
        public int ExploreCards { get; set; }
        public int Difficulty { get; set; }
        public string Image { get; set; }
        public string BGColor { get; set; }
        public int TravelTime { get; set; }
        public TileType TileType { get; set; }

        [ForeignKey("Map")]
        public int MapId { get; set; }
        public virtual Map Map { get; set; }
        
        public int DungeonID { get; set; }

        [NotMapped]
        public int ListIndex { get; set; }

        public virtual ICollection<Dwelling> Dwellings { get; set; } = new List<Dwelling>();
        public virtual ICollection<Dungeon> Dungeons { get; set; } = new List<Dungeon>();
        public virtual ICollection<WorldObject> WorldObjects { get; set; } = new List<WorldObject>();
        public virtual ICollection<Party> Parties { get; set; } = new List<Party>();
        public virtual ICollection<TileCard> Cards { get; set; } = new List<TileCard>();

        public string PlayersString { get; set; }
        [NotMapped]
        public List<int> Players
        {
            get
            {
                if (String.IsNullOrEmpty(PlayersString)) { return new List<int>(); }
                return Array.ConvertAll(PlayersString.Split(';'), Int32.Parse).ToList();
            }
            set
            {
                PlayersString = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }
        

        public Tile()
        {
        }
        public Tile(int iX, int iY)
        {
            this.XCoord = iX;
            this.YCoord = iY;
        }

        public List<TileCard> GetCards(Player player)
        {
            List<TileCard> res = new List<TileCard>();
            int i = -1;
            foreach (Dwelling dw in this.Dwellings)
            {
                if (!player.VisibleObjects.Contains(new KeyValuePair<int, int>((int)TileCardType.Dwelling, dw.ID)))
                {
                    res.Add(new TileCard(TileCardType.Dwelling, dw.ID, i));
                    i--;
                }
            }
            foreach (Dungeon du in this.Dungeons)
            {
                if (!player.VisibleObjects.Contains(new KeyValuePair<int, int>((int)TileCardType.Dungeon, du.ID)))
                {
                    res.Add(new TileCard(TileCardType.Dungeon, du.ID, i));
                    i--;
                }
            }
            foreach (WorldObject obj in this.WorldObjects)
            {
                if (!player.VisibleObjects.Contains(new KeyValuePair<int, int>((int)TileCardType.Object, obj.ID)))
                {
                    res.Add(new TileCard(TileCardType.Object, obj.ID, i));
                    i--;
                }
            }
            foreach (Party p in this.Parties)
            {
                if (!player.VisibleObjects.Contains(new KeyValuePair<int, int>((int)TileCardType.Army, p.ID)))
                {
                    res.Add(new TileCard(TileCardType.Army, p.ID, i));
                    i--;
                }
            }
            foreach (TileCard c in this.Cards)
            {
                if (c.PlayerOnlyID == 0 && c.PlayerOnlyID == player.ID)
                {
                    res.Add(c);
                }
            }
            return res;
        }

        public TileCard DrawCard(Player player, Calc calc = null)
        {
            List<TileCard> lst = GetCards(player);
            if (lst.Count == 0) { return null; }
            List<TileCard> lstTemp = lst.Where(c => c.Weight > 0).OrderBy(c => c.Weight).ToList();
            if (lstTemp.Count > 0) { return lstTemp[0]; }
            lstTemp = lst.Where(c => c.Weight == 0).ToList();
            if (lstTemp.Count > 0)
            {
                if (calc == null) { calc = new Calc(); }
                Choice oCh = new Choice();
                foreach (TileCard card in lstTemp)
                {
                    oCh.Add(card.ID, 1);
                }
                return lst.Find(c => c.ID == oCh.Make(calc).ID);
            }
            lstTemp = lst.Where(c => c.Weight < 0).OrderByDescending(c => c.Weight).ToList();
            if (lstTemp.Count > 0) { return lstTemp[0]; }
            return null;
        }

        public bool IsNeighbour(int iX, int iY)
        {
            if (this.XCoord == iX && (((this.YCoord - 1) == iY) || ((this.YCoord + 1) == iY)))
            {
                return true;
            }
            if (this.YCoord == iY && (((this.XCoord - 1) == iX) || ((this.XCoord + 1) == iX)))
            {
                return true;
            }
            return false;
        }

        public void SetTerrain(TDBTerrain terr)
        {
            this.Name = terr.Name;
            this.Description = terr.Description;
            this.TerrainID = terr.ID;
            this.Image = terr.Filename;
            this.BGColor = terr.BGColor;
            this.TravelTime = terr.TravelTime;
            this.TileType = terr.TileType;
        }

    }
}