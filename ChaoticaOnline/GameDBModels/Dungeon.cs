using ChaoticaOnline.GameModels;
using ChaoticaOnline.TemplateModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.GameDBModels
{
    public class Dungeon
    {
        public int ID { get; set; }
        public int BaseDungeonID { get; set; }
        public int Difficulty { get; set; }
        public bool IsOpen { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string LeaderName { get; set; }
        public int LeaderID { get; set; }


        [ForeignKey("Tile")]
        public int TileId { get; set; }
        public virtual Tile Tile { get; set; }

        public virtual ICollection<Tile> Tiles { get; set; } = new List<Tile>();

        public string MinionsLeftString { get; set; }
        [NotMapped]
        public List<int> MinionsLeft
        {
            get
            {
                if (MinionsLeftString == "") { return new List<int>(); }
                return Array.ConvertAll(MinionsLeftString.Split(';'), Int32.Parse).ToList();
            }
            set
            {
                MinionsLeftString = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }
        
        public Dungeon()
        {
        }
        public Dungeon(TDBDungeon du)
        {
            this.Name = du.Name;
            this.BaseDungeonID = du.ID;
            this.Difficulty = du.Difficulty;
            this.IsOpen = false;
            this.Description = du.Description;
            this.Image = du.Image;
            this.LeaderName = du.LeaderName;
            this.LeaderID = du.LeaderID;
            this.MinionsLeftString = du.MinionsString;
        }

        //Public Property SkillChecks As List(Of SkillCheck)
        //Public Property QuestCards As List(Of Card)


    }
}