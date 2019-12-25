using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private string mName;
    private int mID;
    private double mATK, mDEF, mHPmax, mHPcurrent;
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

    public void Initialize(string name, int id, double atk, double def, double hpmax, double hpcurrent)
    {
        mName = name;
        mID = id;
        mATK = atk;
        Renew(atk, def, hpmax, hpcurrent);
    }

    public void Renew(double atk, double def, double hpmax, double hpcurrent)
    {
        mATK = atk;
        mDEF = def;
        mHPmax = hpmax;
        mHPcurrent = hpcurrent;
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
        }

        UIController.Instance.ShowMonsterGaugeBar(mHPcurrent, mHPmax);
    }
}
