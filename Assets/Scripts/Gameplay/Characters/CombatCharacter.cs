using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class CombatCharacter : NetworkBehaviour
{
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector2 rotationLimits;
    private Camera playerCamera;
    private WeaponManager weaponManager;

    public override void OnStartClient()
    {
        base.OnStartClient();
        weaponManager = transform.Find("WeaponManager").GetComponent<WeaponManager>();
        if (IsOwner)
        {
            playerCamera = Camera.main;
            playerCamera.transform.parent = cameraHolder;
            playerCamera.transform.localPosition = Vector3.zero;
            playerCamera.transform.localRotation = Quaternion.identity;
        }
        else
        {
            weaponManager.DisableAllWeapons();
            GetComponent<CombatCharacter>().enabled = false;
        }
    }

    void Update()
    {
        SetCharacterRotation();
    }

    private void SetCharacterRotation()
    {
        float mousePosX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
        mousePosX = Mathf.Clamp(mousePosX, 0, 1);
        float frameRotation = 2 * rotationSpeed * mousePosX - rotationSpeed;
        Vector3 rotationVec = new Vector3(0, frameRotation * Time.deltaTime, 0);
        transform.Rotate(rotationVec, Space.World);
        float rotation = Mathf.Clamp(transform.eulerAngles.y, rotationLimits.x, rotationLimits.y);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotation, transform.eulerAngles.z);
    }
}
