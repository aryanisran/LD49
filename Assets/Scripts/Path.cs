using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public GameObject itself;
    public GameObject pathPrefab;
    public GameObject spawnPoint;

    public GameObject powerUp;

    public GameObject[] powerSpawnPoint;


    private void Start()
    {
        foreach (GameObject point in powerSpawnPoint)
        {
            int chance = Random.Range(1, 10);
            if (chance >= 1 && chance <= 5)
            {
                Instantiate(powerUp, point.transform.position, Quaternion.identity);
            }
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rocket")
        {
            StartCoroutine(NewPath());
        }
    }

    public IEnumerator NewPath()
    {
        Instantiate(pathPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(6f);
        Destroy(itself);
    }

    
}
