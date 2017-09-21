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

        public string PlayersString { get; set; }
        [NotMapped]
        public List<int> Players
        {
            get
            {
                if (PlayersString == null || PlayersString == "") { return new List<int>(); }
                return Array.ConvertAll(PlayersString.Split(';'), Int32.Parse).ToList();
            }
            set
            {
                PlayersString = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }
        
        public virtual ICollection<Dwelling> Dwellings { get; set; } = new List<Dwelling>();
        public virtual ICollection<Dungeon> Dungeons { get; set; } = new List<Dungeon>();

        public Tile()
        {
        }
        public Tile(int iX, int iY)
        {
            this.XCoord = iX;
            this.YCoord = iY;
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