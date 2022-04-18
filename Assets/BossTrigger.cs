using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject boss;
    public GameObject stadium;
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
            StartCoroutine("ActivateBoss");
            boss.GetComponent<Animator>().SetBool("Open_Anim", true);
            stadium.GetComponent<Animator>().SetTrigger("Open");
        }
    }
    public IEnumerator ActivateBoss()
    {
        yield return new WaitForSeconds(4f);
        boss.GetComponent<BossController>().activate = true;
    }
}
