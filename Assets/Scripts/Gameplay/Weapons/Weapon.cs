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
        GameObject bulletClone = Instantiate(stats.bulletPrefab);
        bulletClone.transform.rotation = bulletHolder.rotation;
        bulletClone.transform.position = bulletHolder.position;
        bulletClone.GetComponent<Bullet>().Shot(stats);
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
    }

    private void FireGravitationalWeapon()
    {
        
    }

    private void FireSplinterWeapon()
    {

    }
}
