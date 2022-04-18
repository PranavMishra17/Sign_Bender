using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public float healthEnemy = 500f;
    public float timetodie = 2f;
    public Slider healthBar;
    //public Animator anim;
    public AudioClip BossDeath;
    public AudioClip explosion;
    AudioSource audio;
    public BossController bctrl;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        bctrl = gameObject.GetComponent<BossController>();
        audio = GetComponent<AudioSource>();
        healthBar.value = 500f;
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


    }
    public void ReduceHealth()
    {
        healthEnemy -= 40f;
        healthBar.value -= 40f;
        if (healthEnemy < 10f)
        {
            TimetoDie();
        }
    }
    private void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.tag == "PlayerBullet" && bctrl.canbeshot)
        {
            ReduceHealth();
        }
    }
    public void TimetoDie()
    {
        bctrl.CancelInvoke("Shoot");

        audio.PlayOneShot(BossDeath);

        Destroy(healthBar, timetodie);

    }
}