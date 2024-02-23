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
            case WeaponStats.Type.Parabollic:
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

    private void FireParabolicWeapon()
    {
        GameObject bulletClone = Instantiate(stats.bulletPrefab, bulletHolder);
        bulletClone.GetComponent<Bullet>().Shot(stats);
    }

    private void FireGravitationalWeapon()
    {

    }

    private void FireSplinterWeapon()
    {

    }
}
