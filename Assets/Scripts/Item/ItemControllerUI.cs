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
        this.itemData = new ItemData();
        this.itemViewUI.SetController(this);
    }
    public void SetParent(RectTransform transform)
    {
        itemViewUI.transform.SetParent(transform, false);
    }

    public void OnItemSelected(UnityAction<ItemControllerUI> onItemSelect)
    {
        itemViewUI.itemButton.onClick.AddListener(delegate { onItemSelect(this); });
    }

    public void SetData(ItemData itemData)
    {
        this.itemData = itemData;
        itemViewUI.SetItemUI(this.itemData);
    }
    public void DestroyItem()
    {
        Object.Destroy(this.itemViewUI.gameObject);
    }
    public ItemData GetData() => this.itemData;
}