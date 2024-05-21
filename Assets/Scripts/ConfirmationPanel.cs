using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationPanel : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text messageText;
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;

    private EventService eventService;
    private ItemData itemData;
    private bool isSelling;

    private void OnYesButtonClicked()
    {
        if (isSelling)
            eventService.OnSellFromConfirmationPanel.RaiseEvent(this.itemData);
        else
            eventService.OnBuyFromConfirmationPanel.RaiseEvent(this.itemData);

        this.gameObject.SetActive(false);
    }

    public void Init(EventService eventService)
    {
        this.eventService = eventService;
        this.eventService.OnSellFromManagePanel.AddListener(ShowSellConfirmation);
        this.eventService.OnBuyFromManagePanel.AddListener(ShowBuyConfirmation);
        yesButton.onClick.AddListener(OnYesButtonClicked);
        noButton.onClick.AddListener(OnNoButtonClicked);
    }


    public void ShowBuyConfirmation(ItemData itemData)
    {
        this.itemData = itemData;
        this.isSelling = false;
        SetBuyMessageText();
    }

    public void ShowSellConfirmation(ItemData itemData)
    {
        this.itemData = itemData;
        this.isSelling = true;
        SetSellMessageText();
    }

    public void SetBuyMessageText()
    {
        messageText.text = $"Do you want to buy X{this.itemData.quantity} of {this.itemData.itemName} for {this.itemData.buyingprice}";
    }

    public void SetSellMessageText()
    {
        messageText.text = $"Do you want to sell X{this.itemData.quantity} of {this.itemData.itemName} for {this.itemData.sellingprice}";
    }

    private void OnNoButtonClicked() => this.gameObject.SetActive(false);


}
