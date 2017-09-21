using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.TemplateModels
{
    public class TDBDwelling
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Alignment Alignment { get; set; }
        public int LeaderID { get; set; }
        public int StartPower { get; set; }
        public string LeaderName { get; set; }
        public bool SpawnsParties { get; set; }
        public int Movement { get; set; }
        public int BaseQuestLevel { get; set; }
        public bool CanBeRaided { get; set; }
        public bool CanBeAttacked { get; set; }
        public bool HasStrength { get; set; }
        public bool IsUnique { get; set; }
        public int RaceID { get; set; }
        public string TradeUnitsString { get; set; }
        [NotMapped]
        public Dictionary<int, KeyValuePair<int, int>> TradeUnits
        {
            get
            {
                return DictionaryHack.GetKVsByString(TradeUnitsString);
            }
            set
            {
                TradeUnitsString = DictionaryHack.GetStringByKVs(value);
            }
        }
        public string FightUnitsString { get; set; }
        [NotMapped]
        public Dictionary<int, int> FightUnits
        {
            get
            {
                return DictionaryHack.GetIntsByString(TradeUnitsString);
            }
            set
            {
                FightUnitsString = DictionaryHack.GetStringByInts(value);
            }
        }
        
        //Public Property LitterBox As Integer
        //Public Property ItemsForSale As List(Of KV_IntStr)
        //Public Property TrainingAvailable As List(Of Training)
    }
}