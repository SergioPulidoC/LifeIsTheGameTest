using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCanvas : MonoBehaviour
{
    [SerializeField] private Transform weaponSelector;
    public void HighlightSelectedWeapon(int weaponIndex)
    {
        foreach (Transform weapon in weaponSelector)
            weapon.GetChild(0).gameObject.SetActive(false);
        Transform selectedWeapon = weaponSelector.GetChild(weaponIndex);
        selectedWeapon.GetChild(0).gameObject.SetActive(true);

    }
}
