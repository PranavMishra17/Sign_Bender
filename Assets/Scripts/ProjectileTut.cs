using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTut : MonoBehaviour
{
    private bool collided;
    public GameObject impactVFX;

    public LayerMask canBeShot;
    public GameObject bulletMark;
    public string notToHit = "Player";


    public float force = 3f;
    float Val_Slice = 100f;

    public AudioClip explosion;
    AudioSource audio;

    private void Start()
    {
        //audio = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision co)
    {
       // Debug.Log("Tag of obj " + co.collider.tag);

        if (co.gameObject.tag != "Bullet" && co.gameObject.tag != notToHit && !collided)
        {
            collided = true;

            var impact = Instantiate(impactVFX, co.contacts[0].point, Quaternion.identity) as GameObject;

            AudioSource audi = impact.GetComponent<AudioSource>();
            audi.spatialBlend = 1;
            audi.PlayOneShot(explosion);

            GameObject t_BulletMark = Instantiate(bulletMark, co.contacts[0].point + co.contacts[0].normal * 0.001f, Quaternion.identity) as GameObject;
            t_BulletMark.transform.LookAt(co.contacts[0].point + co.contacts[0].normal * 1f);
           
            Destroy(impact, 2);
            Destroy(gameObject);
            Destroy(t_BulletMark, 5);
            //FadeAndDestroy(t_BulletMark);
            //instantiate explosion of projectile
        }
        // If the object we hit is the enemy
        if (co.gameObject.tag == "Enemy")
        {
            // Calculate Angle Between the collision point and the player
            Vector3 dir = co.contacts[0].point - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            //GetComponent<Rigidbody>().AddForce(dir * force);
        }
    }
    void FadeAndDestroy(GameObject gm)
    {
        //Loop until slice value reaches zero
        while (Val_Slice > 0.0f)
        {
            //Reduce slice value over time
            Val_Slice -= Time.deltaTime;

            //Apply the slice value to shader(change the string "_SliceAmount" to ur shader property string)
            var myMat = gm.GetComponent<Renderer>().material;
            //myMat.SetFloat("Sprites/Default (Shader)", Val_Slice);
            myMat.SetFloat("_MainTex", Val_Slice);
            //Use this line if you want to increase the delay in decrementing
            //yield return new WaitForSeconds(0.1f);

        }
        Destroy(gm);
    }
}
