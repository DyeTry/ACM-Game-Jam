using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Transform weaponHolder = null;
    private GunInventory inventory;

    [SerializeField] private int tempIndex;
    private bool canEquip;

    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        WeaponSwitching weaponSwitching = GetComponent<WeaponSwitching>();
        tempIndex = weaponSwitching.weaponIndex;

        if (canEquip)
        {
            EquipGun(inventory.GetGun(tempIndex).prefab, tempIndex);
            EndEquip();
        }
    }

    private void EquipGun(GameObject gun, int gunStyle)
    {
        Gun gunObject = inventory.GetGun(gunStyle);
        Debug.Log(gunStyle);
        
        if (gunObject != null)
        {
            //Debug.Log("gunObect is not null");
            Instantiate(gun, weaponHolder);
        }
    }

    public void StartEquip() { canEquip = true; }

    public void EndEquip() { canEquip = false; }

    private void GetReferences()
    {
        inventory = GetComponent<GunInventory>();
        //if (inventory != null) Debug.Log("Inventory Found");
    }
}