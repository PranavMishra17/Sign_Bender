using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossMusicTrigger : MonoBehaviour
{

    public AudioSource asrc;
    public AudioClip bg;
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
            asrc.clip = bg;
            asrc.Play();
        }
    }
}
