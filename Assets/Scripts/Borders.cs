using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    public GameController gm;
    public Material normalMat, bouncyMat;
    public GameObject bounceParticle;
    MeshRenderer mr;

    public void Start()
    {
        gm = GameController.instance;
        mr = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (gm.bouncyWalls)
        {
            mr.material = bouncyMat;
        }
        else
        {
            mr.material = normalMat;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rocket"))
        {
            if (gm.bouncyWalls)
            {
                PlayerController pc = other.GetComponent<PlayerController>();
                Vector3 normal = Vector3.zero;
                if (transform.CompareTag("LeftWall"))
                {
                    normal = transform.right * -1;
                }
                else if(transform.CompareTag("RightWall"))
                {
                    normal = transform.right;
                }

                other.transform.up = Vector3.Reflect(other.transform.up, normal);
            }
            else
            {
                gm.GameOver();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Rocket"))
        {
            if (gm.bouncyWalls)
            {
                Vector3 normal = Vector3.zero;
                if (transform.CompareTag("LeftWall"))
                {
                    normal = transform.right * -1;
                }
                else if (transform.CompareTag("RightWall"))
                {
                    normal = transform.right;
                }

                Instantiate(bounceParticle, collision.contacts[0].point, Quaternion.identity);
                collision.transform.transform.up = Vector3.Reflect(collision.transform.transform.up, normal);
            }
            else
            {
                gm.GameOver();
            }
        }
    }
}
