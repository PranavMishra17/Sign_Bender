using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFreeAnim : MonoBehaviour {

	Vector3 rot = Vector3.zero;
	float rotSpeed = 40f;
	Animator anim;
    public float SphereAttackSpeed = 100f;
    public float SphereWalkSpeed = 20f;

    public float awarenessRange = 50f;
    public float attackRange = 30f;
    public float attackpoint = 15f;

    public Transform playerTransform;
    AudioSource audio;

    public bool attackingplayer = true;
    //public bool attackingplayer = false;

    // Use this for initialization
    void Awake()
	{
		anim = gameObject.GetComponent<Animator>();
		gameObject.transform.eulerAngles = rot;
	}
    void Start()
    {

        playerTransform = GameObject.Find("Player").transform;
        audio = GetComponent<AudioSource>();
       // audio.clip = droneMove;
       // audio.Play();
    }

    // Update is called once per frame
    void Update()
	{
        if (Vector3.Distance(transform.position, playerTransform.position) < awarenessRange && attackingplayer)
        {
            anim.SetBool("Open_Anim", true);
            LookAtPlayer();
            if (Vector3.Distance(transform.position, playerTransform.position) < attackRange)
            {
                anim.SetBool("Open_Anim", false);
                AttackPlayer();

            }
        }
        else anim.SetBool("Open_Anim", false);
        CheckKey();
		gameObject.transform.eulerAngles = rot;
	}

	void CheckKey()
	{
		// Walk
		if (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z)) < awarenessRange)
        {
            anim.SetBool("Walk_Anim", true);
            if (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z)) < attackpoint)
            {
                anim.SetBool("Roll_Anim", true);
            }



            }
		else if (Input.GetKeyUp(KeyCode.W))
		{
			anim.SetBool("Walk_Anim", false);
		}

		// Rotate Left
		if (Input.GetKey(KeyCode.A))
		{
			rot[1] -= rotSpeed * Time.fixedDeltaTime;
		}

		// Rotate Right
		if (Input.GetKey(KeyCode.D))
		{
			rot[1] += rotSpeed * Time.fixedDeltaTime;
		}

		// Roll
		if (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z)) < attackpoint)
		{
            anim.SetBool("Roll_Anim", true);
            float step = SphereAttackSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z - 10f), step);

		}

		// Close 
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			if (!anim.GetBool("Open_Anim"))
			{
				anim.SetBool("Open_Anim", true);
			}
			else
			{
				anim.SetBool("Open_Anim", false);
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
    public void AttackPlayer()
    {
        anim.SetBool("Roll_Anim", true);
        float step = SphereAttackSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z - 10f), step);

    }

    public void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.tag == "Player")
        {
            if (anim.GetBool("Roll_Anim"))
            {
                anim.SetBool("Roll_Anim", false);
                attackingplayer = false;
            }
        }
        if (co.gameObject.tag == "PlayerBullet")
        {
            Debug.Log("ReduceHealth");
        }
    }

}
