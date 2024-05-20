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


    public void SetItemInfo(ItemData itemData, bool isSelling)
    {
        ToggleButtons(isSelling);

        if (isSelling)
            this.itemCostText.text = $"Cost - {itemData.sellingprice}";
        else
            this.itemCostText.text = $"Cost - {itemData.buyingprice}";

        this.itemDescriptionText.text = itemData.description;
        this.itemNameText.text = itemData.itemName;
        this.itemAmountText.text = $"X{itemData.quantity}";
        this.itemWeightText.text = $"Weight - {itemData.weight}kg";
        this.itemIcon.sprite = itemData.icon;
    }

    public void OnCloseButtonClicked() => this.gameObject.SetActive(false);


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