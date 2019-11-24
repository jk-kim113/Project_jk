using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTable : MonoBehaviour
{
    private bool IsPlayer;

    private void Awake()
    {
        IsPlayer = true;
    }

    public bool GetIsPlayer()
    {
        return IsPlayer;
    }

    public void StateChange()
    {
        IsPlayer = !IsPlayer;
    }
}
