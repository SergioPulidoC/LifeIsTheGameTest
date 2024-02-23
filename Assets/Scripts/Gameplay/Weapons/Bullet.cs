using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletHole;
    private Rigidbody rigid;
    private WeaponStats stats;
    private Dictionary<GameObject, IEnumerator> gravitationalCoroutines = new Dictionary<GameObject, IEnumerator>();
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
            gravitationRadius = GetComponent<SphereCollider>().radius;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal);
        GameObject hole = Instantiate(bulletHole, collision.contacts[0].point, rotation * Quaternion.Euler(90f, 0f, Random.Range(0f, 360f)));
        hole.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
        hole.transform.position -= hole.transform.forward * 0.2f;
        if (stats.weaponType != WeaponStats.Type.Gravitational)
        {
            Destroy(hole, 5);
            Destroy(gameObject);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (stats.weaponType != WeaponStats.Type.Gravitational)
            return;
        if (other.tag != "AffectedByGravitation")
            return;
        print(other.tag);
        print(other.name);
        IEnumerator cor = GravitateTowardsBullet(other.gameObject);
        StartCoroutine(cor);
        gravitationalCoroutines.Add(other.gameObject, cor);

    }

    private void OnTriggerExit(Collider other)
    {
        if (stats.weaponType != WeaponStats.Type.Gravitational)
            return;
        if (other.tag != "AffectedByGravitation")
            return;
        print(other.name);
        StopCoroutine(gravitationalCoroutines[other.gameObject]);
    }

    private IEnumerator GravitateTowardsBullet(GameObject other)
    {
        Rigidbody otherRigid = other.GetComponent<Rigidbody>();
        while (true)
        {
            var dirVector = other.transform.position - transform.position;
            var distance = dirVector.magnitude;
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
