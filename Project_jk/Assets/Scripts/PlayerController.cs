using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    protected float mATK, mDEF, mHEAL;

    private void Start()
    {
    
    }

    private void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 15);
    }

    //private void OnMouseUp()
    //{
    //    RaycastHit hit;

    //    if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
    //    {
    //        Debug.Log(hit.collider.gameObject.name);
    //        if (hit.collider.gameObject.CompareTag("Player"))
    //        {
    //            Debug.Log(hit.collider.gameObject.name);
    //            transform.position = hit.collider.gameObject.transform.position;
    //        }
            
    //    }
    //}
}
