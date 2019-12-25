using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{
    public static RayController Instance;

    private BattleTable mBattleTable;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public BattleTable DetectBattleTable(Player player)
    {
        player.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 70);

        RaycastHit hit;

        if (Physics.Raycast(player.transform.position, player.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition), out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("BattleTable"))
            {
                mBattleTable = hit.collider.gameObject.GetComponent<BattleTable>();
                if (mBattleTable.PlayerIsHere() && player.GetPlayerState() == ePlayerState.Battle)
                {
                    mBattleTable.StateChange();
                }
            }
            else
            {
                mBattleTable = null;
            }
        }

        return mBattleTable;
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                hit.collider.gameObject.GetComponent<Player>().RaySelected();
            }
            else
            {
                UIController.Instance.OffPlayerInfo();
            }
        }
    }
}
