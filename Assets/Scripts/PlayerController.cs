using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject head, tail;
    Rigidbody headRb, tailRb;
    float headMoveTime;
    int headDirection, tailDirection;
    float headSpeed, tailSpeed;
    public float maxHeadSpeed, baseTailSpeed;
    // Start is called before the first frame update
    void Start()
    {
        headRb = head.GetComponent<Rigidbody>();
        tailRb = tail.GetComponent<Rigidbody>();
        tailSpeed = baseTailSpeed;
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
            tailDirection = -1;
            tailSpeed += Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            tailDirection = 1;
            tailSpeed += Time.deltaTime;
        }
        
        else
        {
            tailDirection = 0;
            tailSpeed = baseTailSpeed;
        }
    }

    private void FixedUpdate()
    {
        headRb.velocity = Vector3.right * headDirection * headSpeed + Vector3.up * 3;
        tailRb.velocity = Vector3.right * tailDirection * tailSpeed;
    }
}
