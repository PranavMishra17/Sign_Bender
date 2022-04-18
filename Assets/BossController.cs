using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    Vector3 rot = Vector3.zero;
    float rotSpeed = 40f;
    Animator anim;
    public float SphereAttackSpeed = 100f;
    public float SphereWalkSpeed = 20f;

    public float awarenessRange = 50f;
    public float attackRange = 30f;
    public float attackpoint = 15f;

    public Transform playerTransform;

    public bool attackingplayer = true;
    public bool setplayerloc = true;
    public bool canbeshot = false;
    public bool activate = false;
    bool returnpos;
    public GameObject oposGO;
    public Health playerhealth;
    Vector3 ppos;

    public AudioClip RSRoll;
    public AudioClip RSOpen;
    public AudioClip RSWalk;
    public AudioClip RSDestroy;
    AudioSource audio;

    // Use this for initialization
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        gameObject.transform.eulerAngles = rot;
    }
    void Start()
    {
        setplayerloc = true;
        audio = GetComponent<AudioSource>();

        playerTransform = GameObject.Find("Player").transform;
        audio = GetComponent<AudioSource>();
        audio.clip = RSWalk;
        // audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            if (anim.GetBool("Open_Anim")  == false)
            {
                anim.SetBool("Open_Anim", true);
            }
           
            if (Vector3.Distance(transform.position, playerTransform.position) < awarenessRange && attackingplayer)
            {
                LookAtPlayer();
                anim.SetBool("Walk_Anim", true);
                if (Vector3.Distance(transform.position, playerTransform.position) < attackRange)
                {
                    anim.SetBool("Walk_Anim", false);
                    anim.SetBool("Roll_Anim", true);
                    StartCoroutine("AttackPlayerCall");
                }
            }
            //CheckKey();
            //gameObject.transform.eulerAngles = rot;
            if (Vector3.Distance(transform.position, playerTransform.position) < attackpoint)
            {
                returnpos = true;

                if (attackingplayer)
                {
                    attackingplayer = false;
                    playerhealth.ReduceHealth();
                }

            }
            if (returnpos) ReturnToOPos();
        }
        else
        {
            anim.SetBool("Open_Anim", false);
        }
        
    }

    private void ReturnToOPos()
    {
        canbeshot = true;
        //audio.PlayOneShot(RSOpen);
        anim.SetBool("Roll_Anim", false);
        anim.SetBool("Walk_Anim", true);
        audio.PlayOneShot(RSWalk);
        float step = SphereAttackSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), oposGO.transform.position, step);
        if (new Vector3(transform.position.x, transform.position.y, transform.position.z) == oposGO.transform.position) { returnpos = false; attackingplayer = true; setplayerloc = true; canbeshot = false; }
    }

    public void LookAtPlayer()
    {
       // Debug.Log("Look at Player");
        Vector3 relativePos = playerTransform.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1 * Time.deltaTime);
        Vector3 ShootPos = new Vector3();
        ShootPos = transform.position;
    }
    public void SetPlayerlocation()
    {
        if (setplayerloc)
        {
            ppos = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
            setplayerloc = false;
        }
    }
    IEnumerator AttackPlayerCall()
    {
        yield return new WaitForSeconds(2f);
        AttackPlayer();
    }
    public void AttackPlayer()
    {
        audio.PlayOneShot(RSRoll);
        //attackingplayer = false;
        float step = SphereAttackSpeed * Time.deltaTime;
        SetPlayerlocation();
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), ppos, step);
        //transform.position += (ppos - new Vector3(transform.position.x, transform.position.y, transform.position.z)).normalized * step;
        StartCoroutine("AttackPlayerTime");
    }
    IEnumerator AttackPlayerTime()
    {
        yield return new WaitForSeconds(2.75f);
        returnpos = true;
        attackingplayer = false;
    }

}
