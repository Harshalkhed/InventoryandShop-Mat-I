using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryService : MonoBehaviour
{
    [SerializeField] ItemInfoPanel itemInfoPanel;
    [SerializeField] RectTransform itemContainer;
    [SerializeField] ItemViewUI inventorySlotPrefab;
    [SerializeField] ItemDataScriptableObject itemDataScriptableObject;

    private List<ItemControllerUI> inventoryItems = new List<ItemControllerUI>();

    public void Start()
    {
        AddItem();
    }

    public void AddItem()
    {
        ItemData itemData = itemDataScriptableObject.GetItemData("Necklace");
        ItemControllerUI itemControllerUI = new ItemControllerUI(inventorySlotPrefab);
        itemControllerUI.SetData(itemData);
        itemControllerUI.SetParent(itemContainer);
        itemControllerUI.OnItemSelected(ShowInfoPanel);
        inventoryItems.Add(itemControllerUI);
    }

    public void ShowInfoPanel(ItemData itemData)
    {
        itemInfoPanel.SetItemInfo(itemData, true);
        itemInfoPanel.gameObject.SetActive(true);
    }

}