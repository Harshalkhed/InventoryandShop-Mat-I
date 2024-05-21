using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryService: MonoBehaviour
{
    [SerializeField] TMP_Text inventoryWeightText;
    [SerializeField] RectTransform itemContainer;
    [SerializeField] ItemViewUI inventorySlotPrefab;
    [SerializeField] ItemDataScriptableObject itemDataScriptableObject;
    [SerializeField] Button gatherResourcesButton;
    [SerializeField] AudioSource audioSource;

    [SerializeField] int maxWeight;
    
    
    private ItemInfoPanel itemInfoPanel;
    private ItemManagePanel itemManagePanel;
    private ConfirmationPanel confirmationPanel;
    

    private List<ItemControllerUI> inventoryItems = new List<ItemControllerUI>();
    private ItemControllerUI selectedItem;
    private float inventoryWeight;

    
    private GameService gameService;
    private EventService eventService;
    private UIService uIservice;
    

    

    public void GatherRandomItem()
    {
        ItemData randomItemData = itemDataScriptableObject.GetRandomItemData();
        AddItem(randomItemData);
    }

    public void AddItem(ItemData itemToadd)
    {
        ItemControllerUI itemController = null;
        foreach (ItemControllerUI itemControllerUI in inventoryItems)
        {
            ItemData itemData = itemControllerUI.GetData();
            //if we find an item that is at its maxstack then continue on to find the same item that has not reached its max stack
            if (itemData.itemName == itemToadd.itemName)
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
                if (this.inventoryWeight + itemData.weight > this.maxWeight)
                {
                    uIservice.ShowMessage("Cannot add this item the inventory might be full!!!");
                    return;
                }
                itemData.quantity++;
                IncreaseInventoryWeight(itemData.weight);
                itemController.SetData(itemData);
                return;
            }
        }
        CreateItemSlot(itemToadd);
    }


    public void CreateItemSlot(ItemData itemData)
    {
        if (this.inventoryWeight + itemData.weight  >= this.maxWeight)
        {
            uIservice.ShowMessage("Cannot add this item the inventory might be full!!!");
            return;
        }

        ItemControllerUI itemControllerUI = new ItemControllerUI(inventorySlotPrefab);
        itemControllerUI.SetData(itemData);
        itemControllerUI.SetParent(itemContainer);
        itemControllerUI.OnItemSelected(OnItemSelected);
        inventoryItems.Add(itemControllerUI);
        IncreaseInventoryWeight(itemData.weight);
    }

    public void Init(GameService gameService,
        UIService uIservice,EventService eventService)
    {
        
        this.gameService = gameService;
        this.eventService = eventService;
        this.uIservice = uIservice;
        SetEvents();
    }

    public void SetEvents()
    {
        eventService.OnSellFromConfirmationPanel.AddListener(SellItem);
        eventService.OnBuyItem.AddListener(AddItem);
        gatherResourcesButton.onClick.AddListener(GatherRandomItem);
    }

    public void SellItem(ItemData sellingItemdata)
    {
        ItemData selecteditemData = selectedItem.GetData();
        if (selecteditemData.quantity > sellingItemdata.quantity)
        {
            selecteditemData.quantity -= sellingItemdata.quantity;
            DecreaseInventoryWeight(selecteditemData.weight);
        }
        else
        {
            inventoryItems.Remove(selectedItem);
            DecreaseInventoryWeight(selecteditemData.weight);
            selectedItem.DestroyItem();
        }
        uIservice.ShowMessage($"You gained {sellingItemdata.sellingprice} coins!!");
        selectedItem.SetData(selecteditemData);
        
        this.gameService.IncreaseCoins(sellingItemdata.sellingprice);
        this.eventService.OnSellItem.RaiseEvent(sellingItemdata);
    }

    public void OnItemSelected(ItemControllerUI itemControllerUI)
    {
        selectedItem = itemControllerUI;
        uIservice.ShowItemInfoPanel();
        eventService.OnInventoryItemSelected.RaiseEvent(selectedItem.GetData());
    }

 
    public void DecreaseInventoryWeight(float weight)
    {
        this.inventoryWeight -= weight;
        SetInventoryWeightText();
    }

    public void IncreaseInventoryWeight(float weight)
    {
        this.inventoryWeight += weight;
        this.inventoryWeight = float.Parse(this.inventoryWeight.ToString("0.00"));
        SetInventoryWeightText();
    }

    public void SetInventoryWeightText()
    {
        this.inventoryWeightText.text = $"{inventoryWeight}kg";
    }

 


}
