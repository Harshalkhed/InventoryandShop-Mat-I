using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : MonoBehaviour
{
    // [SerializeField] InventoryService inventoryService;
    [SerializeField]
    UIService uIService;
    private EventService eventService;


    private void Start()
    {
        SetDependencies();
    }

    private void SetDependencies()
    {
        eventService = new EventService();
        uIService.Init(eventService);
        
    }



}