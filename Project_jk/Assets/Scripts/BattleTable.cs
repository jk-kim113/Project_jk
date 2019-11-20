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

    private void OnMouseUp()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.CompareTag("Monster"))
            {
                Debug.Log(hit.collider.gameObject.name);
                mPlayer.transform.position = hit.collider.gameObject.transform.position;
            }

        }
    }
}
