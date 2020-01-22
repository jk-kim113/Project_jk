using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private string mName;
    private int mID;
    private double mATK, mDEF, mHPmax, mHPcurrent, mATKWeight, mDEFWeight, mHPWeight;
    public double ATK
    {
        get
        {
            return mATK;
        }
    }

    private Animator mAnimator;
    private static int DieAnim = Animator.StringToHash("dead");

    private void Awake()
    {
        mAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        UIController.Instance.ShowMonsterGaugeBar(mHPcurrent, mHPmax);
    }

    public void Initialize(string name, int id, double atk, double def, double hpmax, double hpcurrent, double atkweight, double defweight, double hpweight)
    {
        mName = name;
        mID = id;
        mATKWeight = atkweight;
        mDEFWeight = defweight;
        mHPWeight = hpweight;
        mHPmax = hpmax;
        mHPcurrent = mHPmax;

        Renew(atk, def);
    }

    public void Renew(double atk, double def)
    {
        mATK = atk;
        mDEF = def;
    }

    public void GetDamage(double damage)
    {
        if(damage < 0)
        {
            return;
        }

        if(damage > mDEF)
        {
            mHPcurrent -= damage - mDEF;
        }

        if(mHPcurrent <= 0)
        {
            mAnimator.SetBool(DieAnim, true);
            MonsterController.Instance.DeadSpawnedMonster();
        }

        UIController.Instance.ShowMonsterGaugeBar(mHPcurrent, mHPmax);
    }
}
