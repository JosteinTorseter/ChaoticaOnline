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
    public class Dwelling
    {
        public int ID { get; set; }
        public int BaseDwellingID { get; set; }
        public int Power { get; set; }
        public int QuestLevel { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Alignment Alignment { get; set; }
        public string LeaderName { get; set; }
        public bool CanBeRaided { get; set; }
        public bool CanBeAttacked { get; set; }
        public bool HasStrength { get; set; }
        public int LeaderID { get; set; }
        public bool SpawnsParties { get; set; }
        public int Movement { get; set; }

        [ForeignKey("Tile")]
        public int TileId { get; set; }
        public virtual Tile Tile { get; set; }

        public Dwelling ()
        {
        }
        public Dwelling(TDBDwelling dw)
        {
            this.Name = dw.Name;
            this.Description = dw.Description;
            this.Image = dw.Image;
            this.Alignment = dw.Alignment;
            this.LeaderName = dw.LeaderName;
            this.CanBeRaided = dw.CanBeRaided;
            this.CanBeAttacked = dw.CanBeAttacked;
            this.HasStrength = dw.HasStrength;
            this.BaseDwellingID = dw.ID;
            this.Power = dw.StartPower;
            this.QuestLevel = dw.BaseQuestLevel;
            this.BaseDwellingID = dw.ID;
            this.LeaderID = dw.LeaderID;
            this.SpawnsParties = dw.SpawnsParties;
            this.Movement = dw.Movement;
        }


    }
}