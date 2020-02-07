using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;

#pragma warning disable 0649
    [SerializeField]
    private ItemInventory mItemInventory;
    [SerializeField]
    private EquipInventory mEquipInventory;
#pragma warning restore

    private ItemData[] mItemData;
    private EquipData[] mEquipData;

    private List<Player> mPlayerList;

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

    private void OnEnable()
    {
        mItemData = LoadOriginFiles.Instance.LoadItemDataArr();
        mEquipData = LoadOriginFiles.Instance.LoadEquipDataArr();
        mPlayerList = PlayerController.Instance.PlayerSpawnedList;
    }

    private void Start()
    {
        mItemInventory.ShowItemInven(mPlayerList, mItemData);
        mEquipInventory.ShowItemInven(mPlayerList, mEquipData);
    }

    public void OpenItemInventory()
    {
        mItemInventory.gameObject.SetActive(true);
        
    }

    public void OpenEquipInventory()
    {
        mEquipInventory.gameObject.SetActive(true);
        mEquipInventory.OpenPanel();
    }

    public void ApplyConsumeItem(int playerID, int itemID)
    {
        PlayerController.Instance.ConsumeItem(playerID, mItemData[itemID].Value);
    }

    public void ApplyEquipItem(int playerID, eEquipType equiptype,int equipID)
    {
        PlayerController.Instance.EquipItem(playerID, equipID, equiptype, mEquipData[equipID].Value);
    }
}
