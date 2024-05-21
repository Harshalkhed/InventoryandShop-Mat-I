using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManagePanel : MonoBehaviour
{
    
    [SerializeField] Button buyButton;
    [SerializeField] Button sellButton;
    [SerializeField] Button increaseAmountbutton;
    [SerializeField] Button decreaseAmountbutton;

    [SerializeField] TMP_Text itemWeightText;
    [SerializeField] TMP_Text itemCostText;
    [SerializeField] TMP_Text itemAmountText;
    [SerializeField] TMP_Text itemNameText;
    
    [SerializeField] Image itemIcon;

    
    private int totalQuantity;
    private float totalWeight;
    private int totalCost;

    private bool isSelling;
    private EventService eventService;
    private UIService uIService;

    private ItemData itemData;

    private void OnManageBuy(ItemData itemData)
    {
        isSelling = false;
        this.itemData = itemData;
        ToggleButtons(false);
        buyButton.gameObject.SetActive(true);
        this.totalQuantity = 1;
        this.totalWeight = itemData.weight;
        this.totalCost = itemData.buyingprice;
        SetItemInfoUI(itemData);
    }

    private void OnManageSell(ItemData itemData)
    {
        isSelling = true;
        this.itemData = itemData;
        ToggleButtons(false);
        sellButton.gameObject.SetActive(true);
        this.totalQuantity = 1;
        this.totalWeight = itemData.weight;
        this.totalCost = itemData.sellingprice;
        SetItemInfoUI(itemData);
    }

    public void SetItemInfoUI(ItemData itemData)
    {
        this.itemData = itemData;
        this.itemCostText.text = $"{this.totalCost}";
        this.itemNameText.text = itemData.itemName;
        this.itemAmountText.text = $"X{this.totalQuantity}";
        this.itemWeightText.text = $"{this.totalWeight}kg";
        this.itemIcon.sprite = itemData.icon;
    }

    public void Init(EventService eventService, UIService uIservice)
    {
        this.eventService = eventService;
        this.uIService = uIservice;
        this.eventService.OnSellFromInfoPanel.AddListener(OnManageSell);
        this.eventService.OnBuyFromInfoPanel.AddListener(OnManageBuy);
        this.increaseAmountbutton.onClick.AddListener(OnItemQuantityIncreased);
        this.decreaseAmountbutton.onClick.AddListener(OnItemQuantityDecreased);
        this.sellButton.onClick.AddListener(OnSellItem);
        this.buyButton.onClick.AddListener(OnBuyItem);
    }

    public void OnItemQuantityIncreased()
    {
        if (this.totalQuantity < this.itemData.quantity)
            this.totalQuantity++;
        else
            return;

        this.totalWeight += this.itemData.weight;

        if (isSelling)
            this.totalCost += this.itemData.sellingprice;
        else
            this.totalCost += this.itemData.buyingprice;

        SetItemInfoUI(this.itemData);
    }

    public void OnItemQuantityDecreased()
    {
        if (this.totalQuantity == 1)
            return;

        this.totalQuantity--;
        this.totalWeight -= this.itemData.weight;
        if (isSelling)
            this.totalCost -= this.itemData.sellingprice;
        else
            this.totalCost -= this.itemData.buyingprice;

        SetItemInfoUI(this.itemData);
    }

    public void OnSellItem()
    {
        this.itemData.sellingprice = this.totalCost;
        this.itemData.quantity = this.totalQuantity;
        this.itemData.weight = this.totalWeight;
        uIService.ShowConfirmationPanel();
        eventService.OnSellFromManagePanel.RaiseEvent(this.itemData);
        this.gameObject.SetActive(false);
    }

    public void OnBuyItem()
    {
        this.itemData.buyingprice = this.totalCost;
        this.itemData.quantity = this.totalQuantity;
        this.itemData.weight = this.totalWeight;
        uIService.ShowConfirmationPanel();
        eventService.OnBuyFromManagePanel.RaiseEvent(this.itemData);
        this.gameObject.SetActive(false);
    }

    public void ToggleButtons(bool toggle)
    {
        sellButton.gameObject.SetActive(toggle);
        buyButton.gameObject.SetActive(toggle);
    }

}
