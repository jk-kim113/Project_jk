using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private PlayerController mPlayerController;

    [SerializeField]
    private Button mMoveToBattleBtn;

    private void Start()
    {
        mPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void MoveToBattle(Vector3 pos)
    {
        if(mMoveToBattleBtn.gameObject.activeInHierarchy)
        {
            return;
        }
        mMoveToBattleBtn.gameObject.SetActive(true);
        mMoveToBattleBtn.gameObject.transform.position = pos + Vector3.down * 100;
    }
}
