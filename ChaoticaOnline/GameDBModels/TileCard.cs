using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.GameDBModels
{
    public class TileCard
    {
        public int ID { get; set; }
        public WorldObjectType ObjectType { get; set; }
        public TileCardType CardType { get; set; }
        public int RelatedID { get; set; }
        public int PlayerOnlyID { get; set; }
        public int Weight { get; set; }

        [ForeignKey("Tile")]
        public int TileId { get; set; }
        public virtual Tile Tile { get; set; }

        public TileCard()
        {
        }

        public TileCard(TileCardType eType, int iRelated, int iID)
        {
            this.CardType = eType;
            this.RelatedID = iRelated;
            this.ID = iID;
        }
    }
}