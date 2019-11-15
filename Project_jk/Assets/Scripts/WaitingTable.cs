using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingTable : MonoBehaviour
{
    private bool IsWaiting;

    private void Awake()
    {
        IsWaiting = true;
    }

    public bool WaitingPossible()
    {
        return IsWaiting;
    }

    public void ChangeWaiting()
    {
        IsWaiting = !IsWaiting;
    }
}
