using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{

    [Header("Currency Info")]
    public int currency;
    public TextMeshProUGUI currencyText;

    void Start()
    {
        
    }


    void Update()
    {
        currencyText.text = currency.ToString();
    }

    public void AddMoney(int amount)
    {
        //Debug.Log("Added to Player");
        currency += amount;
        currencyText.text = currency.ToString();
    }

    public void SendMoney()
    {
        
    }

    public int GetCurrency() { return currency; }
}