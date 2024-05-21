using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : MonoBehaviour
{
    public int coins { get; set; }

    [SerializeField] UIService uIService;
    [Space(10)]
    [Header("ScriptableObjects")]
    

    private EventService eventService;

    private void Start()
    {
        SetDependencies();
    }

    public void IncreaseCoins(int amount)
    {
        this.coins += amount;
        uIService.SetCoinText();
    }

    public void DecreaseCoins(int amount)
    {
        this.coins -= amount;
        uIService.SetCoinText();
    }

    private void SetDependencies()
    {
        eventService = new EventService();
        uIService.Init(this,eventService);

    }

    

}
