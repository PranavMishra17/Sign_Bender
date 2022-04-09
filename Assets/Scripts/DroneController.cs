using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public Transform playerTransform;
    public bool lookAtPlayer;
    public bool shootAtPlayer;
    public bool moveTowardsPlayer;
    //private bool aaa = true;
    public GameObject projectile;
    public GameObject muzzle;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float JRspeed = 10f;
    public float movDistance = 10f;
    //public Animator anim;
    private float nextActionTime = 2f;
    public float period = 4f;
    Vector3 initialPosDrone, movPosDrone;
    public Animator anim;

    private Quaternion targetRotation;

    public float awarenessRange = 50f;
    public float attackRange = 30f;
    public float attackpoint = 15f;

    public float height = 6.75f;

    public AudioClip droneMove;
    public AudioClip droneAttack;
   //public AudioClip droneDeath;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        
        playerTransform = GameObject.Find("Player").transform;
        audio = GetComponent<AudioSource>();
        audio.clip = droneMove;
        audio.Play();
        Vector3 initialPosDrone = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 movPosDrone = new Vector3(transform.position.x, transform.position.y, transform.position.z + movDistance);
        targetRotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < awarenessRange)
        {
            
            LookAtPlayer();
            if (Vector3.Distance(transform.position, playerTransform.position) < attackRange)
            {
                StartCoroutine("MoveTowardsPlayer1");
                // MoveTowardsPlayer();
            }
        }
        else
        {
            audio.Stop();
        }
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.useGravity == true) CancelInvoke("Shoot");
        if(transform.position.y != height && rb.useGravity != true)
        {
            MoveToHeight();
        }
    }
    public void LookAtPlayer()
    {
        Vector3 relativePos = playerTransform.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1 * Time.deltaTime);
        Vector3 ShootPos = new Vector3();
        ShootPos = transform.position;
    }
    public void MoveToHeight()
    {
        float step = JRspeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x, height, transform.position.z), step);

    }
    public void MoveTowardsPlayer()
    {
        if (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z)) > attackpoint)
        {
            
            //FindObjectOfType<AudioManager>().Play("Dronemove");
            //anim.SetBool("Run", true);
            // Move our position a step closer to the target.
            float step = JRspeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z), step);
        }

        else
        {
            /*//audio.Pause();
            //FindObjectOfType<AudioManager>().Pause("Dronemove");
            shootAtPlayer = true;
            moveTowardsPlayer = false;
            //anim.SetBool("Run", false);
           // anim.SetTrigger("Attack");
            if (Time.time > nextActionTime)
            {
                nextActionTime += period;
                Shoot();
            }
            */
        }
    }
    IEnumerator MoveTowardsPlayer1()
    {
        yield return new WaitForSeconds(1f);
        MoveTowardsPlayer();
    }
    public void ShootPrep()
    {
        shootAtPlayer = true;
        moveTowardsPlayer = false;
        InvokeRepeating("Shoot" , 0.2f, 3f);
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
            //StartCoroutine("Shoottt");
            var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
            projectileObj.GetComponent<Rigidbody>().velocity = (playerTransform.position - firePoint.position).normalized * projectileSpeed;
            //anim.SetTrigger("Idle");
            //iTween.PunchPosition(projectileObj, new Vector3(Random.Range(arcRange, arcRange), Random.Range(arcRange, arcRange), 0), Random.Range(0.5f, 2));
            
            audio.PlayOneShot(droneAttack);

            Destroy(projectileObj, 5f);
            var muzzleObj = Instantiate(muzzle, firePoint.position, Quaternion.identity) as GameObject;
            Destroy(muzzleObj, 2);
        }
    }
    IEnumerator Shoottt()
    {
        yield return new WaitForSeconds(1.5f);
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (playerTransform.position - firePoint.position).normalized * projectileSpeed;
        //anim.SetTrigger("Idle");
        //iTween.PunchPosition(projectileObj, new Vector3(Random.Range(arcRange, arcRange), Random.Range(arcRange, arcRange), 0), Random.Range(0.5f, 2));

        Destroy(projectileObj, 5f);
        var muzzleObj = Instantiate(muzzle, firePoint.position, Quaternion.identity) as GameObject;
        Destroy(muzzleObj, 2);
    }
}