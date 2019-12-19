using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private MonsterData[] mMonsterDataArr;

    private void Awake()
    {
        #region MonsterData
        mMonsterDataArr = new MonsterData[4];

        mMonsterDataArr[0] = new MonsterData();
        mMonsterDataArr[0].Name = "Troll";
        mMonsterDataArr[0].ID = 0;
        mMonsterDataArr[0].Attack = 8;
        mMonsterDataArr[0].Defend = 2;
        mMonsterDataArr[0].HPmax = 50;
        mMonsterDataArr[0].HPcurrent = 50;

        mMonsterDataArr[1] = new MonsterData();
        mMonsterDataArr[1].Name = "HobGoblin";
        mMonsterDataArr[1].ID = 1;
        mMonsterDataArr[1].Attack = 3;
        mMonsterDataArr[1].Defend = 7;
        mMonsterDataArr[1].HPmax = 70;
        mMonsterDataArr[1].HPcurrent = 70;

        mMonsterDataArr[2] = new MonsterData();
        mMonsterDataArr[2].Name = "Goblin";
        mMonsterDataArr[2].ID = 2;
        mMonsterDataArr[2].Attack = 5;
        mMonsterDataArr[2].Defend = 5;
        mMonsterDataArr[2].HPmax = 40;
        mMonsterDataArr[2].HPcurrent = 40;

        mMonsterDataArr[3] = new MonsterData();
        mMonsterDataArr[3].Name = "Wolf";
        mMonsterDataArr[3].ID = 3;
        mMonsterDataArr[3].Attack = 9;
        mMonsterDataArr[3].Defend = 1;
        mMonsterDataArr[3].HPmax = 90;
        mMonsterDataArr[3].HPcurrent = 90;
        #endregion
    }
}

public class MonsterData
{
    public string Name;
    public int ID;
    public float Attack;
    public float Defend;
    public float HPmax;
    public float HPcurrent;
}