using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // Reference to the bullet prefab
    [SerializeField] private Transform firePoint; // The spawn position for bullets
    [SerializeField] private float bulletSpeed = 20f; // Speed of the bullet
    [SerializeField] private int maxAmmo = 10; // Maximum ammunition capacity
    private int currentAmmo; // The current available ammo

    private void Start()
    {
        currentAmmo = maxAmmo; // Initialize ammo when the game starts
    }

    private void Update()
    {
        HandleShooting(); // Check for shooting input in each frame
    }

    /// <summary>
    /// Checks if the player presses the shoot button and triggers shooting if ammo is available.
    /// </summary>
    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Fire when spacebar is pressed
        {
            if (currentAmmo > 0)
            {
                Shoot(); // Fire a bullet
            }
            else
            {
                Debug.Log("No Ammo!"); // Notify the player that they are out of ammo
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) // Reload when 'R' key is pressed
        {
            Reload(); // Reload the ammo
        }
    }

    private void Shoot()
    {
        // Instantiate the bullet at the firePoint position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed; // Apply velocity to move the bullet
        }

        currentAmmo--; // Reduce the ammo count
        Debug.Log($"Remaining Ammo: {currentAmmo}"); // Log remaining ammo count
    }

    /// <summary>
    /// Reloads the ammo to its maximum capacity.
    /// </summary>
    public void Reload()
    {
        currentAmmo = maxAmmo; // Reset the ammo count
        Debug.Log("Ammo Reloaded!"); // Log the reload action
    }
}
