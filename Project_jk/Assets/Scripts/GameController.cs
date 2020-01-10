using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

#pragma warning disable 0649
    [SerializeField]
    private Button mTurnExitBtn;
#pragma warning restore

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
        if (mTurnExitBtn != null)
            mTurnExitBtn.onClick.AddListener(() => {

                StartCoroutine(TurnExchange());

            });

        FieldController.Instance.SpawnField();
        MonsterController.Instance.SpawnMonster();
        PlayerController.Instance.SpawnPlayers();
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

        FieldController.Instance.FieldEffect();

        mTurnExitBtn.gameObject.SetActive(true);
    }

    public void ClearStage()
    {
        PlayerController.Instance.AddEXP();

        //UI Update
        //Field Update
    }
}