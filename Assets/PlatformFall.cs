using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour
{
    public Rigidbody platform;
    private bool hasPlayerExited;
    float timer = 0f;
    public float timeToFall = 1.5f;

    void Start()
    {
        platform = gameObject.GetComponent<Rigidbody>();
        platform.isKinematic = true;
        platform.useGravity = false;
    }

    void OnTriggerEnter(Collider co)
    {
        if (co.tag == "Player")
        {
            //Debug.Log("Player collided");
            hasPlayerExited = true;
        }
            
    }

    void Update()
    {
        if (hasPlayerExited)
        {
            //Debug.Log("In here");
            timer += Time.deltaTime;

            if (timer > timeToFall)
            {
                //Debug.Log("Fall initiated");
                platform.isKinematic = false;
                platform.useGravity = true;
            }
        }
    }
}
