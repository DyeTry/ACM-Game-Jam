using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{

    public float attackRadius = 30f;
    public float damage = 15f;
    
    Transform target;
    NavMeshAgent agent;

    [SerializeField] private float timer = 1f;
    private float bulletTime;
    public GameObject projectile;
    public Transform spawnPoint;
    public float shootSpeed;
    public float shootRange;

    GameObject bulletObject;
    Rigidbody bulletBody;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance < attackRadius)
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0)
        {
            return;
        }

        bulletTime = timer;
        
        bulletObject = Instantiate(projectile, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        bulletBody = bulletObject.GetComponent<Rigidbody>();

        bulletBody.AddForce(bulletBody.transform.forward * shootSpeed);

        Destroy(bulletObject, 2f);

        RaycastHit hit;

        if (Physics.Raycast(spawnPoint.transform.position, spawnPoint.transform.forward, out hit, shootRange))
        {
            //Debug.Log("PlayerHealth Tester");
            PlayerHealth target = hit.transform.GetComponent<PlayerHealth>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}