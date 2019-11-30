using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    protected float mATK, mDEF, mHEAL;

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
