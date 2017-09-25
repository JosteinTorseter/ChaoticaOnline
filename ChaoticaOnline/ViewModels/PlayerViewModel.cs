using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.GameModels;
using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.ViewModels
{
    public class PlayerViewModel
    {
        public int ID { get; set; }
        public string Color { get; set; }
        public string MovePoints { get; set; }
        public int CurrentGold { get; set; }
        public int PartyID { get; set; }
        public int RosterID { get; set; }
        public Alignment Alignment { get; set; }
        public string HeroImage { get; set; }
        public PlayerViewModel()
        {
        }
        public PlayerViewModel(Player player)
        {
            this.ID = player.ID;
            this.Color = player.Color;
            this.PartyID = player.PartyID;
            this.RosterID = player.RosterID;
            this.MovePoints = player.MovePointsLeft + " / " + player.MaxMovePoints;
            this.CurrentGold = player.CurrentGold;
            this.Alignment = player.Alignment;
            this.HeroImage = player.HeroImage;
        }
        public PlayerViewModel(int iID, string sColor)
        {
            ID = iID;
            Color = sColor;
        }
    }
}