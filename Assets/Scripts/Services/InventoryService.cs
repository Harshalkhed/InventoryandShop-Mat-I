using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryService : MonoBehaviour
{

    private ItemInfoPanel itemInfoPanel;
    private ItemManagePanel itemManagePanel;
    private ConfirmationPanel confirmationPanel;

    [SerializeField] RectTransform itemContainer;
    [SerializeField] ItemViewUI inventorySlotPrefab;
    [SerializeField] ItemDataScriptableObject itemDataScriptableObject;
    [SerializeField] Button gatherResourcesButton;
    [SerializeField] TMPro.TMP_Text inventoryWeightText;

    private List<ItemControllerUI> inventoryItems = new List<ItemControllerUI>();
    private EventService eventService;
    private ItemControllerUI selectedItem;
    private int inventoryWeight;

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
        ItemData randomItemData = itemDataScriptableObject.GetRandomItemData();
        ItemControllerUI itemController = null;
        foreach (ItemControllerUI itemControllerUI in inventoryItems)
        {
            ItemData itemData = itemControllerUI.GetData();
          
            if (itemData.itemName == randomItemData.itemName)
            {
                if (itemData.quantity == itemData.maxStack)
                {
                    continue;
                }
                else
                {
                    itemController = itemControllerUI;
                    break;
                }

            }
        }


        if (itemController != null)
        {
            ItemData itemData = itemController.GetData();
            if (itemData.isStackable && itemData.quantity < itemData.maxStack)
            {
                itemData.quantity++;
                itemController.SetData(itemData);
                return;
            }
        }
        CreateItemSlot(randomItemData);
    }

    public void CreateItemSlot(ItemData itemData)
    {
        ItemControllerUI itemControllerUI = new ItemControllerUI(inventorySlotPrefab);
        itemControllerUI.SetData(itemData);
        itemControllerUI.SetParent(itemContainer);
        itemControllerUI.OnItemSelected(OnItemSelected);
        inventoryItems.Add(itemControllerUI);
        IncreaseInventoryWeight(itemData.weight);
    }

    public void SetInventoryWeightText()
    {
        this.inventoryWeightText.text = $"{inventoryWeight}kg";
    }

    public void Init(EventService eventService, ItemInfoPanel itemInfoPanel,
        ItemManagePanel itemManagePanel,
        ConfirmationPanel confirmationPanel)
    {
        this.eventService = eventService;
        this.itemInfoPanel = itemInfoPanel;
        this.itemManagePanel = itemManagePanel;
        this.confirmationPanel = confirmationPanel;
        SetEvents();
    }

    public void SetEvents()
    {
        eventService.OnSellFromInfoPanel.AddListener(ShowItemManagePanel);
        eventService.OnSellFromManagePanel.AddListener(ShowConfirmationPanel);
        eventService.OnSellItem.AddListener(SellItem);
        gatherResourcesButton.onClick.AddListener(GatherResources);
    }

    public void SellItem(ItemData sellingItemdata)
    {
        ItemData selecteditemData = selectedItem.GetData();
        if (selecteditemData.quantity > sellingItemdata.quantity)
        {
            selecteditemData.quantity -= sellingItemdata.quantity;
            IncreaseInventoryWeight(selecteditemData.weight);
        }
        else
        {
            inventoryItems.Remove(selectedItem);
            DecreaseInventoryWeight(selecteditemData.weight);
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

    public void GatherResources() => AddItem();


    public void DecreaseInventoryWeight(int weight)
    {
        this.inventoryWeight -= weight;
        SetInventoryWeightText();
    }

    public void IncreaseInventoryWeight(int weight)
    {
        this.inventoryWeight += weight;
        SetInventoryWeightText();
    }


}