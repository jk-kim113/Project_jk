using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    protected float mATK, mDEF, mHEAL;

    private Transform mBattletablePos;
    private Vector3 mStartPos;

    private void Start()
    {
        mStartPos = transform.position;
    }

    private void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition), out hit ,Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("Monster"))
            {
                mBattletablePos = hit.collider.transform;
            }
            else
            {
                mBattletablePos = null;
            }
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
            transform.position = mStartPos;
        }
        else
        {
            transform.position = mBattletablePos.position + Vector3.up * 1.5f;
        }
    }
}
