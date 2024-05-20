using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManagePanel : MonoBehaviour
{
    [Header("BUTTONS")]
    [SerializeField] Button buyButton;
    [SerializeField] Button sellButton;
    [SerializeField] Button increaseAmountbutton;
    [SerializeField] Button decreaseAmountbutton;

    [Space(10)]
    [Header("TEXT")]
    [SerializeField] TMP_Text itemWeightText;
    [SerializeField] TMP_Text itemCostText;
    [SerializeField] TMP_Text itemAmountText;
    [SerializeField] TMP_Text itemNameText;
    [Space(10)]
    [SerializeField] Image itemIcon;

    private EventService eventService;
    private ItemData itemData;
    private bool isSelling;

    private void OnManageBuy(ItemData itemData)
    {
        this.itemData = itemData;
        isSelling = false;
        ToggleButtons(false);
        buyButton.gameObject.SetActive(true);
    }

    private void OnManageSell(ItemData itemData)
    {
        this.itemData = itemData;
        isSelling = true;
        ToggleButtons(false);
        sellButton.gameObject.SetActive(true);
    }

    public void SetItemInfoUI(ItemData itemData)
    {
        this.itemData = itemData;
        if (isSelling)
            this.itemCostText.text = $"{itemData.sellingprice}";
        else
            this.itemCostText.text = $"{itemData.buyingprice}";

        this.itemNameText.text = itemData.itemName;
        this.itemAmountText.text = $"X{itemData.quantity}";
        this.itemWeightText.text = $"{itemData.weight}kg";
        this.itemIcon.sprite = itemData.icon;
    }

    public void Init(EventService eventService)
    {
        this.eventService = eventService;
        this.eventService.OnSellFromInfoPanel.AddListener(OnManageSell);
        this.eventService.OnBuyFromInfoPanel.AddListener(OnManageBuy);
        this.increaseAmountbutton.onClick.AddListener(OnItemQuantityIncreased);
        this.decreaseAmountbutton.onClick.AddListener(OnItemQuantityDecreased);
        this.sellButton.onClick.AddListener(OnSellItem);
        this.buyButton.onClick.AddListener(OnBuyItem);
    }

    public void OnItemQuantityIncreased()
    {
        this.itemData.quantity++;
        this.itemData.weight += this.itemData.weight;

        if (isSelling)
            this.itemData.sellingprice += this.itemData.sellingprice;
        else
            this.itemData.buyingprice += this.itemData.buyingprice;

        SetItemInfoUI(this.itemData);
    }

    public void OnItemQuantityDecreased()
    {
        if (this.itemData.quantity == 1)
            return;

        this.itemData.quantity--;
        this.itemData.weight -= this.itemData.weight;

        if (isSelling)
            this.itemData.sellingprice -= this.itemData.sellingprice;
        else
            this.itemData.buyingprice -= this.itemData.buyingprice;

        SetItemInfoUI(this.itemData);
    }

    public void OnSellItem()
    {
        this.gameObject.SetActive(false);
        eventService.OnSellFromManagePanel.RaiseEvent(itemData);
    }

    public void OnBuyItem()
    {
        this.gameObject.SetActive(false);
        eventService.OnBuyFromManagePanel.RaiseEvent(itemData);
    }

    public void ToggleButtons(bool toggle)
    {
        sellButton.gameObject.SetActive(toggle);
        buyButton.gameObject.SetActive(toggle);
    }

}