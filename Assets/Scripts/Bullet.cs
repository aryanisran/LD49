using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    bool bounced = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 2.5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            Destroy(this.gameObject);
        }

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
            case "Rocket":
                other.GetComponent<PlayerController>().LoseHealth();
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
}
