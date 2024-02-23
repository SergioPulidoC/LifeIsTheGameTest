using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletHole;
    Rigidbody rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    public void Shot(WeaponStats weaponStats)
    {
        rigid.AddForce(transform.forward * weaponStats.bulletForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //GameObject bulletHoleClone = Instantiate(bulletHole);
        //bulletHoleClone.transform.position = collision.contacts[0].point;
        //bulletHoleClone.transform.eulerAngles = collision.contacts[0].normal;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal);
        var go = new GameObject();
        go.transform.position = collision.contacts[0].point;
        go.transform.eulerAngles = collision.contacts[0].normal;
        Instantiate(bulletHole, collision.contacts[0].point, rotation * Quaternion.Euler(0f, 0f, 0f));
        Destroy(gameObject);
    }
}
