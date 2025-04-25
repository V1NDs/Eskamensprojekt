using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform cam;
    [SerializeField] private AudioSource audio;
    public GameObject text; // Hjalte er en brilleabbet amfetamin bruger.

    float timeSinceLastShot;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload()
    {
        if (!gunData.reloading && this.gameObject.activeSelf)
            StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        // Her sender vi datoen fra reload til teksten
        text.GetComponent<TMP_Text>().text = "" + gunData.currentAmmo;

        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    private void Shoot()
    {
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    EnemyAI damageable = hitInfo.transform.GetComponent<EnemyAI>();
                    damageable?.TakeDamage(gunData.damage);
                }

                gunData.currentAmmo--;

                // Her sender vi datoen fra SKudt skud til teksten
                text.GetComponent<TMP_Text>().text = "" + gunData.currentAmmo;

                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(cam.position, cam.forward * gunData.maxDistance);
    }

    private void OnGunShot() {
        audio.Play();
    }
}