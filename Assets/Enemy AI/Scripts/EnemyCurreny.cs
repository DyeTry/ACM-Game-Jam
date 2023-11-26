using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCurreny : MonoBehaviour
{
    [Header("Currency Info")]
    public int currencyDrop;
    private PlayerCurrency playerCurrency;

    void Start()
    {
        playerCurrency = FindObjectOfType<PlayerCurrency>();

        if (playerCurrency == null) Debug.Log("Player not Found");
    }


    void Update()
    {

    }

    public void DropCurrency(int money)
    {
        if (playerCurrency != null)
        {;
            playerCurrency.AddMoney(money);
        }
    }
}