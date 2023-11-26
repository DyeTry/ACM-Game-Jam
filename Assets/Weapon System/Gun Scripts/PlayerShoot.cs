using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;
    private Gun gun;
    private WeaponSwitching switching;

    public void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        gun = GetComponent<Gun>();
        switching = GetComponentInParent<WeaponSwitching>();

        onFoot.Shoot.started += ctx => StartShoot();
        onFoot.Shoot.canceled += ctx => EndShoot();
        onFoot.Reload.performed += ctx => StartReload();

        onFoot.Switch.performed += xtx => StartSwitch();
    }

    public void StartShoot()
    {
        //Debug.Log("Shooting is True");
        gun.StartShoot();
    }

    public void EndShoot()
    {
        //Debug.Log("Shooting is False");
        gun.EndShoot();
    }

    public void StartReload()
    {
        //Debug.Log("Reloading");
        gun.StartReload();
    }

    public void StartSwitch()
    {
        //Debug.Log("Switching");
        switching.StartSwitch();
    }

    private void OnEnable() { onFoot.Enable(); }

    private void OnDisable() { onFoot.Disable(); }    
}