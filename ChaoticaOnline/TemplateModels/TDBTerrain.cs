using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.TemplateModels
{
    public class TDBTerrain
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public string BGColor { get; set; }
        public int Difficulty { get; set; }
        public int SetDungeon { get; set; }
        public int SetDwelling { get; set; }
        public int TravelTime { get; set; }
        public TileType TileType { get; set; }

        public string ItemDropsString { get; set; }
        [NotMapped]
        public Dictionary<int, KeyValuePair<int, int>> ItemDrops
        {
            get
            {
                return DictionaryHack.GetKVsByString(ItemDropsString);
            }
            set
            {
                ItemDropsString = DictionaryHack.GetStringByKVs(value);
            }
        }

        public string SpecialItemsString { get; set; }
        [NotMapped]
        public List<int> SpecialItems
        {
            get
            {
                if (String.IsNullOrEmpty(SpecialItemsString)) { return new List<int>(); }
                return Array.ConvertAll(SpecialItemsString.Split(';'), Int32.Parse).ToList();
            }
            set
            {
                SpecialItemsString = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }

        public string DwellingTypesString { get; set; }
        [NotMapped]
        public Dictionary<int, int> DwellingTypes
        {
            get
            {
                return DictionaryHack.GetIntsByString(DwellingTypesString);
            }
            set
            {
                DwellingTypesString = DictionaryHack.GetStringByInts(value);
            }
        }

        public string RaceTypesString { get; set; }
        [NotMapped]
        public Dictionary<int, int> RaceTypes
        {
            get
            {
                return DictionaryHack.GetIntsByString(RaceTypesString);
            }
            set
            {
                RaceTypesString = DictionaryHack.GetStringByInts(value);
            }
        }

        public string DungeonTypesString { get; set; }
        [NotMapped]
        public Dictionary<int, int> DungeonTypes
        {
            get
            {
                return DictionaryHack.GetIntsByString(DungeonTypesString);
            }
            set
            {
                DungeonTypesString = DictionaryHack.GetStringByInts(value);
            }
        }
    }
}