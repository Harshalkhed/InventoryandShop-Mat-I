using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopService : MonoBehaviour
{
    [SerializeField] RectTransform materialsScrollView;
    [SerializeField] RectTransform weaponsScrollView;
    [SerializeField] RectTransform treasuresScrollView;
    [SerializeField] RectTransform consumablesScrollView;


    [SerializeField] RectTransform materialsContainer;
    [SerializeField] RectTransform weaponsContainer;
    [SerializeField] RectTransform treasuresContainer;
    [SerializeField] RectTransform consumablesContainer;

    [SerializeField] Button materialsTab;
    [SerializeField] Button weaponsTab;
    [SerializeField] Button treasuresTab;
    [SerializeField] Button consumablesTab;

    [SerializeField] ItemDataScriptableObject itemsData;
    [SerializeField] ItemViewUI slotPrefab;
    


    
    private ItemInfoPanel itemInfoPanel;
    private ItemManagePanel itemManagePanel;
    private ConfirmationPanel confirmationPanel;
    

    private List<ItemControllerUI> shopItems = new List<ItemControllerUI>();
    private ItemControllerUI selectedItem;

    private GameService gameService;
    private EventService eventService;
    private UIService uIService;
    

    private void Start()
    {
        for(int i = 0;i<100;i++)
        {
            AddRandomItem();
        }
        materialsTab.Select();
    }

    public void AddRandomItem()
    {
        ItemData randomitemData = itemsData.GetRandomItemData();
        AddItem(randomitemData);
    }

    public void AddItem(ItemData itemToAdd)
    {
        ItemControllerUI itemController = null;
        foreach (ItemControllerUI itemControllerUI in shopItems)
        {
            ItemData itemdata = itemControllerUI.GetData();
           
            if (itemdata.itemName == itemToAdd.itemName)
            {
                if (itemdata.quantity == itemToAdd.maxStack)
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
        CreateItemSlot(itemToAdd);
    }

    public void CreateItemSlot(ItemData itemData)
    {
        ItemControllerUI itemControllerUI = new ItemControllerUI(slotPrefab);
        itemControllerUI.SetData(itemData);
        switch (itemData.itemType)
        {
            case ItemType.Materials:
                itemControllerUI.SetParent(materialsContainer);
                break;
            case ItemType.Consumables:
                itemControllerUI.SetParent(consumablesContainer);
                break;
            case ItemType.Weapons:
                itemControllerUI.SetParent(weaponsContainer);
                break;
            case ItemType.Treasure:
                itemControllerUI.SetParent(treasuresContainer);
                break;
        }

        itemControllerUI.OnItemSelected(OnItemSelected);
        shopItems.Add(itemControllerUI);
    }

    public void Init(GameService gameService
        ,UIService uIService,EventService eventService
        ,ItemInfoPanel itemInfopanel
        ,ItemManagePanel itemManagePanel,
        ConfirmationPanel confirmationpanel)
    {
        
        this.gameService = gameService;
        this.eventService = eventService;
        this.uIService = uIService;
        this.itemInfoPanel = itemInfopanel;
        this.itemManagePanel = itemManagePanel;
        this.confirmationPanel = confirmationpanel;
        SetEvents();
    }

    public void SetEvents()
    {
        eventService.OnBuyFromConfirmationPanel.AddListener(BuyItem);
        eventService.OnSellItem.AddListener(AddItem);
        materialsTab.onClick.AddListener(OnClickMaterialsTab);
        treasuresTab.onClick.AddListener(OnClickTreasuresTab);
        consumablesTab.onClick.AddListener(OnClickConsumablesTab);
        weaponsTab.onClick.AddListener(OnClickWeaponsTab);
    }

    public void OnClickMaterialsTab()
    {
        ToggleScrollViews(false);
        materialsScrollView.gameObject.SetActive(true);
    }

    public void OnClickTreasuresTab()
    {
        ToggleScrollViews(false);
        treasuresScrollView.gameObject.SetActive(true);
    }

    public void OnClickConsumablesTab()
    {
        ToggleScrollViews(false);
        consumablesScrollView.gameObject.SetActive(true);
    }

    public void OnClickWeaponsTab()
    {
        ToggleScrollViews(false);
        weaponsScrollView.gameObject.SetActive(true);
    }

    public void BuyItem(ItemData buyingitemdata)
    {
        if(buyingitemdata.buyingprice > this.gameService.coins)
        {
            uIService.ShowMessage("Not enough coins to buy this!!");
            return;
        }

        ItemData selectedItemData = selectedItem.GetData();
        if (selectedItem.GetData().quantity > buyingitemdata.quantity)
        {
            selectedItemData.quantity -= buyingitemdata.quantity;
        }
        else
        {
            shopItems.Remove(selectedItem);
            selectedItem.DestroyItem();
        }

        uIService.ShowMessage($"You bought {buyingitemdata.itemName}!!");
        selectedItem.SetData(selectedItemData);
        
        this.gameService.DecreaseCoins(buyingitemdata.buyingprice);
        this.eventService.OnBuyItem.RaiseEvent(buyingitemdata);
    }

    public void OnItemSelected(ItemControllerUI itemControllerUI)
    {
        selectedItem = itemControllerUI;
        uIService.ShowItemInfoPanel();
        eventService.OnShopItemSelected.RaiseEvent(selectedItem.GetData());
    }

    public void ToggleScrollViews(bool toggle)
    {
        materialsScrollView.gameObject.SetActive(toggle);
        weaponsScrollView.gameObject.SetActive(toggle);
        consumablesScrollView.gameObject.SetActive(toggle);
        treasuresScrollView.gameObject.SetActive(toggle);
    }

 
} 
