using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LogicAttack : MonoBehaviour
{
    [Header("Attack Info")]
    public float attackRadius;
    public float damage;
    public float timer;
    public GameObject projectile;
    public float shootSpeed;
    public float shootRange;

    [Header("Bullet Projectile")]
    public Transform bulletSpawnPoint;
    private float bulletTime;
    private GameObject bulletObject;
    private Rigidbody bulletBody;

    private Transform target;
    private NavMeshAgent agent;
    private float distance;

    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");

        if (playerObject != null)
        {
            target = playerObject.transform;
            agent = GetComponent<NavMeshAgent>();
        }

        distance = Vector3.Distance(target.position, transform.position);
    }

    void Update()
    {
        if (distance <= attackRadius) AttackPlayer();
    }

    private void AttackPlayer()
    {
        bulletTime -= Time.deltaTime;
        if (bulletTime > 0) return;

        bulletTime = timer;

        bulletObject = Instantiate(projectile, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation) as GameObject;
        bulletBody = bulletObject.GetComponent<Rigidbody>();

        bulletBody.AddForce(bulletBody.transform.forward * shootSpeed);
        Destroy(bulletObject, 2f);

        RaycastHit hit;

        if (Physics.Raycast(bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.forward, out hit, shootRange))
        {
            PlayerHealth target = hit.transform.GetComponent<PlayerHealth>();
            if (target != null) target.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
