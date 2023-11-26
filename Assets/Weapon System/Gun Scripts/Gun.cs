using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    [Header("Info")]
    public new string name;

    [Header("Shooting")]
    public float damage;
    public float maxDistance;

    [Header("Reloading")]
    public int currentAmmo;
    public int magSize;
    public int maxAmmo;
    public float fireRate;
    public float reloadTime;

    [Header("Currency")]
    public int currencyPerHit;

    [Header("Store Price")]
    public int cost;

    [HideInInspector]
    public bool isReloading;
    private float timeToFire = 0f;
    private bool canShoot;
    private bool canReload;

    [Header("Muzzle Options")]
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impactEffect;

    private Camera fpsCam;
    

    //[Header("Ammo Display")]
    private TextMeshProUGUI ammoText;

    //[Header("Animator")]
    private Animator animator;

    [Header("Style")]
    public GunStyle style;

    public GameObject prefab;

    void Start()
    {        
        GetReferences();

        currentAmmo = magSize;
        ammoText.text = currentAmmo + "/" + maxAmmo;
    }
    
    private void GetReferences()
    {
        fpsCam = Camera.main;

        GameObject ammoTextObject = GameObject.FindGameObjectWithTag("AmmoText");
        if (ammoTextObject != null) ammoText = ammoTextObject.GetComponent<TextMeshProUGUI>();
        
        GameObject animatorObject = GameObject.FindGameObjectWithTag("Animator");
        if (animatorObject != null) animator = animatorObject.GetComponent<Animator>();

        prefab = gameObject;
    }

    void OnEnable()
    {
        isReloading = false;
        //NullReferenceException: Object reference not set to an instance of an object
        //Getting error but it still works huh
        //Fucking duct tape and hope code lmao
        animator.SetBool("Reloading", false);
    }
    
    void Update()
    {
        ammoText.text = currentAmmo + "/" + maxAmmo;

        if (isReloading) {return;}

        if (currentAmmo == 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (canReload)
        {
            if (currentAmmo <= magSize)
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
                timeToFire = Time.time + 1f/fireRate;

                if (currentAmmo > 0)
                {
                    Shoot();
                }
            }
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;

        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - 0.25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        maxAmmo -= magSize - currentAmmo;
        currentAmmo = magSize;
        isReloading = false;
    }

    public void Shoot()
    {
        muzzleFlash.Play();
        currentAmmo--;
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, maxDistance))
        {
        EnemyScalable target = hit.transform.GetComponent<EnemyScalable>();
         
        if (target != null)
        {
            target.TakeDamage(damage);
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

public enum GunStyle { Primary, Secondary }