using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChaoticaOnline.lib
{

    public enum WorldObjectType
    {
        None = 0,
        Door = 1,
        Chest = 2,
        Special = 3
    }
    public enum TileCardType
    {
        None = 0,
        Dungeon = 1,
        Dwelling = 2,
        Army = 3,
        Reward = 4,
        Object = 5,
        Unit = 6
    }
    public enum ActionRangeType
    {
        None = 0,
        HTH = 1,
        Ranged = 2
    }
    public enum TargetType
    {
        None = 0,
        Self = 1,
        SingleAlly = 2,
        MultipleAlly = 3,
        AllAlly = 4,
        SingleEnemy = 5,
        MultipleEnemy = 6,
        AllEnemy = 7,
        All = 8,
        AllyRow = 9,
        EnemyRow = 10,
        DeadUnit = 11,
        FreeSpace = 12
    }
    public enum SpecialCategory
    {
        None = 0,
        Spell = 1,
        Ability = 2,
        Innate = 3,
        AfterCombat = 4,
        Skill = 5
    }
    public enum EffectCategory
    {
        None = 0,
        Damage = 1,
        Enchant = 2,
        Bonus = 3,
        Heal = 4,
        Summon = 5,
        Resurrect = 6,
        MapAction = 7
    }
    public enum HeroStat
    {
        None = 0,
        Strength = 1,
        Dexterity = 2,
        Constitution = 3,
        Wisdom = 4,
        Intelligence = 5,
        Cunning = 6
    }
    public enum ItemCategory
    {
        None = 0,
        Key = 1,
        Spawn = 2,
        HeroItem = 3,
        WorldItem = 4,
        CompanionItem = 5,
        Vehichle = 6
    }
    public enum EntityType
    {
        None = 0,
        Game = 1,
        Player = 2,
        Map = 3,
        Tile = 4,
        Dwelling = 5,
        Dungeon = 6,
        Unit = 7,
        SpecialAction = 8,
        Attribute = 9,
        WorldItem = 10
    }
    public enum ButtonAction
    {
        None = 0,
        Move = 1,
        Explore = 2,
        Search = 3,
        Tax = 4,
        Rest = 5,
        Open = 6,
        Unlock = 7,
        Raid = 8,
        Attack = 9,
        Enter = 10,
        Lockpick = 11,
        Crush = 12,
        Buy = 13,
        GetQuest = 14,
        AcceptQuest = 15,
        Bribe = 16
    }

    public enum TileVisibility
    {
        None = 0,
        Fogged = 1,
        Visible = 2,
        Visited = 3,
        Movable = 4
    }

    public enum TileSelectionType
    {
        None = 0,
        Selected = 1,
        QuestOrigin = 2,
        QuestTarget = 3
    }

    public enum Alignment
    {
        Inherited = -2,
        Evil = -1,
        Neutral = 0,
        Good = 1,
        Any = 2
    }

    public enum TileType
    {
        None = 0,
        Heaven = 1,
        Hell = 2,
        TheHill = 3,
        Hillside = 4,
        Special = 5,
        Dungeon = 6
    }

    public enum BonusType
    {
        None = 0,
        MaxHPBonus = 1,
        DMGBonus = 2,
        AttackBonus = 3,
        DefenceBonus = 4,
        MaxDMGBonus = 5,
        //Unknown = 6,
        //FaithBonus = 7,
        MaxManaBonus = 8,
        StrBonus = 9,
        DexBonus = 10,
        ConBonus = 11,
        WisBonus = 12,
        IntBonus = 13,
        CunBonus = 14,
        MoveBonus = 15,
        //CurHPBonus = 16,
        //NrOfSpellsBonus = 17,
        AddAbility = 18,
        AddSpell = 19,
        //MapCurHPBonus = 20,
        //CurMana = 21,
        //MaxMana = 22,
        SpeedBonus = 23,
        RangeBonus = 24,
        //ReceiveItem = 25,
        //DoT = 26,
        //KnockBack = 27,
        //DoTMax = 28,
        NrOfTargets = 29,
        NrOfAttacks = 30,
        //Stun = 31,
        //CannotUseAbilities = 32,
        //CannotUseSpells = 33,
        //HealDoT = 34,
        //HealDoTMax = 35,
        //SummonTime = 36,
        //SummonUnit = 37,
        //MaxRangeFromOrigin = 38,
        //DamageMultiplyer = 39,
        //Defending = 40,
        //VoidAttack = 41,
        //RepeatedSpell = 42,
        //Timestop = 43,
        //ChainDelimiter = 44,
        //KillHD = 45,
        //PercentDamage = 46,
        //MoveMultiplyer = 47,
        InstantKillChance = 48,
        //DrainPercent = 49,
        Resistance = 50,
        MagResistance = 51,
        //SpellResistance = 52,
        TwoHanded = 53,
        Spawn = 54,
        //RandomMageSpell = 55,
        //RandomPriestSpell = 56,
        //RandomHTHAction = 57,
        //RandomRangedAction = 58,
        //RandomAction = 59,
        //RandomSpell = 60,
        //PickChance = 61,
        //Lockpick = 62,
        //TrueSight = 63,
        //PercentHeal = 64,
        //PercentMana = 65,
        //MaxUnits = 66,
        //AttributeGain = 67,
        //RandomScroll = 68,
        //PickPocket = 69,
        //Gamble = 70,
        Command = 71,
        Teletport = 72,
        DestroyItem = 73,
        ManaDot = 74,
        MagicPower = 75,
        MagicRange = 76,
        MagicDamage = 77,
        EffectNrOfTurns = 78,
        SalePrice = 79
    }
}