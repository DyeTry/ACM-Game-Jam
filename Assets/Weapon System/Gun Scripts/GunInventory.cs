using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class GunInventory : MonoBehaviour
{
    [SerializeField] private Gun[] guns;
    private InventoryManager inventoryManager;

    private void Start()
    {
        InitVariables();
    }

    public void AddGun(Gun newGun)
    {
        int index = (int)newGun.style;
        Debug.Log("Style Index = " + index);
        
        if (guns[index] != null) 
        {
            //Debug.Log("Slot Occupied");
            RemoveGun(index);
        }
        guns[index] = newGun;
        inventoryManager.StartEquip();
    }

    public void RemoveGun(int index)
    {
        guns[index] = null;
        Debug.Log("Gun Removed");
    }

    public Gun GetGun(int index)
    {
        return guns[index];
    }

    public bool FindDuplicateGun(Gun newGun)
    {
        foreach (Gun i in guns)
        {
            if (i == newGun)
            {
                return false;
            }   
        }
        return true;
    }

    private void InitVariables()
    {
        guns = new Gun[2];
        inventoryManager = GetComponent<InventoryManager>();
    }
}