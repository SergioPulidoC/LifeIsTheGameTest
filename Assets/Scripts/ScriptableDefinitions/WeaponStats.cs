using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New weapon", menuName = "Tools/Weapon Stats")]
public class WeaponStats : ScriptableObject
{
    public enum Type
    {
        Parabollic,
        Gravitational,
        Splinter
    }

    [Tooltip("Prefab to be shown on the gameplay.")]
    public GameObject weaponPrefab;
    [Tooltip("Prefab of bullets to be instantiated on fire.")]
    public GameObject bulletPrefab;
    [Tooltip("The weapon type defines its bullet behaviour.")]
    public Type weaponType;
    [Tooltip("Force of the bullets in a parabollic-type weapon.")]
    public float bulletForce;
    [Tooltip("Gravitational force in a gravitational-type weapon.")]
    public float gravitationalForce;
    [Tooltip("Cone angle in a splinter-type weapon.")]
    public float coneAngle;
    [Tooltip("Pellet count in a splinter-type weapon.")]
    public float pelletCount;
}
