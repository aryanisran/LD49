using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public GameObject itself;
    public GameObject pathPrefab;
    public GameObject spawnPoint;

    public GameObject powerUp, enemy;

    public GameObject[] itemSpawnPoint;


    private void Start()
    {
        foreach (GameObject point in itemSpawnPoint)
        {
            int chance = Random.Range(1, 10);
            GameObject item = null;
            if (chance >= 1 && chance <= 5)
            {
                item = Instantiate(powerUp, point.transform.position, Quaternion.identity);
                
            }
            else if (chance == 6)
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
