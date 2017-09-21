using ChaoticaOnline.DAL;
using ChaoticaOnline.GameModels;
using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.GameDBModels
{
    public class Game
    {
        [Key]
        public int ID { get; set; }
        public virtual Map Map { get; set; }
        public bool IsKingOfTheHill { get; set; }
        public bool IsGoodVsEvil { get; set; }
        public double XPDiffMultiplier { get; set; } = 1;
        public double GoldDiffMultiplier { get; set; } = 1;
        public double PartyDiffMultiplier { get; set; } = 1;
        public double HappyGainDiffMultiplier { get; set; } = 1;

        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
        public virtual ICollection<Party> Parties { get; set; } = new List<Party>();

        public Game()
        {
        }

    }
}