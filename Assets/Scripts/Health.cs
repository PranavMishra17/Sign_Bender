using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health = 1.00f;

    public bool inWater;
    public Image bloodOverlay;
    public cameraMovement cm;
    public FPSShooter fps;
    public Slider healthSlider;

    public AudioClip playerHit;
    public AudioClip playerhealthplus;
    AudioSource audio;
    public float throwspeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        Color c = bloodOverlay.color;
        c.a = health;
        bloodOverlay.color = c; health= 1;
    }

    // Update is called once per frame
    void Update()
    {
        Color c = bloodOverlay.color;

        if (health == 1) { c.a = 1f - health; healthSlider.gameObject.SetActive(false); }
        else { c.a = 1.05f - health; healthSlider.gameObject.SetActive(true); }
        if (c.a > 1) c.a = 1;

        bloodOverlay.color = c;
    }
    public void DeathSequence()
    {
        audio.clip = null;
        health = 0;
        fps.Death();
        cm.isAlive = false;
    }
    public void ReduceHealth()
    {
        health -= 0.21f;
        healthSlider.value = health;
        if (health < 0) { DeathSequence();  }
    }
    public void IncreaseHealth()
    {
        audio.PlayOneShot(playerhealthplus);
        health += 0.25f;
        if (health > 1) health = 1;
        healthSlider.value = health;
    }
    public void ReducePartialHealth()
    {
        audio.PlayOneShot(playerHit);
        if (inWater) InvokeRepeating("ReduceHealth", 0.1f, 3f);
        else CancelInvoke("ReduceHealth");
        if (health < 0) DeathSequence();
        healthSlider.value = health;
    }
    public void ThrowPlayer()
    {
        ReduceHealth();
        float step = throwspeed * Time.deltaTime;
        healthSlider.value = health;
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x, transform.position.y +2.5f, transform.position.z-10f), step);
    }

    public void OnCollisionEnter (Collision co)
    {

        if (co.gameObject.tag == "Water")
        {
            inWater = true;
            if (inWater) InvokeRepeating("ReduceHealth", 1.0f, 3f);
        }
        // If the object we hit is the enemy

        if (co.gameObject.tag == "Bullet")
        {
            audio.PlayOneShot(playerHit);
            //Debug.Log("Tag of obj " + co.collider.tag);
            ReduceHealth();
        }
        if (co.gameObject.tag == "Boss")
        {
            audio.PlayOneShot(playerHit);
            Debug.Log("Tag of obj " + co.collider.tag);
            ReduceHealth();
        }

    }
    public void OnTriggerEnter(Collider co)
    {
        //Debug.Log("Tag of obj " + co.tag);
        if (co.tag == "Cloud")
        {
            inWater = true;
            audio.PlayOneShot(playerHit);
            ReducePartialHealth();
        }
        if (co.gameObject.tag == "Health")
        {
            //Debug.Log("Tag of obj " + co.collider.tag);
            IncreaseHealth();
        }
    }
    public void OnTriggerExit(Collider co)
    {
        //Debug.Log("Tag of obj " + co.tag);
        if (co.tag == "Cloud")
        {
            inWater = false;
            ReducePartialHealth();
        }
    }
}