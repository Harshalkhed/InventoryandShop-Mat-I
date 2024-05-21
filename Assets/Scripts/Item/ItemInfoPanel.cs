using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class ItemInfoPanel : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text itemNameText;
    [SerializeField] TMP_Text itemDescriptionText;
    [SerializeField] TMP_Text itemWeightText;
    [SerializeField] TMP_Text itemAmountText;
    [SerializeField] TMP_Text itemCostText;
    [SerializeField] Button sellButton;
    [SerializeField] Button buyButton;

    private EventService eventService;
    private UIService uiService;
    private ItemData itemData;

    public void Init(EventService eventService, UIService uIservice)
    {
        this.eventService = eventService;
        this.uiService = uIservice;
        this.eventService.OnInventoryItemSelected.AddListener(OnInventoryItemSelected);
        this.eventService.OnShopItemSelected.AddListener(OnShopItemSelected);
        sellButton.onClick.AddListener(OnSellButtonClicked);
        buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    public void OnInventoryItemSelected(ItemData itemData)
    {
        this.itemData = itemData;
        ToggleButtons(true);
        this.itemCostText.text = $"{itemData.sellingprice}";
        this.itemDescriptionText.text = itemData.description;
        this.itemNameText.text = itemData.itemName;
        this.itemAmountText.text = $"X{itemData.quantity}";
        this.itemWeightText.text = $"{itemData.weight}kg";
        this.itemIcon.sprite = itemData.icon;
    }

    public void OnShopItemSelected(ItemData itemData)
    {
        this.itemData = itemData;
        ToggleButtons(false);
        this.itemCostText.text = $"{itemData.buyingprice}";
        this.itemDescriptionText.text = itemData.description;
        this.itemNameText.text = itemData.itemName;
        this.itemAmountText.text = $"X{itemData.quantity}";
        this.itemWeightText.text = $"{itemData.weight}kg";
        this.itemIcon.sprite = itemData.icon;
    }

    public void SetItemInfo(ItemData itemData, bool isSelling)
    {
        this.itemData = itemData;
        ToggleButtons(isSelling);

        if (isSelling)
            this.itemCostText.text = $"{itemData.sellingprice}";
        else
            this.itemCostText.text = $"{itemData.buyingprice}";

        this.itemDescriptionText.text = itemData.description;
        this.itemNameText.text = itemData.itemName;
        this.itemAmountText.text = $"X{itemData.quantity}";
        this.itemWeightText.text = $"{itemData.weight}kg";
        this.itemIcon.sprite = itemData.icon;
    }

    public void OnCloseButtonClicked() => this.gameObject.SetActive(false);

    public void OnSellButtonClicked()
    {
        uiService.ShowItemManagePanel();
        eventService.OnSellFromInfoPanel.RaiseEvent(this.itemData);
        this.gameObject.SetActive(false);
    }

    public void OnBuyButtonClicked()
    {
        uiService.ShowItemManagePanel();
        eventService.OnBuyFromInfoPanel.RaiseEvent(this.itemData);
        this.gameObject.SetActive(false);
    }

    public void ToggleButtons(bool isSelling)
    {
        sellButton.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(false);

        if (isSelling)
            sellButton.gameObject.SetActive(true);
        else
            buyButton.gameObject.SetActive(true);

    }
}
