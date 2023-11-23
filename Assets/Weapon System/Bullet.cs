using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
    public bool OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("OnTriggerEnter Passed Check");
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}