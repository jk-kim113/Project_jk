using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private PlayerController mPlayerController;

    private void Start()
    {
        mPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
}
