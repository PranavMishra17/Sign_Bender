using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDrone : MonoBehaviour
{
    public float healthEnemy = 100f;
    public float timetodie = 2f;
    public Slider healthBar;
    //public Animator anim;
    public Rigidbody rb;
    public GameObject go;
    public GameObject healthPortion;
    public GameObject healthPortioneffect;
    public AudioClip droneDeath;
    public AudioClip explosion;
    AudioSource audio;
    public DroneController dctrl;

    public Text scoreCounter;
    //public int score;
    public FPSShooter fps;

    // Start is called before the first frame update
    void Start()
    {
        dctrl = gameObject.GetComponent<DroneController>();
        audio = GetComponent<AudioSource>();
        healthBar.value = 1f;
        fps = GameObject.Find("Player").GetComponent<FPSShooter>();
        scoreCounter = GameObject.FindGameObjectWithTag("ScoreCounter").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }
    public void ReduceHealth()
    {
        healthEnemy -= 45f;
        healthBar.value -= 0.45f;
        if (healthEnemy < 0f)
        {
            TimetoDie();
        }
    }
    private void OnCollisionEnter(Collision co)
    {

        // If the object we hit is the enemy

        if (co.gameObject.tag == "PlayerBullet")
        {
            ReduceHealth();
        }
    }
    public void TimetoDie()
    {
        dctrl.CancelInvoke("Shoot");
       // audio.Stop();
        audio.PlayOneShot(droneDeath);
        rb.useGravity = true;
       // Destroy(go, timetodie);
       
        Destroy(healthBar, timetodie);
        StartCoroutine("HealthPortion");
        
    }
    IEnumerator HealthPortion()
    {
       
        yield return new WaitForSeconds(2);
        go.SetActive(false);
        var healthheff = Instantiate(healthPortioneffect, rb.transform.position, Quaternion.identity) as GameObject;
        var healthh = Instantiate(healthPortion, rb.transform.position, Quaternion.identity) as GameObject;
        AudioSource audi = healthheff.GetComponent<AudioSource>();
        audi.clip = explosion;
        audi.Play();
        Destroy(healthheff, 3f);
        Destroy(go, 4f);
        fps.incScore(15);
    }
}
