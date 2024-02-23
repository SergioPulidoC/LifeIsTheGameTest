using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class PlayerSpawnBullet : NetworkBehaviour
{

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (IsOwner)
        {
        }
        else
        {
            GetComponent<PlayerSpawnBullet>().enabled = false;
        }
    }
    [ServerRpc]
    public void SpawnBullet(GameObject spawnedBullet, WeaponStats stats, Vector3 position, Quaternion rotation, Transform character)
    {
        GameObject clone = Instantiate(spawnedBullet);
        clone.transform.position = position;
        clone.transform.rotation = rotation;
        clone.GetComponent<Bullet>().character = character;
        clone.GetComponent<Bullet>().spawnedBullet = clone;
        ServerManager.Spawn(clone);
        SetSpawnedBullet(spawnedBullet, clone, stats);
    }

    [ObserversRpc]
    public void SetSpawnedBullet(GameObject localBullet, GameObject spawned, WeaponStats stats)
    {
        spawned.GetComponent<Bullet>().Shot(stats);
        localBullet.GetComponent<Bullet>().spawnedBullet = spawned;
    }
}
