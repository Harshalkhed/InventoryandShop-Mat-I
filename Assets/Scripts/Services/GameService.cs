using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : MonoBehaviour
{
    public static GameService instance;

    [SerializeField] InventoryService inventoryService;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

}