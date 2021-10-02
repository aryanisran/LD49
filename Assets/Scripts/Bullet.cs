using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    bool bounced;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "LeftWall":
                if (bounced) Destroy(gameObject);
                rb.velocity = Vector3.Reflect(rb.velocity, Vector3.left);
                bounced = true;
                break;
            case "RightWall":
                if (bounced) Destroy(gameObject);
                rb.velocity = Vector3.Reflect(rb.velocity, Vector3.right);
                bounced = true;
                break;
            case "Player":
                break;
            default:
                break;
        }
    }
}