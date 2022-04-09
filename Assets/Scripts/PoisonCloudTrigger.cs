using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloudTrigger : MonoBehaviour
{
    public cameraMovement cm;
    public Health hlth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider co)
    {
       //Debug.Log("Tag of obj " + co.tag);
        if (co.tag == "Player")
        {
            hlth.inWater = true;
            hlth.ReducePartialHealth();
        }
    }
    public void OnTriggerExit(Collider co)
    {
        //Debug.Log("Tag of obj " + co.tag);
        if (co.tag == "Player")
        {
            hlth.inWater = false;
            hlth.ReducePartialHealth();
        }
    }
}
