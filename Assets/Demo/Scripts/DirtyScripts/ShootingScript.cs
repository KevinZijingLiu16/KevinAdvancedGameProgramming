using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float bulletSpeed = 20f;
    public int ammo = 10;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ammo > 0)
            {
                GameObject b = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
                b.GetComponent<Rigidbody>().linearVelocity = transform.forward * bulletSpeed;
                ammo = ammo - 1;
            }
            else
            {
                Debug.Log("No Ammo!");
            }
        }
    }
}
