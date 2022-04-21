using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthEnemy : MonoBehaviour
{
    public float healthEnemy = 100f;
    public float timetodie = 1.8f;
    public Animator anim;
    public Rigidbody rb;
    public GameObject go;
    public Slider healthBar;

    public GameObject healthPortion;
    public GameObject healthPortioneffect;

    public AudioClip JRDeath;
    public AudioClip explosion;
    AudioSource audio;
    Transform jr;

    public bool canbeHit = true;

    public Text scoreCounter;
    //public int score;
    public FPSShooter fps;

    // Start is called before the first frame update
    void Start()
    {
        fps = GameObject.Find("Player").GetComponent<FPSShooter>();
        audio = GetComponent<AudioSource>();
        jr = GetComponent<Transform>();
        healthBar.value = 1f;
        scoreCounter = GameObject.FindGameObjectWithTag("ScoreCounter").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jr.position.y < -7f)
        {
            //ReduceHealth();
        }
    }
    public void ReduceHealth()
    {
        healthEnemy -= 60f;
        healthBar.value -= 0.6f;
        if (healthEnemy < 0f)
        {
            if (gameObject.tag == "Tower")
            {
                TowerDestroy();
            }
            else { anim.SetTrigger("Death"); TimetoDie(); }
        }
    }
    private void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.tag == "PlayerBullet" && canbeHit)
        {
            ReduceHealth();
        }
    }
    public void TimetoDie()
    {
       // score.ToString() = scoreCounter.text;
        audio.PlayOneShot(JRDeath);
        rb.useGravity = true;
       // Destroy(go, timetodie);

        StartCoroutine("HealthPortion");
    }
    public void TowerDestroy()
    {
        Debug.Log("TowerDestroy ");
        audio.PlayOneShot(JRDeath);

        gameObject.GetComponentInChildren<DroneTunnel>().CancelInvoke("CreateEnemy");
        var healthheff = Instantiate(healthPortioneffect, go.transform.position, Quaternion.identity) as GameObject;
        //go.SetActive(false);
        Destroy(go);

        //StartCoroutine("HealthPortion");
    }
    IEnumerator HealthPortion()
    {
        yield return new WaitForSeconds(1.6f);
        go.SetActive(false);
        var healthheff = Instantiate(healthPortioneffect, rb.transform.position, Quaternion.identity) as GameObject;
        var healthh = Instantiate(healthPortion, rb.transform.position, Quaternion.identity) as GameObject;
        AudioSource audi = healthheff.GetComponent<AudioSource>();
        audi.clip = explosion;
        audi.Play();
        Destroy(healthheff, 3f);
        Destroy(go, 4f);
        fps.incScore(10);
    }
}
