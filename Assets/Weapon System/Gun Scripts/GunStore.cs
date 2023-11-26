using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStore : MonoBehaviour
{

    public PlayerCurrency playerCurrency;
    private GunInventory instance;

    void Awake()
    {
        GetReferences();
    }

    public void PurchaseWeapon(Gun gun)
    {
        if (instance.FindDuplicateGun(gun))
        {
            if (playerCurrency.GetCurrency() >= gun.cost)
            {
                if (instance != null) Debug.Log("Called instance in GunStore");
                instance.AddGun(gun);

                playerCurrency.currency -= gun.cost;
                Debug.Log("Successful Purchase");
            }
        }
        else
        {
            Debug.Log("Duplciate gun found");
            //playerCurrency.currency -= gun.cost;
            //Code to restore ammo
        }
    }

    private void GetReferences()
    {
        playerCurrency = FindObjectOfType<PlayerCurrency>();
        instance = FindObjectOfType<GunInventory>();
    }
}