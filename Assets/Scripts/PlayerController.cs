using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject head, tail;
    Rigidbody headRb, tailRb;
    float headMoveTime;
    int headDirection;
    float headSpeed;
    public float maxHeadSpeed, tailSpeed;
    // Start is called before the first frame update
    void Start()
    {
        headRb = head.GetComponent<Rigidbody>();
        tailRb = tail.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(headMoveTime <= 0)
        {
            headMoveTime = Random.Range(1f, 3f);
            headDirection = Random.Range(-1, 2);
            headSpeed = Random.Range(0.1f, maxHeadSpeed);
        }
        headMoveTime -= Time.deltaTime;

        if(Input.GetKey(KeyCode.A))
        {
            tailRb.velocity = Vector3.left * tailSpeed;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            tailRb.velocity = Vector3.right * tailSpeed;
        }
        
        else
        {
            tailRb.velocity = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        headRb.velocity = Vector3.right * headDirection * headSpeed + Vector3.up * 3;
    }
}
