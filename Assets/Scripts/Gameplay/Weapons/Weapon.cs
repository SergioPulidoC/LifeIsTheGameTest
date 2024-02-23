using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform bulletHolder;
    public WeaponStats stats;

    private void Awake()
    {
        bulletHolder = transform.Find("BulletHolder");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireWeapon();
        }
    }

    private void FireWeapon()
    {

        switch (stats.weaponType)
        {
            case WeaponStats.Type.Parabolic:
                FireParabolicWeapon();
                break;
            case WeaponStats.Type.Gravitational:
                FireGravitationalWeapon();
                break;
            case WeaponStats.Type.Splinter:
                FireSplinterWeapon();
                break;
        }
    }

    private GameObject InstantiateBullet()
    {
        GameObject bulletClone = Instantiate(stats.bulletPrefab);
        bulletClone.transform.rotation = bulletHolder.rotation;
        bulletClone.transform.position = bulletHolder.position;
        return bulletClone;
    }

    private void FireParabolicWeapon()
    {
        GameObject bullet = InstantiateBullet();
        bullet.GetComponent<Bullet>().Shot(stats);
    }

    private void FireGravitationalWeapon()
    {
        GameObject bullet = InstantiateBullet();
        bullet.GetComponent<Bullet>().Shot(stats);
    }

    private void FireSplinterWeapon()
    {
        for (int i = 0; i < stats.pelletCount; i++)
        {
            GameObject bullet = InstantiateBullet();
            float coneAngle = stats.coneAngle;
            float xRotation = Random.Range(-coneAngle, coneAngle);
            float yRotation = Random.Range(-coneAngle, coneAngle);
            bullet.transform.Rotate(xRotation, yRotation, 0f);
            bullet.GetComponent<Bullet>().Shot(stats);
        }
    }
}
