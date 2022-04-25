using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JRController : MonoBehaviour
{
    public Transform playerTransform;
    public bool lookAtPlayer;
    public bool shootAtPlayer;
    public bool moveTowardsPlayer;

    public GameObject projectile;
    public GameObject muzzle;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float JRspeed = 10f;
    public Animator anim;
    private float nextActionTime = 2f;
    public float period = 4f;
    public float awarenessRange = 50f;
    public float attackRange = 30f;
    public float attackRangeyplus = 2f;
    public float attackRangeyminus = -2f;
    public float attackpoint = 15f;
    bool isShoottcouritine = false;

    public AudioClip JRMove;
    public AudioClip JRShoot;
    AudioSource audio;
    public bool statJR = false;
    public Vector3 halfDimensions;
    private Vector3 boxCenter;
    public Transform boxcenter;
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        audio = GetComponent<AudioSource>();
        audio.clip = JRMove;
        boxCenter = boxcenter.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < awarenessRange)
        {
            LookAtPlayer();
            bool okToAttack ;
            
            if (Vector3.Distance(transform.position, playerTransform.position) < attackRange )
            {
                float dis = (transform.position.y - playerTransform.position.y);
                if (dis*dis < 4) okToAttack = true;
                else okToAttack = false;

                if (okToAttack)
                {
                    MoveTowardsPlayer();
                }
                
            }
        }
        else { anim.SetTrigger("Idle"); audio.Stop(); }

        if (statJR)
        {
            //OnDrawGizmos();
            Collider[] playerIn = Physics.OverlapBox(boxCenter, halfDimensions, Quaternion.identity, layerMask);
            //Debug.Log("player in :" + playerIn.Length);
            if (playerIn.Length > 0)
            {
                LookAtPlayer();
                //Debug.Log("Shoot trigerred" );
                shootAtPlayer = true;
                moveTowardsPlayer = false;
                anim.SetTrigger("Attack");
                if (Time.time > nextActionTime)
                {
                    nextActionTime += period;
                    Shoot();
                }
            }
            else
            {
                anim.SetTrigger("Idle");
            }
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(boxCenter, halfDimensions);
    }
    public void MoveTowardsPlayer()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) > attackpoint)
        {
            if (audio.isPlaying != true) audio.Play();

            anim.SetBool("Run", true);
            // Move our position a step closer to the target.
            float step = JRspeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, step);
        }

        else
        {
            shootAtPlayer = true;
            moveTowardsPlayer = false;
            anim.SetBool("Run", false);
            anim.SetTrigger("Attack");
            if (Time.time > nextActionTime)
            {
                nextActionTime += period;
                Shoot();
            } 
        }
    }

    public void Shoot()
    {
        if (shootAtPlayer)
        {
            audio.Stop();
            shootAtPlayer = false;
            anim.SetTrigger("Idle");
            StartCoroutine("Shoottt");
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
    IEnumerator  Shoottt()
    {
        if (!isShoottcouritine)
        {
            isShoottcouritine = true;
            yield return new WaitForSeconds(period);
            audio.PlayOneShot(JRShoot);
            var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
            projectileObj.GetComponent<Rigidbody>().velocity = (playerTransform.position - firePoint.position).normalized * projectileSpeed;
            anim.SetTrigger("Idle");
            //iTween.PunchPosition(projectileObj, new Vector3(Random.Range(arcRange, arcRange), Random.Range(arcRange, arcRange), 0), Random.Range(0.5f, 2));

            Destroy(projectileObj, 5f);
            var muzzleObj = Instantiate(muzzle, firePoint.position, Quaternion.identity) as GameObject;
            Destroy(muzzleObj, 2);
            StartCoroutine("Any");
        }
    }
    IEnumerator Any()
    {
        yield return new WaitForSeconds(2f);
        isShoottcouritine = false;
    }
}
