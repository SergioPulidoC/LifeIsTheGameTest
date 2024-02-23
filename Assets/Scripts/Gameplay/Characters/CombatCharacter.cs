using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCharacter : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector2 rotationLimits;
    void Update()
    {
        SetCharacterRotation();
    }

    private void SetCharacterRotation()
    {
        //Vector3 eulerAngles = transform.eulerAngles;
        //if (eulerAngles.y < rotationLimits.x)
        //{
        //    transform.eulerAngles = new Vector3(eulerAngles.x, rotationLimits.x, eulerAngles.z);
        //    return;
        //}
        //else if (eulerAngles.y > rotationLimits.y)
        //{
        //    transform.eulerAngles = new Vector3(eulerAngles.x, rotationLimits.y, eulerAngles.z);
        //    return;
        //}
        float mousePosX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
        mousePosX = Mathf.Clamp(mousePosX, 0, 1);
        float frameRotation = 2 * rotationSpeed * mousePosX - rotationSpeed;
        Vector3 rotationVec = new Vector3(0, frameRotation * Time.deltaTime, 0);
        transform.Rotate(rotationVec, Space.World);
        float rotation = Mathf.Clamp(transform.eulerAngles.y, rotationLimits.x, rotationLimits.y);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotation, transform.eulerAngles.z);
    }
}
