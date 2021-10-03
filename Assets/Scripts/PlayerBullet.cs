using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 4f);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * 10f;
    }
}
