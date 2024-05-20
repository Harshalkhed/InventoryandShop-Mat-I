using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemControllerUI
{
    private ItemViewUI itemViewUI;
    private ItemData itemData;

    public ItemControllerUI(ItemViewUI itemViewUI)
    {
        this.itemViewUI = Object.Instantiate(itemViewUI);
        this.itemViewUI.SetController(this);
    }
    public void SetParent(RectTransform transform)
    {
        itemViewUI.transform.SetParent(transform, false);
    }

    public void OnItemSelected(UnityAction<ItemData> onItemSelect)
    {
        itemViewUI.itemButton.onClick.AddListener(delegate { onItemSelect(this.itemData); });
    }

    public void SetData(ItemData itemData)
    {
        this.itemData = itemData;
        itemViewUI.Init(itemData);
    }

    public ItemData GetData() => itemData;
}