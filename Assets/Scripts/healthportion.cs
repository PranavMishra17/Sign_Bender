using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthportion : MonoBehaviour
{
    public GameObject healthPortion;

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
        if (co.tag == "Player")
        {
            Destroy(healthPortion);
        }
    }

    }
