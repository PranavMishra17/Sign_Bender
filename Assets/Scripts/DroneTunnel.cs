using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneTunnel : MonoBehaviour
{
    public GameObject drone;
    public Transform dronepoint;
    public DroneController dctrl;
    public float setHeight = 0f;
    private bool used;
    public bool bosstrigger = false;
    public HealthEnemy he;
    // Start is called before the first frame update
    void Start()
    {
        used = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateEnemy()
    {
        var newDrone = Instantiate(drone, dronepoint.transform.position, Quaternion.identity) as GameObject;
        newDrone.GetComponent<DroneController>().attackRange = 100f;
        newDrone.GetComponent<DroneController>().awarenessRange = 110f;
        newDrone.GetComponent<DroneController>().height = setHeight;
    }
    public void OnTriggerEnter(Collider co)
    {
        if (co.tag == "Player" && used && !bosstrigger)
        {
            var newDrone = Instantiate(drone, dronepoint.transform.position, Quaternion.identity) as GameObject;
            newDrone.GetComponent<DroneController>().height = setHeight;
            Debug.Log("In here");
            //he.canbeHit = true;
        }
        else if (co.tag == "Player" && used && bosstrigger)
        {
            InvokeRepeating("CreateEnemy", 1f,10f); he.canbeHit = true;
        }
    }
    public void OnTriggerExit(Collider co)
    {
        if (co.tag == "Player")
        {
            used = false;
        }
    }
}
