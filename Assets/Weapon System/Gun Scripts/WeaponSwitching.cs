using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{

    [Header("Switch Weapon")]
    public int weaponIndex = 0;
    private bool canSwitch;


    private void Start()
    {
        SelectedWeapon();
    }


    private void Update()
    {
        //Debug.Log("In Update");
        if(canSwitch)
        {
            if (weaponIndex <= 0)
            {
                //Debug.Log("In If Block");
                weaponIndex = transform.childCount - 1;
            }
            else
            {
                //Debug.Log("In Else Block");
                weaponIndex--;
            }
            SelectedWeapon();
            canSwitch = false;
        }
    }

    public void SelectedWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == weaponIndex)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    public void StartSwitch() { canSwitch = true; }
}
