using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

#pragma warning disable 0649
    [SerializeField]
    private Button mTurnExitBtn;
    [SerializeField]
    private Image mSceneExitPanel;
    [SerializeField]
    private Image mTurnExitPanel;
    [SerializeField]
    private Toggle mAutoTurnToggle;
    [SerializeField]
    private BattleTable[] mBattleTable;
#pragma warning restore
    
    public int StageLevel { get { return SaveLoadData.Instance.SaveData.StageLevel; } }

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

        mSceneExitPanel.gameObject.SetActive(false);
        mTurnExitPanel.gameObject.SetActive(false);
        mAutoTurnToggle.isOn = false;
    }

    void Start()
    {
        if (mTurnExitBtn != null)
            mTurnExitBtn.onClick.AddListener(() => {

                StartCoroutine(TurnExchange());

            });

        StartCoroutine(LoadData());
        SpawnInGame();
    }

    public void SpawnInGame()
    {
        for(int i = 0; i < mBattleTable.Length; i++)
        {
            mBattleTable[i].ResetGame();
        }

        PlayerController.Instance.SetActiveFalse();
        MonsterController.Instance.SetActiveFalse();

        CardEffectController.Instance.SpawnCardEffect();
        FieldController.Instance.SpawnField();
        MonsterController.Instance.SpawnMonster();
        PlayerController.Instance.SpawnPlayers();

        mTurnExitPanel.gameObject.SetActive(false);

        UIController.Instance.ShowStageLevel(StageLevel);
    }

    private IEnumerator LoadData()
    {
        WaitForSeconds one = new WaitForSeconds(.1f);

        while(!PlayerController.Instance.IsSpawnFinish || !MonsterController.Instance.IsSpawnFinish)
        {
            yield return one;
        }

        SaveLoadData.Instance.Load();
        PlayerController.Instance.Load(SaveLoadData.Instance.SaveData.PlayerLevel);
        MonsterController.Instance.Load(
            SaveLoadData.Instance.SaveData.MonsterAttack,
            SaveLoadData.Instance.SaveData.MonsterDefend,
            SaveLoadData.Instance.SaveData.MonsterHPmax);
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

        CardEffectController.Instance.NextCardEffect();
    }

    public void ClearStage()
    {
        SaveLoadData.Instance.SaveData.StageLevel++;
        PlayerController.Instance.AddEXP();
        //UI Update
        //Field Update

        if (!mAutoTurnToggle.isOn)
        {
            mTurnExitPanel.gameObject.SetActive(true);
        }
        else
        {
            SpawnInGame();
        }
    }

    public void SceneExitButton()
    {
        mSceneExitPanel.gameObject.SetActive(true);
    }

    public void SceneExitYesBtn()
    {
        SaveLoadData.Instance.Save();

        SceneManager.LoadScene("Lobby");
    }

    public void SceneExitNoBtn()
    {
        mSceneExitPanel.gameObject.SetActive(false);
    }
}