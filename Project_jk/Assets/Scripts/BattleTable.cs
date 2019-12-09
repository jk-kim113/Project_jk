using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTable : MonoBehaviour
{
    private bool IsPlayer;

    private void Awake()
    {
        IsPlayer = false;
        Debug.Log(gameObject.name + IsPlayer);
    }

    public bool PlayerIsHere()
    {
        return IsPlayer;
    }

    public void StateChange()
    {
        IsPlayer = !IsPlayer;
        Debug.Log(gameObject.name + IsPlayer);
    }
}
