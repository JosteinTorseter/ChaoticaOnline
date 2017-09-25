using ChaoticaOnline.DAL;
using ChaoticaOnline.GameModels;
using ChaoticaOnline.lib;
using ChaoticaOnline.TemplateModels;
using ChaoticaOnline.ViewModels;
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
        public string Name { get; set; }
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
        public int CultivatingID { get; set; }
        public int Command { get; set; }
        public int UsedCommand { get; set; }
        public int CultivationTurnsLeft { get; set; }
        public Alignment Alignment { get; set; }
        public string HeroImage { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Wisdom { get; set; }
        public int Intelligence { get; set; }
        public int Cunning { get; set; }
        public int CurHP { get; set; }
        public int CurMana { get; set; }
        public int Level { get; set; }
        public int Line { get; set; }
        public int XP { get; set; }

        public virtual ICollection<Bonus> PermanentBonuses { get; set; } = new List<Bonus>();

        public bool CanWearItem(TDBWorldItem it)
        {
            if ((int)it.RequiredAlignment < 2 && this.Alignment != it.RequiredAlignment)
            {
                return false;
            }
            if (!this.MeetsAttribReqs(it.RequiredStats))
            {
                return false;
            }
            return true;
        }

        public bool MeetsAttribReqs (Dictionary<int, int> req)
        {
            foreach (KeyValuePair<int, int> kv in req)
            {
                if (!this.MeetsAttribReq(kv.Key, kv.Value))
                {
                    return false;
                }
            }
            return true;
        }
        public bool MeetsAttribReq(int stat, int val)
        {
            return (this.GetStat((HeroStat)stat) < val);
        }
        public bool AlreadyWearingThis(WorldItem it)
        {
            return (this.WorldItems.Where(wit => wit.Wearing == true && wit.BaseItemID == it.BaseItemID).ToList().Count > 0);
        }

        public WorldItem WornItemByType(string itemType)
        {
            List<WorldItem> res = this.WorldItems.Where(wit => wit.Wearing == true && wit.TypeName == itemType).ToList();
            if (res.Count > 0)
            {
                return res[0];
            }
            return null;
        }
        public List<WorldItem> WornItemsByType(string itemType)
        {
            return this.WorldItems.Where(wit => wit.Wearing == false && wit.TypeName == itemType).ToList();
        }

        public int GetPermanentBonus(BonusType bt)
        {
            int iRes = 0;
            foreach (Bonus b in this.PermanentBonuses)
            {
                if (b.BonusType == bt) { iRes += b.IntValue(); }
            }
            return iRes;
        }

        public int GetMagicPowerBonus(BonusType bt, int iPower)
        {
            int iRes = 0;
            switch (bt)
            {
                case BonusType.MagicRange:
                    {
                        iRes = Calc.Round(iPower / 5, -1);
                        break;
                    }
                case BonusType.MagicDamage:
                    {
                        iRes = Calc.Round(iPower / 2, -1);
                        break;
                    }
                case BonusType.MagResistance:
                    {
                        iRes = iPower;
                        break;
                    }
                case BonusType.EffectNrOfTurns:
                    {
                        iRes = Calc.Round(iPower / 4, -1);
                        break;
                    }
            }
            return iRes;
        }

        public void AddPermanentBonus(BonusType bt, double value)
        {
            bool bFound = false;
            foreach (Bonus b in this.PermanentBonuses)
            {
                if (b.BonusType == bt)
                {
                    b.Value += value;
                    bFound = true;
                    break;
                }
            }
            if (!bFound)
            {
                this.PermanentBonuses.Add(new Bonus(bt, value, 0));
            }
        }

        public int GetAttributeValue(BonusType bt)
        {
            int iRes = 0;
            int iPower = 0;
            iPower += this.GetPermanentBonus(BonusType.MagicPower);
            iPower += this.GetStatBonus(BonusType.MagicPower);
            iPower += this.GetItemBonus(BonusType.MagicPower);
            iRes += this.GetMagicPowerBonus(bt, iPower);
            iRes += this.GetPermanentBonus(bt);
            iRes += this.GetStatBonus(bt);
            iRes += this.GetItemBonus(bt);
            return iRes;
        }

        public Unit GetHeroUnit()
        {
            Unit res = new Unit();
            res.Alignment = this.Alignment;
            res.Attack = (double)GetAttributeValue(BonusType.AttackBonus);
            res.Defence = (double)GetAttributeValue(BonusType.DefenceBonus);
            res.DamageBase = (double)GetAttributeValue(BonusType.DMGBonus);
            res.DamageRoll = (double)GetAttributeValue(BonusType.MaxDMGBonus);
            res.Speed = (double)GetAttributeValue(BonusType.SpeedBonus);
            res.AttackRange = GetAttributeValue(BonusType.RangeBonus);
            res.BaseUnitID = 0;
            res.Difficulty = 1;
            res.MaxHP = GetAttributeValue(BonusType.MaxHPBonus);
            res.MaxMana = GetAttributeValue(BonusType.MaxManaBonus);
            res.HP = this.CurHP;
            res.Mana = this.CurMana;
            res.MaxLevel = Statics.HeroMaxLevel;
            res.Level = this.Level;
            res.Line = this.Line;
            res.MagicPower = (double)GetAttributeValue(BonusType.MagicPower);
            res.MagicRange = GetAttributeValue(BonusType.MagicRange);
            res.MagicResistance = (double)GetAttributeValue(BonusType.MagResistance);
            res.Name = this.Name;
            res.NextLevelXP = 1000;
            res.NrOfAttacks = (double)GetAttributeValue(BonusType.NrOfAttacks);
            res.NrOfTargets = (double)GetAttributeValue(BonusType.NrOfAttacks);
            res.PartyListIndex = 0;
            res.RaceClassName = "Hero";
            res.Resistance = (double)GetAttributeValue(BonusType.MagResistance); ;
            res.Takes2Slots = false;
            res.XP = this.XP;
            res.Image = this.HeroImage;
            res.ID = 0;
            res.IsHero = true;
            return res;
        }

        public int GetStat(HeroStat stat, bool addBonuses = true)
        {
            int res = 0;
            switch (stat)
            {
                case HeroStat.Strength:
                    {
                        res = this.Strength;
                        if (addBonuses)
                        {
                            res += this.GetItemBonus(BonusType.StrBonus);
                        }
                        break;
                    }
                case HeroStat.Dexterity:
                    {
                        res = this.Dexterity;
                        if (addBonuses)
                        {
                            res += this.GetItemBonus(BonusType.DexBonus);
                        }
                        break;
                    }
                case HeroStat.Constitution:
                    {
                        res = this.Constitution;
                        if (addBonuses)
                        {
                            res += this.GetItemBonus(BonusType.ConBonus);
                        }
                        break;
                    }
                case HeroStat.Wisdom:
                    {
                        res = this.Wisdom;
                        if (addBonuses)
                        {
                            res += this.GetItemBonus(BonusType.WisBonus);
                        }
                        break;
                    }
                case HeroStat.Intelligence:
                    {
                        res = this.Intelligence;
                        if (addBonuses)
                        {
                            res += this.GetItemBonus(BonusType.IntBonus);
                        }
                        break;
                    }
                case HeroStat.Cunning:
                    {
                        res = this.Cunning;
                        if (addBonuses)
                        {
                            res += this.GetItemBonus(BonusType.CunBonus);
                        }
                        break;
                    }
            }
            return res;
        }

        public int GetItemBonus(BonusType bt)
        {
            int iRes = 0;
            foreach (WorldItem it in this.WorldItems.Where(wit => wit.Wearing == true).ToList())
            {
                iRes += it.GetBonus(bt);
            }
            return iRes;
        }

        public int GetStatBonus(BonusType bt)
        {
            int iRes = 0;
            switch (bt)
            {
                case BonusType.AttackBonus:
                    {
                        iRes += Calc.Round((GetStat(HeroStat.Strength) - 1) / 2, 1) - 5;
                        iRes += Calc.Round(GetStat(HeroStat.Dexterity) / 2, 1) - 5;
                        break;
                    }
                case BonusType.DefenceBonus:
                    {
                        iRes += Calc.Round((GetStat(HeroStat.Cunning) - 1) / 2, 1) - 5;
                        iRes += Calc.Round(GetStat(HeroStat.Constitution) / 2, 1) - 5;
                        break;
                    }
                case BonusType.DMGBonus:
                    {
                        iRes += GetStat(HeroStat.Strength) - 9;
                        break;
                    }
                case BonusType.MaxDMGBonus:
                    {
                        iRes += GetStat(HeroStat.Dexterity) - 9;
                        break;
                    }
                case BonusType.MaxHPBonus:
                    {
                        iRes += GetStat(HeroStat.Constitution) * 2;
                        break;
                    }
                case BonusType.MaxManaBonus:
                    {
                        iRes += GetStat(HeroStat.Wisdom);
                        iRes += GetStat(HeroStat.Intelligence);
                        break;
                    }
                case BonusType.SpeedBonus:
                    {
                        iRes += Calc.Round((GetStat(HeroStat.Dexterity) + GetStat(HeroStat.Cunning)) / 2, 1) - 5;
                        break;
                    }
                case BonusType.MoveBonus:
                    {
                        iRes += GetStat(HeroStat.Cunning) - 10;
                        break;
                    }
                case BonusType.MagicPower:
                    {
                        iRes += Calc.Round((GetStat(HeroStat.Wisdom) - 1) / 2, 1) - 5;
                        iRes += Calc.Round(GetStat(HeroStat.Intelligence) / 2, 1) - 5;
                        break;
                    }
                case BonusType.RangeBonus:
                    {
                        int iDex = GetStat(HeroStat.Dexterity);
                        if (iDex > 15) { iRes++; }
                        if (iDex > 19) { iRes++; }
                        break;
                    }
                case BonusType.Resistance:
                    {
                        iRes += Calc.Round((GetStat(HeroStat.Constitution) - 17) / 2, 1);
                        break;
                    }
            }
            return iRes;
        }

        [ForeignKey("Game")]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        public virtual ICollection<WorldItem> WorldItems { get; set; } = new List<WorldItem>();

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

        public List<List<SmallWorldItemViewModel>> GetHeroItems(TemplateContext dbT)
        {
            List<int> itemIDs = new List<int>();
            List<List<SmallWorldItemViewModel>> res = new List<List<SmallWorldItemViewModel>>();
            res.Add(new List<SmallWorldItemViewModel>());
            res.Add(new List<SmallWorldItemViewModel>());
            foreach (WorldItem it in this.WorldItems.Where(wit => wit.Category == ItemCategory.HeroItem))
            {
                if (!itemIDs.Contains(it.BaseItemID)) { itemIDs.Add(it.BaseItemID); }
            }
            Dictionary<int, TDBWorldItem> baseItems = new Dictionary<int, TDBWorldItem>();
            foreach (TDBWorldItem bit in dbT.TDBWorldItems.Where(bwit => itemIDs.Contains(bwit.ID)).ToList())
            {
                baseItems.Add(bit.ID, bit);
            }
            foreach (WorldItem it in this.WorldItems.Where(wit => wit.Category == ItemCategory.HeroItem))
            {
                if (it.Wearing)
                {
                    res[0].Add(new SmallWorldItemViewModel(baseItems[it.BaseItemID], 1, it.ID));
                } else
                {
                    res[1].Add(new SmallWorldItemViewModel(baseItems[it.BaseItemID], it.Count, it.ID));
                }
            }
            return res;
        }

        public List<SmallWorldItemViewModel> GetInventoryItems(TemplateContext dbT)
        {
            List<int> itemIDs = new List<int>();
            List<SmallWorldItemViewModel> res = new List<SmallWorldItemViewModel>();
            foreach (WorldItem it in this.WorldItems.Where(wit => wit.Wearing == false))
            {
                if (!itemIDs.Contains(it.BaseItemID)) { itemIDs.Add(it.BaseItemID); }
            }
            Dictionary<int, TDBWorldItem> baseItems = new Dictionary<int, TDBWorldItem>();
            foreach (TDBWorldItem bit in dbT.TDBWorldItems.Where(bwit => itemIDs.Contains(bwit.ID)).ToList())
            {
                baseItems.Add(bit.ID, bit);
            }
            foreach (WorldItem it in this.WorldItems.Where(wit => wit.Wearing == false))
            {
                res.Add(new SmallWorldItemViewModel(baseItems[it.BaseItemID], it.Count, it.ID));
            }
            return res;
        }
        
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