using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<WeaponStats> weapons;
    [SerializeField] private float distanceBetweenWeapons;
    [SerializeField] private Transform characterWeaponHolder;
    [HideInInspector] public Transform character;
    private CombatCanvas combatCanvas;

    private void Awake()
    {
        combatCanvas = FindObjectOfType<CombatCanvas>();
        character = transform.parent;
        InitializeWeapons();
        SwapWeapon(0);
    }

    private void InitializeWeapons()
    {
        float weaponDistance = 0;
        foreach (WeaponStats weapon in weapons)
        {
            InitializeGroundWeapon(weapon, weaponDistance);
            weaponDistance += distanceBetweenWeapons;
            InitializeCharacterWeapon(weapon);
        }
    }

    private void InitializeGroundWeapon(WeaponStats weapon, float weaponDistance)
    {
        GameObject weaponHolder = new GameObject();
        weaponHolder.transform.parent = transform;
        weaponHolder.transform.localPosition = new Vector3(0, weaponDistance, 0); ;
        weaponHolder.transform.localRotation = Quaternion.identity;
        Instantiate(weapon.weaponPrefab, weaponHolder.transform);
    }

    private void InitializeCharacterWeapon(WeaponStats stats)
    {
        GameObject weaponClone = Instantiate(stats.weaponPrefab, characterWeaponHolder);
        Weapon weapon = weaponClone.AddComponent<Weapon>();
        weapon.stats = stats;
        weapon.character = character;
    }

    private void Update()
    {
        CheckForInput();
    }

    private void CheckForInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwapWeapon(2);
        }
    }

    private void SwapWeapon(int newWeaponIdx)
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
        transform.GetChild(newWeaponIdx).gameObject.SetActive(false);
        foreach (Transform child in characterWeaponHolder)
            child.gameObject.SetActive(false);
        characterWeaponHolder.GetChild(newWeaponIdx).gameObject.SetActive(true);
        combatCanvas.HighlightSelectedWeapon(newWeaponIdx);
    }

    public void DisableAllWeapons()
    {
        foreach (Transform weapon in characterWeaponHolder)
        {
            print("Disable!!!");
            weapon.GetComponent<Weapon>().enabled = false;
        }
    }
}
