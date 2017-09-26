using ChaoticaOnline.GameDBModels;
using ChaoticaOnline.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.ViewModels
{
    public class CharSheetViewModel
    {
        public List<string> Stats { get; set; }
        public string CommandString { get; set; }
        public PlayerViewModel Player { get; set; }
        public List<SmallWorldItemViewModel> WornItems { get; set; }
        public List<SmallWorldItemViewModel> Inventory { get; set; }
        public CharSheetViewModel() { }
        public CharSheetViewModel(Player player, List<SmallWorldItemViewModel> wornItems, 
            List<SmallWorldItemViewModel> inventory)
        {
            this.Player = new PlayerViewModel(player);
            this.WornItems = wornItems;
            this.Inventory = inventory;
            this.Stats = new List<string>();
            this.Stats.Add("");
            this.Stats.Add(player.GetStat(HeroStat.Strength).ToString()  + " (" + player.Strength.ToString() + ")");
            this.Stats.Add(player.GetStat(HeroStat.Dexterity).ToString() + " (" + player.Dexterity.ToString() + ")");
            this.Stats.Add(player.GetStat(HeroStat.Constitution).ToString() + " (" + player.Constitution.ToString() + ")");
            this.Stats.Add(player.GetStat(HeroStat.Wisdom).ToString() + " (" + player.Wisdom.ToString() + ")");
            this.Stats.Add(player.GetStat(HeroStat.Intelligence).ToString() + " (" + player.Intelligence.ToString() + ")");
            this.Stats.Add(player.GetStat(HeroStat.Cunning).ToString() + " (" + player.Cunning.ToString() + ")");
            this.CommandString = player.UsedCommand + " / " + player.Command;
        }

        public SmallWorldItemViewModel ItemByType(string itemType)
        {
            foreach (SmallWorldItemViewModel it in WornItems)
            {
                if (it.TypeName == itemType) { return it; }
            }
            if (itemType == "Offhand")
            {

            }
            return null;
        }
        public List<SmallWorldItemViewModel> ItemsByType(string itemType)
        {
            List<SmallWorldItemViewModel> res = new List<SmallWorldItemViewModel>();
            foreach (SmallWorldItemViewModel it in WornItems)
            {
                if (it.TypeName == itemType) { res.Add(it); }
            }
            return res;
        }
    }
}