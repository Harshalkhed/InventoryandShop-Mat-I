using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryService : MonoBehaviour
{
    [Header("SUB PANELS")]
    [SerializeField] ItemInfoPanel itemInfoPanel;
    [SerializeField] ItemManagePanel itemManagePanel;
    [SerializeField] ConfirmationPanel confirmationPanel;
    [Space(10)]
    [SerializeField] RectTransform itemContainer;
    [SerializeField] ItemViewUI inventorySlotPrefab;
    [SerializeField] ItemDataScriptableObject itemDataScriptableObject;

    private List<ItemControllerUI> inventoryItems = new List<ItemControllerUI>();
    private EventService eventService;
    private ItemControllerUI selectedItem;

    public void Start()
    {
        AddItem();
        AddItem();
        AddItem();
        AddItem();
        AddItem();
        AddItem();
    }

    private void OnDisable()
    {
        eventService.OnSellFromInfoPanel.RemoveListener(ShowItemManagePanel);
        eventService.OnBuyFromInfoPanel.RemoveListener(ShowItemManagePanel);
    }

    public void AddItem()
    {
        ItemData itemData = itemDataScriptableObject.GetRandomItemData();
        ItemControllerUI itemControllerUI = new ItemControllerUI(inventorySlotPrefab);
        itemControllerUI.SetData(itemData);
        itemControllerUI.SetParent(itemContainer);
        itemControllerUI.OnItemSelected(OnItemSelected);
        inventoryItems.Add(itemControllerUI);
    }


    public void Init(EventService eventService)
    {
        this.eventService = eventService;
        SetEvents();
        itemInfoPanel.Init(eventService);
        itemManagePanel.Init(eventService);
        confirmationPanel.Init(eventService);
    }

    public void SetEvents()
    {
        eventService.OnSellFromInfoPanel.AddListener(ShowItemManagePanel);
        eventService.OnSellFromManagePanel.AddListener(ShowConfirmationPanel);
        eventService.OnSellItem.AddListener(SellItem);
    }

    public void SellItem(ItemData sellingItemdata)
    {
        ItemData selecteditemData = selectedItem.GetData();
        if (selecteditemData.quantity > sellingItemdata.quantity)
        {
            selecteditemData.quantity -= sellingItemdata.quantity;
        }
        else
        {
            inventoryItems.Remove(selectedItem);
            selectedItem.DestroyItem();
        }

        selectedItem.SetData(selecteditemData);

    }

    public void ShowItemManagePanel(ItemData itemData)
    {
        itemManagePanel.SetItemInfoUI(itemData);
        itemManagePanel.gameObject.SetActive(true);
    }

    public void OnItemSelected(ItemControllerUI itemControllerUI)
    {
        selectedItem = itemControllerUI;
        ShowInfoPanel(selectedItem.GetData());
    }

    public void ShowInfoPanel(ItemData itemData)
    {
        itemInfoPanel.SetItemInfo(itemData, true);
        itemInfoPanel.gameObject.SetActive(true);
    }

    public void ShowConfirmationPanel(ItemData itemData)
    {
        confirmationPanel.SetItemData(itemData);
        confirmationPanel.SetSellMessageText(itemData);
        confirmationPanel.gameObject.SetActive(true);
    }


}