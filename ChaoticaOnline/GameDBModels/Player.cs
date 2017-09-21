using ChaoticaOnline.DAL;
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
    public class Player
    {
        public int ID { get; set; }
        public int PlayerNumber { get; set; }
        public string Color { get; set; }
        public int StartTileID { get; set; }
        public int TileID { get; set; }
        public int PartyID { get; set; }
        public int RosterID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int MaxMovePoints { get; set; }
        public int MovePointsLeft { get; set; }
        public int CurrentGold { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
        
        public string TileVisibilityString { get; set; }
        [NotMapped]
        public List<int> VisibleTiles { get; set; }
        [NotMapped]
        public List<int> FoggedTiles { get; set; }
        [NotMapped]
        public List<int> VisitedTiles { get; set; }
        [NotMapped]
        public List<int> MovableTiles { get; set; }

        public void SetTileListsString()
        {
            string res = "";
            foreach (int i in VisibleTiles)
            {
                this.VisTileString(ref res, i, TileVisibility.Visible);
            }
            foreach (int i in FoggedTiles)
            {
                this.VisTileString(ref res, i, TileVisibility.Fogged);
            }
            foreach (int i in VisitedTiles)
            {
                this.VisTileString(ref res, i, TileVisibility.Visited);
            }
            foreach (int i in MovableTiles)
            {
                this.VisTileString(ref res, i, TileVisibility.Movable);
            }
        }

        private void VisTileString(ref string res, int i, TileVisibility eVis)
        {
            if (res != "")
            {
                res += "#";
            }
            res += i.ToString() + ":" + ((int)eVis).ToString();
        }

        public void ArrangeStringToTileLists()
        {
            VisibleTiles = new List<int>();
            FoggedTiles = new List<int>();
            VisitedTiles = new List<int>();
            MovableTiles = new List<int>();
            if (TileVisibilityString == null || TileVisibilityString == "") { return; }
            foreach (string sFull in TileVisibilityString.Split('#'))
            {
                string[] s = sFull.Split(':');
                switch ((TileVisibility)Int32.Parse(s[1]))
                {
                    case TileVisibility.Visible:
                        {
                            VisibleTiles.Add(Int32.Parse(s[0]));
                            break;
                        }
                    case TileVisibility.Fogged:
                        {
                            FoggedTiles.Add(Int32.Parse(s[0]));
                            break;
                        }
                    case TileVisibility.Visited:
                        {
                            VisitedTiles.Add(Int32.Parse(s[0]));
                            break;
                        }
                    case TileVisibility.Movable:
                        {
                            MovableTiles.Add(Int32.Parse(s[0]));
                            break;
                        }
                }
            }

        }

        public Player()
        {
        }

        public TileVisibility TileVisible(int tileID)
        {
            if (this.VisibleTiles.Contains(tileID)) { return TileVisibility.Visible; }
            if (this.FoggedTiles.Contains(tileID)) { return TileVisibility.Fogged; }
            return TileVisibility.None;
        }
        //Public Property Inventory As List(Of WorldItem)
        //Public Property DungeonID As Integer
        //Public Property Cultivating As Integer
        //Public Property CultivationTime As Integer
        //Public Property MaxUnits As Integer
        //Public Property Effects As List(Of WorldEffect)
        //Public Property PotionsLeft As Integer
        //Public Property Keys As List(Of KV_IntInt)
        //Public Property SkillsUsed As List(Of Integer)
        //Public Property NotMyTurnXP As Integer
        //Public Property NotMyTurnGold As Integer
        //Public Property NotMyTurnItems As List(Of WorldItem)
    }
}