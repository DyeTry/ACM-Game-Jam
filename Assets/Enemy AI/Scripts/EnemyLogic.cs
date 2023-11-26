using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    [Header("Enemy Info")]
    public new string name;

    [Header("Controller Info")]
    public float lookRadius;

    [Header("Health Info")]
    public float health;

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

    [Header("Experience Info")]
    public int level;
    public float experience;

    [Header("Currency Info")]
    public float currency;

    private Transform target;
    private NavMeshAgent agent;
    float distance;
    private Wave wave;

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
        distance = Vector3.Distance(target.position, transform.position);
        if (target == null) return;

        if (distance <= lookRadius) WalkToPlayer();
        if (distance <= attackRadius) AttackPlayer();
    }

    private void WalkToPlayer()
    {
        agent.SetDestination(target.position);
        if (distance <= agent.stoppingDistance)
        {
            FaceTarget();
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);
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

    public void SetWaveReference(Wave waveReference) {wave = waveReference; }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
        if (wave != null) wave.enemiesLeft--;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}