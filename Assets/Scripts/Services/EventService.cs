using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService
{
    
    public EventController<ItemData> OnSellFromInfoPanel;
    public EventController<ItemData> OnBuyFromInfoPanel;



    public EventController<ItemData> OnSellFromManagePanel;
    public EventController<ItemData> OnBuyFromManagePanel;



    public EventController<ItemData> OnSellFromConfirmationPanel;
    public EventController<ItemData> OnBuyFromConfirmationPanel;


    public EventController<ItemData> OnShopItemSelected;
    public EventController<ItemData> OnInventoryItemSelected;

    public EventController<ItemData> OnSellItem;
    public EventController<ItemData> OnBuyItem;
    

    public EventService()
    {
        OnSellFromInfoPanel = new EventController<ItemData>();
        OnBuyFromInfoPanel = new EventController<ItemData>();
        OnSellFromManagePanel = new EventController<ItemData>();
        OnBuyFromManagePanel = new EventController<ItemData>();
        OnSellFromConfirmationPanel = new EventController<ItemData>();
        OnSellItem = new EventController<ItemData>();
        OnBuyItem = new EventController<ItemData>();
        OnBuyFromConfirmationPanel = new EventController<ItemData>();
        OnShopItemSelected = new EventController<ItemData>();
        OnInventoryItemSelected = new EventController<ItemData>();
    }


}
