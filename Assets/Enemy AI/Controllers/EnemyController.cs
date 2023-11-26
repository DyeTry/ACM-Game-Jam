using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Info")]
    public new string name;

    [Header("Controller Info")]
    public float lookRadius;

    private Transform target;
    private NavMeshAgent agent;

    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");

        if (playerObject != null)
        {
            target = playerObject.transform;
            agent = GetComponent<NavMeshAgent>();
        }
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        { 
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance) FaceTarget();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}