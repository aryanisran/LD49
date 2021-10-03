using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameController gm;
    
    public float fireRate;
    bool firecooldown = true;
    //public bool shooting;
    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject bullet;
    [SerializeField] Slider cooldownIndi;
    float cd = 5f;

    public GameObject forcefieldPrefab;
    public GameObject head, tail;
    Rigidbody headRb, tailRb;
    float headMoveTime;
    public bool canUseSkill, canBeDamaged;
    int headDirection;
    float tailDirection;
    float headSpeed, tailSpeed;
    public float maxHeadSpeed, baseTailSpeed, baseMoveUpSpeed, invulnTime, boostTime;
    float moveUpSpeed;
    public int health;
    public bool boosting;
    public ParticleSystem normalParticle, boostParticle;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameController>();
        headRb = head.GetComponent<Rigidbody>();
        tailRb = tail.GetComponent<Rigidbody>();
        tailSpeed = baseTailSpeed;
        moveUpSpeed = baseMoveUpSpeed;
        canUseSkill = true;
        canBeDamaged = true;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownIndi.value = cd;

        if (gm.started == true)
        {
            if (headMoveTime <= 0 && !boosting)
            {
                headMoveTime = Random.Range(1f, 3f);
                headDirection = Random.Range(-1, 2);
                headSpeed = Random.Range(0.1f, maxHeadSpeed);
            }
            headMoveTime -= Time.deltaTime;
            tailDirection = Input.GetAxis("Horizontal");

            if (Input.GetKey(KeyCode.Space) && canUseSkill == true)
            {
                StartCoroutine(Skill());
            }

            if (firecooldown == true)
            {
                StartCoroutine(Shoot());
            }

            if (canUseSkill == false)
            {
                cd -= Time.deltaTime;
            }
            else
            {
                cd = 5f;
            }

        }
    }

    private void FixedUpdate()
    {
        if (gm.started == true)
        {
            headRb.velocity = Vector3.right * headDirection * headSpeed + head.transform.up * moveUpSpeed;
            tailRb.velocity = Vector3.right * tailDirection * tailSpeed;
        }
    }

    public IEnumerator Skill()
    {
        //cd -= Time.deltaTime;
        canUseSkill = false;
        Instantiate(forcefieldPrefab, head.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(5f);
        canUseSkill = true;
        //cd = 5f;
    }

    public void LoseHealth()
    {
        if (canBeDamaged)
        {
            GetComponent<Animator>().SetTrigger("damaged");
            health--;
            StartCoroutine(Invuln());
            GameController.instance.UpdateHealth(health);
            if(health <= 0)
            {
                GameController.instance.GameOver();
            }
        }
    }

    IEnumerator Invuln()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(invulnTime);
        canBeDamaged = true;
    }

    public IEnumerator Shoot()
    {
        firecooldown = false;
        Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        yield return new WaitForSeconds(fireRate);
        firecooldown = true;
    }

    public void SetBoosting()
    {
        boosting = true;
        moveUpSpeed = baseMoveUpSpeed * 2;
        headDirection = 0;
        normalParticle.Pause();
        boostParticle.Play();
        CancelInvoke();
        Invoke("UnsetBoosting", boostTime);
    }

    void UnsetBoosting()
    {
        moveUpSpeed = baseMoveUpSpeed;
        normalParticle.Play();
        boostParticle.Stop();
        boosting = false;
    }
}
