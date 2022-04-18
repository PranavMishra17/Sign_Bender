using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    public Transform portalDoor;
    public GameObject player;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = portalDoor.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider co)
    {
        Debug.Log("Trigger called");
        if (co.tag == "Player")
        {
            player.transform.position = pos;
            Debug.Log("Player detected");

        }
    }
}
