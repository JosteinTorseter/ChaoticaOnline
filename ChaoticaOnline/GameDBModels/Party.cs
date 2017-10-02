using ChaoticaOnline.GameModels;
using ChaoticaOnline.lib;
using ChaoticaOnline.TemplateModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.GameDBModels
{
    public class Party
    {
        public int ID { get; set; }
        public int PlayerIndex { get; set; }
        public int Ident { get; set; }
        public Alignment Alignment { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        [ForeignKey("Tile")]
        public int TileId { get; set; }
        public virtual Tile Tile { get; set; }

        public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();

        public Party()
        {
        }
        public List<Unit> OrderedUnits()
        {
            return Units.OrderBy(u => u.PartyListIndex).ToList();
        }
    }
}