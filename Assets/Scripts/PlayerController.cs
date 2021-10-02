using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameController gm;

    public GameObject forcefieldPrefab;
    public GameObject head, tail;
    Rigidbody headRb, tailRb;
    float headMoveTime;
    public bool canUseSkill;
    int headDirection, tailDirection;
    float headSpeed, tailSpeed;
    public float maxHeadSpeed, baseTailSpeed, moveUpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameController>();
        headRb = head.GetComponent<Rigidbody>();
        tailRb = tail.GetComponent<Rigidbody>();
        tailSpeed = baseTailSpeed;
        canUseSkill = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.started == true)
        {
            if (headMoveTime <= 0)
            {
                headMoveTime = Random.Range(1f, 3f);
                headDirection = Random.Range(-1, 2);
                headSpeed = Random.Range(0.1f, maxHeadSpeed);
            }
            headMoveTime -= Time.deltaTime;

            if (Input.GetKey(KeyCode.A))
            {
                tailDirection = -1;
                tailSpeed += Time.deltaTime * baseTailSpeed;
            }

            else if (Input.GetKey(KeyCode.D))
            {
                tailDirection = 1;
                tailSpeed += Time.deltaTime * baseTailSpeed;
            }

            else
            {
                tailDirection = 0;
                tailSpeed = baseTailSpeed;
            }

            if (Input.GetKey(KeyCode.Space) && canUseSkill == true)
            {
                StartCoroutine(Skill());
            }
        }



    }

    private void FixedUpdate()
    {
        if (gm.started == true)
        {
            headRb.velocity = Vector3.right * headDirection * headSpeed + head.transform.up * moveUpSpeed;
            tailRb.velocity = Vector3.right * tailDirection * tailSpeed;
            Debug.Log(headRb.velocity);
            Debug.Log(tailRb.velocity);
        }
    }

    public IEnumerator Skill()
    {
        canUseSkill = false;
        Instantiate(forcefieldPrefab, head.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(5f);
        canUseSkill = true;
    }
}
