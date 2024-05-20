using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemViewUI : MonoBehaviour
{
    public Button itemButton;

    [SerializeField] Image iconImage;
    [SerializeField] TMP_Text quantityText;
    private ItemControllerUI itemControllerUI;

    public void SetController(ItemControllerUI itemController)
    {
        this.itemControllerUI = itemController;
    }


    public void Init(ItemData itemData)
    {
        this.iconImage.sprite = itemData.icon;
        this.quantityText.text = $"X{itemData.quantity}";
    }

}