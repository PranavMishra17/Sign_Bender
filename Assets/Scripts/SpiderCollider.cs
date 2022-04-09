using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderCollider : MonoBehaviour
{
    public SpiderController sctrl;
    public DroneController dctrl;
    public JRController jctrl;
    bool triggerOK = true;
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
        if (co.tag == "Player" )
        {

            if (sctrl != null) sctrl.ShootPrep();
            if (dctrl != null) dctrl.ShootPrep();
            //if (jctrl != null) jctrl.ShootPrep();
        }
    }
    public void OnTriggerExit(Collider co)
    {
        //Debug.Log("Tag of obj " + co.tag);
        if (co.tag == "Player")
        {
            if (sctrl != null) sctrl.ShootEndPrep();
            if (dctrl != null) dctrl.ShootEndPrep();
            //if (jctrl != null) jctrl.ShootEndPrep();
        }
    }
}
