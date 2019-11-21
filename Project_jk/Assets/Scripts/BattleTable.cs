using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTable : MonoBehaviour
{
    private PlayerController mPlayer;

    private void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
}
