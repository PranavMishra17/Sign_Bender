using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenCheck : MonoBehaviour
{
    public Vector3 halfDimensions;
    private Vector3 boxCenter;
    public Transform boxcenter;
    public float speed = 10f;
    public LayerMask layerMask;
    bool active = true;

    public Animator anim;

    void Start()
    {
       
        boxCenter = boxcenter.transform.position;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(boxCenter, halfDimensions);
    }

    // Update is called once per frame
    void Update()
    {
        //OnDrawGizmos();
        if (active)
        {
            Collider[] enemiesRemaining = Physics.OverlapBox(boxCenter, halfDimensions, Quaternion.identity, layerMask);
            //Debug.Log("Enemy rem:" + enemiesRemaining.Length);
            if (enemiesRemaining.Length == 0)
            {
                OpenGate();
            }
        }
        
    }

    private void OpenGate()
    {
        Debug.Log("Arena Cleared.");
        anim.SetTrigger("Open");
        active = false;
        // anim.SetBool("Open", true);
    }
}
