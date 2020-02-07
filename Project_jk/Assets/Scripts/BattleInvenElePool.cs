using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInvenElePool : ObjPool<BattleInvenElement>
{
#pragma warning disable 0649
    [SerializeField]
    private Transform mInvenSpawnPos;
#pragma warning restore

    protected override BattleInvenElement GetNewObj(int id)
    {
        BattleInvenElement newObj = Instantiate(mOriginArr[id], mInvenSpawnPos);
        mPool[id].Add(newObj);
        return newObj;
    }
}
