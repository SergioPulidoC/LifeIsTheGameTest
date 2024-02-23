using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    public Transform bulletHolder;
    public WeaponStats stats;
    [HideInInspector] public Transform character;

    private void Awake()
    {
        bulletHolder = transform.Find("BulletHolder");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            if (!IsMouseOverGameWindow)
                return;
            FireWeapon();
        }
    }

    bool IsMouseOverGameWindow
    {
        get 
        { 
            return !(0 > Input.mousePosition.x || 0 > Input.mousePosition.y || Screen.width < Input.mousePosition.x || Screen.height < Input.mousePosition.y); 
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
        bulletClone.GetComponent<Bullet>().character = character;
        bulletClone.GetComponent<Bullet>().Shot(stats);
        return bulletClone;
    }

    private void FireParabolicWeapon()
    {
        GameObject bullet = InstantiateBullet();
        bullet.GetComponent<Bullet>().Shot(stats);
        character.GetComponent<PlayerSpawnBullet>().SpawnBullet(bullet, stats, bulletHolder.position, bulletHolder.rotation, character);
    }

    private void FireGravitationalWeapon()
    {
        GameObject bullet = InstantiateBullet();
        bullet.GetComponent<Bullet>().Shot(stats);
        character.GetComponent<PlayerSpawnBullet>().SpawnBullet(bullet, stats, bulletHolder.position, bulletHolder.rotation, character);
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
            character.GetComponent<PlayerSpawnBullet>().SpawnBullet(bullet, stats, bulletHolder.position, bullet.transform.rotation, character);
        }
    }
}
