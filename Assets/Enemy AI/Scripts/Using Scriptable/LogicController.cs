using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LogicController : MonoBehaviour
{

    Transform target;
    NavMeshAgent agent;

    private void Start()
    {
        GameObject playerObject = GameObject.Find("Player");

        if (playerObject != null)
        {
            target = playerObject.transform;
            agent = GetComponent<NavMeshAgent>();
        }
    }

    private void Update()
    {
        if (target == null) return;
    }

    public void WalkToPlayer()
    {
        float distance = Vector3.Distance(target.position, transform.position);
    }

    private void FaceTarget()
    {

    }
}
