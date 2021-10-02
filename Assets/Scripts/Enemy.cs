using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Projectile Settings")]
    public int numberOfProjectiles;             // Number of projectiles to shoot.
    public float projectileSpeed;               // Speed of the projectile.
    public GameObject ProjectilePrefab;         // Prefab to spawn.
    public float fireRate;                      // Rate that bullet waves will be spawned

    [Header("Private Variables")]
    private Vector3 startPoint;                 // Starting position of the bullet.
    private const float radius = 1F;            // Help us find the move direction.
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        InvokeRepeating("SpawnProjectile", 0f, fireRate);
    }

    // Spawns x number of projectiles.
    private void SpawnProjectile()
    {
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i <= numberOfProjectiles - 1; i++)
        {
            // Direction calculations.
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            // Create vectors.
            Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
            Vector3 projectileMoveDirection = (projectileVector - startPoint).normalized * projectileSpeed;

            // Create game objects.
            GameObject tmpObj = Instantiate(ProjectilePrefab, startPoint + projectileMoveDirection.normalized, Quaternion.identity);
            tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(projectileMoveDirection.x, projectileMoveDirection.y, 0);

            // Destory the gameobject after 10 seconds.
            Destroy(tmpObj, 10F);

            angle += angleStep;
        }
    }
}
