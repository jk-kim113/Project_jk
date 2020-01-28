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

public class SaveData
{
    public int[] PlayerLevel;
    public int StageLevel;
    public double[] MonsterAttack;
    public double[] MonsterDefend;
    public double[] MonsterHPmax;
    public int[] CardID;
}