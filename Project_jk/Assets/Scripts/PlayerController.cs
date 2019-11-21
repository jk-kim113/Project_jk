using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    protected float mATK, mDEF, mHEAL;

    private Transform mBattletablePos;
    private Transform mStartPos;

    private void Start()
    {
        mStartPos = transform;
    }

    private void Update()
    {
        RaycastHit hit;
        Vector3 mPos = Camera.main.WorldToScreenPoint(transform.position);

        if (Physics.Raycast(Camera.main.ScreenPointToRay(mPos), out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("Monster"))
            {
                mBattletablePos = hit.collider.transform;
            }
            else
            {
                mBattletablePos = null;
            }
            Debug.Log(mBattletablePos);
        }
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 40);
    }

    private void OnMouseUp()
    {
        if(mBattletablePos == null)
        {
            transform.position = mStartPos.position;
        }
        else
        {
            transform.position = mBattletablePos.position;
        }
    }
}
