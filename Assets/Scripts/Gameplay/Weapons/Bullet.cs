using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletHole;
    private Rigidbody rigid;
    private WeaponStats stats;
    //private Dictionary<GameObject, IEnumerator> gravitationalCoroutines = new Dictionary<GameObject, IEnumerator>();
    private float gravitationRadius;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    public void Shot(WeaponStats weaponStats)
    {
        stats = weaponStats;
        rigid.AddForce(transform.forward * stats.bulletForce, ForceMode.Impulse);
        if (stats.weaponType == WeaponStats.Type.Gravitational)
            gravitationRadius = GetComponents<SphereCollider>()[1].radius;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (stats.weaponType == WeaponStats.Type.Gravitational)
            return;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal);
        GameObject hole = Instantiate(bulletHole, collision.contacts[0].point, rotation * Quaternion.Euler(90f, 0f, Random.Range(0f, 360f)));
        hole.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
        hole.transform.position -= hole.transform.forward * 0.2f;
        Destroy(hole, 5);
        Destroy(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (stats.weaponType != WeaponStats.Type.Gravitational)
            return;
        if (other.tag != "AffectedByGravitation")
            return;
        StartCoroutine(GravitateTowardsBullet(other.gameObject));

    }
    private IEnumerator GravitateTowardsBullet(GameObject other)
    {
        Rigidbody otherRigid = other.GetComponent<Rigidbody>();
        Vector3 dirVector = transform.position - other.transform.position;
        float distance = dirVector.magnitude;
        Debug.Log(gravitationRadius);
        while (distance <= gravitationRadius)
        {
            dirVector = transform.position - other.transform.position;
            distance = dirVector.magnitude;
            if (distance > 0)
            {
                float amplitude = -distance / gravitationRadius + 1;
                Debug.Log(amplitude);
                otherRigid.velocity += dirVector.normalized * amplitude * Time.deltaTime * stats.gravitationalForce;
            }
            yield return null;
        }
    }
}
