using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    private float mATK;
    private float mDEF;
    private float mHP;

    // Start is called before the first frame update
    void Start()
    {
        mATK = 3;
        mDEF = 3;
        mHP = 50;
    }

    public void GetDamage(float damage)
    {
        mHP -= damage;
        Debug.Log(mHP);
    }

    public float MonsterAttack()
    {
        return mATK;
    }
}
