using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class GunLogic : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunInfo gunInfo;

    [Header("Muzzle Options")]
    [SerializeField] private Camera fpsCam;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impactEffect;

    [Header("Ammo Display")]
    [SerializeField] private TextMeshProUGUI ammoText;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    private float timeToFire = 0f;
    private bool canShoot;
    private bool canReload;

    void Start()
    {
        gunInfo.currentAmmo = gunInfo.magSize;
        ammoText.text = gunInfo.currentAmmo + "/" + gunInfo.maxAmmo;
    }

    void OnEnable()
    {
        gunInfo.isReloading = false;
        animator.SetBool("Reloading", false);
    }
    
    void Update()
    {
        ammoText.text = gunInfo.currentAmmo + "/" + gunInfo.maxAmmo;

        if (gunInfo.isReloading) {return;}

        if (gunInfo.currentAmmo == 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (canReload)
        {
            if (gunInfo.currentAmmo <= gunInfo.magSize)
            {
                StartCoroutine(Reload());
                canReload = false;
                return;
            }
        }

        if (canShoot)
        {
            if (Time.time >= timeToFire)
            {
                timeToFire = Time.time + 1f/gunInfo.fireRate;

                if (gunInfo.currentAmmo > 0)
                {
                    Shoot();
                }
            }
        }
    }

    private IEnumerator Reload()
    {
        gunInfo.isReloading = true;

        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(gunInfo.reloadTime - 0.25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);
        
        gunInfo.maxAmmo -= gunInfo.magSize - gunInfo.currentAmmo;
        gunInfo.currentAmmo = gunInfo.magSize;
        gunInfo.isReloading = false;
    }

    public void Shoot()
    {
        muzzleFlash.Play();
        gunInfo.currentAmmo--;
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, gunInfo.maxDistance))
        {
        EnemyScalable target = hit.transform.GetComponent<EnemyScalable>();
         
        if (target != null)
        {
            target.TakeDamage(gunInfo.damage);
        }

        GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 2f);
        }
    }

    public void StartShoot() { canShoot = true; }

    public void EndShoot() { canShoot = false; }

    public void StartReload() { canReload = true; }

    public void EndReload() { canReload = false; }
}
