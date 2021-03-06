using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public GameObject itself;
    public GameObject pathPrefab;
    public GameObject spawnPoint;

    GameObject powerUpToSpawn;

    public GameObject  enemy;
    public GameObject[] powerUp;
    public GameObject[] itemSpawnPoint;

    private void Start()
    {
        GameController.instance.background.sprite = GameController.instance.bgs[Random.Range(0, GameController.instance.bgs.Length)];
        foreach (GameObject point in itemSpawnPoint)
        {
            int chance = Random.Range(1, 10);
            GameObject item = null;
            if (chance >= 1 && chance <= 4)
            {
                foreach (GameObject powerup in powerUp)
                {
                    int powerChance = Random.Range(0, powerUp.Length);
                    powerUpToSpawn = powerUp[powerChance];
                }
                item = Instantiate(powerUpToSpawn, point.transform.position, Quaternion.identity);

            }
            else if (chance >= 5 && chance <= 9)
            {
                item = Instantiate(enemy, point.transform.position, Quaternion.identity);
            }
            if(item != null)
            {
                item.transform.SetParent(transform, true);
            }
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rocket")
        {
            Instantiate(pathPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject, 6f);
        }
    }
}
