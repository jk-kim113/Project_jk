using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCurrentState
{
    Attack,
    Defend,
    Heal
}

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    protected float mATK, mDEF, mHEAL;

    [SerializeField]
    private eCurrentState eCurrentState;

    private Transform mBattletablePos;
    private Vector3 mStartPos;

    private bool IsBattlePos;

    private BattleTable mBattleTable;

    private void Start()
    {
        mStartPos = transform.position;
        IsBattlePos = false;
    }
    
    public bool GetIsBattlePos()
    {
        return IsBattlePos;
    }

    public void RaySelected()
    {
        UIController.Instance.ShowPlayerInfo("atk", mATK, mDEF, mHEAL);
    }

    public void NextState()
    {
        eCurrentState++;
        if ((int)eCurrentState > 2)
        {
            eCurrentState = 0;
        }

        switch (eCurrentState)
        {
            case eCurrentState.Attack:
                
                break;
            case eCurrentState.Defend:
                
                break;
            case eCurrentState.Heal:
                
                break;
            default:
                Debug.LogError("Wrong State : " + eCurrentState);
                break;
        }
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 70);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition), out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("BattleTable"))
            {   
                mBattletablePos = hit.collider.transform;
                mBattleTable = hit.collider.gameObject.GetComponent<BattleTable>();

                if(!mBattleTable.GetIsPlayer() && IsBattlePos)
                {
                    mBattleTable.StateChange();
                }
            }
            else
            {
                mBattletablePos = null;
            }
        }
    }

    private void OnMouseUp()
    {
        if(mBattletablePos == null)
        {
            transform.position = mStartPos;
            IsBattlePos = false;
        }
        else
        {
            if(mBattleTable.GetIsPlayer())
            {
                transform.position = mBattletablePos.position + Vector3.up * 1.5f;
                IsBattlePos = true;
                mBattleTable.StateChange();
            }
            else
            {
                transform.position = mStartPos;
                IsBattlePos = false;
            }
        }
    }
}
