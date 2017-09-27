using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.TemplateModels
{
    public class TDBDungeon
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Alignment Alignment { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string LeaderName { get; set; }
        public int Difficulty { get; set; }
        public int LeaderID { get; set; }
        public bool IsUnique { get; set; }
        public string MinionsString { get; set; }
        [NotMapped]
        public int[] Minions
        {
            get
            {
                if (MinionsString == "") { return new int[0]; }
                return Array.ConvertAll(MinionsString.Split(';'), Int32.Parse);
            }
            set
            {
                MinionsString = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }
        public string SecretTilesString { get; set; }
        [NotMapped]
        public Dictionary<int, KeyValuePair<int, int>> SecretTiles
        {
            get
            {
                return DictionaryHack.GetKVsByString(SecretTilesString);
            }
            set
            {
                SecretTilesString = DictionaryHack.GetStringByKVs(value);
            }
        }
        public string TileTypesString { get; set; }
        [NotMapped]
        public Dictionary<int, KeyValuePair<int, int>> TileTypes
        {
            get
            {
                return DictionaryHack.GetKVsByString(TileTypesString);
            }
            set
            {
                TileTypesString = DictionaryHack.GetStringByKVs(value);
            }
        }
        
        
        //Public Property SpecialItems As List(Of KV_IntInt)
        //Public Property Chests As List(Of KV_IntStr)
        //Public Property Doors As List(Of KV_IntInt)
        //Public Property TileTypes As List(Of Integer)
        //Public Property Key As Integer
    }
}