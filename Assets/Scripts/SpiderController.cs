using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderController : MonoBehaviour
{
    public Transform playerTransform;
    public bool lookAtPlayer;
    public bool shootAtPlayer;
    public bool moveTowardsPlayer;
    //private bool aaa = true;
    public GameObject Spider;
    public GameObject muzzle;
    public Transform firePoint;
    //public float projectileSpeed = 10f;
    public float JRspeed = 10f;
    public Animator anim;
    public float nextActionTime = 2f;
    public float period = 4f;
    public float awarenessRange = 50f;
    public float attackRange = 30f;
    public float attackpoint = 15f;
    public Health playerHealth;
    private bool okToAttack = false;

    public AudioClip spiderAttack;
    AudioSource audio;
    public Text scoreCounter;
    //public int score;
    public FPSShooter fps;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        audio = GetComponent<AudioSource>();

        fps = GameObject.Find("Player").GetComponent<FPSShooter>();
        scoreCounter = GameObject.FindGameObjectWithTag("ScoreCounter").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < awarenessRange)
        {
            LookAtPlayer();
            if (Vector3.Distance(transform.position, playerTransform.position) < attackRange)
            {
                float dis = (transform.position.y - playerTransform.position.y);
                if (dis * dis < 4) okToAttack = true;
                else okToAttack = false;

                if (okToAttack)
                {
                    MoveTowardsPlayer();
                }
            }
        }
        else anim.SetTrigger("Idle");
    }
    public void LookAtPlayer()
    {
        anim.SetTrigger("Run");
        Vector3 relativePos = playerTransform.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1 * Time.deltaTime);
        Vector3 ShootPos = new Vector3();
        ShootPos = transform.position;
    }
    public void MoveTowardsPlayer()
    {
        if (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z)) > attackpoint)
        {
            anim.SetTrigger("Run");
            // Move our position a step closer to the target.
            float step = JRspeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z), step);
        }

        else
        {
           /*
            shootAtPlayer = true;
            moveTowardsPlayer = false;
            anim.SetTrigger("Idle");
            if (Time.time > nextActionTime)
            {
                nextActionTime += 2*period;
                Shoot();
            }
            */
        }
    }
    public void ShootPrep()
    {
        shootAtPlayer = true;
        moveTowardsPlayer = false;
        InvokeRepeating("Shoot", 0.2f, 3f);
    }
    public void ShootEndPrep()
    {
        shootAtPlayer = false;
        moveTowardsPlayer = true;
        CancelInvoke("Shoot");
    }
    public void Shoot()
    {
        if (shootAtPlayer)
        {
            audio.PlayOneShot(spiderAttack);
            anim.SetTrigger("Attack");
            var muzzleObj = Instantiate(muzzle, firePoint.position, Quaternion.identity) as GameObject;
            Destroy(muzzleObj, 3);
            
            //anim.SetTrigger("Idle");


            //StartCoroutine("Shoottt");
            /*
            var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
            projectileObj.GetComponent<Rigidbody>().velocity = (playerTransform.position - firePoint.position).normalized * projectileSpeed;
            anim.SetTrigger("Idle");
            //iTween.PunchPosition(projectileObj, new Vector3(Random.Range(arcRange, arcRange), Random.Range(arcRange, arcRange), 0), Random.Range(0.5f, 2));

            Destroy(projectileObj, 5f);
            var muzzleObj = Instantiate(muzzle, firePoint.position, Quaternion.identity) as GameObject;
            Destroy(muzzleObj, 2);*/
        }
    }
    private void OnCollisionEnter(Collision co)
    {
        Debug.Log("Tag of obj " + co.collider.tag);

        if (co.gameObject.tag == "PlayerBullet")
        {
            shootAtPlayer = false;
            attackpoint = 0;
            anim.SetTrigger("Death");
            Destroy(Spider, 1.5f);
            fps.incScore(5);
        }
    }
}