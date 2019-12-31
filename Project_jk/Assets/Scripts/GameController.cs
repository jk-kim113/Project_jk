using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField]
    private BattleTable[] mBattleTable;

    [SerializeField]
    private Button mTurnExitBtn;

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

    void Start()
    {
        MonsterController.Instance.MonsterSpawn();

        if (mTurnExitBtn != null)
            mTurnExitBtn.onClick.AddListener(() => {

                StartCoroutine(TurnExchange());

            });
    }

    private IEnumerator TurnExchange()
    {
        WaitForSeconds onetime = new WaitForSeconds(1);

        mTurnExitBtn.gameObject.SetActive(false);

        yield return onetime;

        MonsterController.Instance.GetDamage(PlayerController.Instance.TotalAtk);

        yield return onetime;

        PlayerController.Instance.GetDamage(MonsterController.Instance.SpawnedMonsterAttack());

        PlayerController.Instance.NextBattleType();

        mTurnExitBtn.gameObject.SetActive(true);
    }

    public void ClearStage()
    {
        PlayerController.Instance.AddEXP();
        //UI Update
    }
}