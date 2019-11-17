using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private PlayerController[] mPlayerController;

    [SerializeField]
    private Transform[] mWaitingTable;

    [SerializeField]
    private StateController[] mStateControl;

    private Monster mMonster;

    [SerializeField]
    private Button mTurnExitBtn;

    void Start()
    {   
        mMonster = GameObject.FindGameObjectWithTag("Monster").GetComponent<Monster>();

        for(int i = 0; i < mPlayerController.Length; i++)
        {
            mPlayerController[i].transform.position = mWaitingTable[i].transform.position + Vector3.up * 1.5f;
        }

        if (mTurnExitBtn != null)
            mTurnExitBtn.onClick.AddListener(() => {

                StartCoroutine(TurnExchange());

            });
    }

    private IEnumerator TurnExchange()
    {
        mTurnExitBtn.gameObject.SetActive(false);

        yield return new WaitForSeconds(1);

        
        yield return new WaitForSeconds(1);

        for(int i = 0; i < mStateControl.Length; i++)
        {
            mStateControl[i].NextState();
        }

        mTurnExitBtn.gameObject.SetActive(true);
    }
}