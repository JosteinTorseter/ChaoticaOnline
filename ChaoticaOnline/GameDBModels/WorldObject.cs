using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.GameDBModels
{
    public class WorldObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public WorldObjectType ObjectType { get; set; }
        public int RelatedID { get; set; }

        [ForeignKey("Tile")]
        public int TileId { get; set; }
        public virtual Tile Tile { get; set; }
    }
}