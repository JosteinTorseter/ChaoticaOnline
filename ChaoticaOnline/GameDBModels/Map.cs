using ChaoticaOnline.DAL;
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
    public class Map
    {
        public int XCount { get; set; }
        public int YCount { get; set; }
        [Key, ForeignKey("Game")]
        public int GameID { get; set; }
        public virtual Game Game { get; set; }

        public virtual ICollection<Tile> Tiles { get; set; } = new List<Tile>();

        public Map()
        {
        }

        public Map(int iX, int iY)
        {
            this.XCount = iX;
            this.YCount = iY;
        }

        public void FillTileCards(Calc calc = null)
        {
            if (calc == null) { calc = new Calc(); }
            foreach (Tile t in this.Tiles)
            {
                t.FillTileCards(calc);
            }
        }
    }
}