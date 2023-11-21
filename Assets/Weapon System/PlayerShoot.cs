using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInput;
    private bool isShooting;

    public void Update()
    {
        if (isShooting)
        {
            Shoot();
        }
    }

    public void StartShoot()
    {
        isShooting = true;
    }

    public void EndShoot()
    {
        isShooting = false;
    }

    public void Shoot()
    {
        shootInput?.Invoke();
    }

    public void Reload()
    {
        reloadInput?.Invoke();
    }
}