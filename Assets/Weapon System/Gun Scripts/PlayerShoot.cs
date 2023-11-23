using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action reloadInput;

    public void Reload()
    {
        reloadInput?.Invoke();
    }
    
}