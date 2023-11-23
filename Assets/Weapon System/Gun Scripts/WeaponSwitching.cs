using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{

    [Header("Switch Weapon")]
    [SerializeField] private int weaponIndex = 0;


    private void Start()
    {
        SelectedWeapon();
    }


    private void Update()
    {

        int previousSelectedWeapon = weaponIndex;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (weaponIndex >= transform.childCount - 1)
            {
                weaponIndex = 0;
            }
            else
            {
                weaponIndex++;
            }
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (weaponIndex <= 0)
            {
                weaponIndex = transform.childCount - 1;
            }
            else
            {
                weaponIndex--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {weaponIndex = 0;}
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 1) {weaponIndex = 1;}

        if (previousSelectedWeapon != weaponIndex)
        {
            SelectedWeapon();
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
}
