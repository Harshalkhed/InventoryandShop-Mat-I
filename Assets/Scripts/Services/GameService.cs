using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] InventoryService inventoryService;
    private EventService eventService;

    private void Start()
    {
        SetDependencies();
    }

    private void SetDependencies()
    {
        eventService = new EventService();
        inventoryService.Init(eventService);
    }



}