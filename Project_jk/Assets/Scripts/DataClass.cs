using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePlayerState
{
    Waiting,
    Battle
}

public enum eBattleType
{
    Attack,
    Defend,
    Heal
}

public enum eFieldType
{
    Normal,
    Fire,
    Ice,
    Poison
}

public enum eShopItemType
{
    Consume,
    Equip
}

public enum eEquipType
{
    Attack,
    Defend,
    Heal
}

public class PlayerData
{
    public string Name;
    public int ID;
    public int Level;
    public double EXPcurrent;
    public double EXPmax;
    public double Attack;
    public double Defend;
    public double Heal;
    public double AttackBase;
    public double AttackWeight;
    public double DefendBase;
    public double DefendWeight;
    public double HealBase;
    public double HealWeight;
    public double HPmax;
    public double HPcurrent;
    public double HPbase;
    public double HPWeight;
    public eBattleType BattleType;
}

public class MonsterData
{
    public string Name;
    public int ID;
    public double Attack;
    public double Defend;
    public double HPmax;
    public double HPcurrent;
    public double AttackWeight;
    public double DefendWeight;
    public double HPWeight;
}

public class FieldData
{
    public string Name;
    public int ID;
    public string Info;
    public int Cycle;
    public double CycleValue;
    public int Condition;
    public double ConditionValue;
}

public class CardData
{
    public int ID;
    public string Contents;
}

public class ItemData
{
    public string Name;
    public int ID;
    public string Info;
    public double Value;
    public double Cost;
}

public class EquipData
{
    public string Name;
    public int ID;
    public string Info;
    public double Value;
    public double Cost;
    public eEquipType EquipType;
}

[System.Serializable]
public class SaveData
{
    public int[] PlayerLevel;
    public int StageLevel;
    public double Gold;
    public double[] MonsterAttack;
    public double[] MonsterDefend;
    public double[] MonsterHPmax;
    public int[] CardID;
    public int[] ItemNum;
    public int[] EquipNum;
}