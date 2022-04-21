using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FPSShooter : MonoBehaviour
{
    public Camera cam;
    public Transform player;
    public Transform swanPos;
    public Transform t1,t2,t3,t4,t5,t6,t7,t9;

    public GameObject projectile;
    private Vector3 destinationV;

    public float projectileSpeed = 30f;
    private float timeToFire, buttonPressed;
    public float arcRange = 1;
    public float range = 250f;
    public float lowerlimit = -20f;

    public bool shootTrue = false;

    public Animator anim;
    public Animator player_anim;

    public GameObject muzzle;
    public GameObject playerHands;

    public Transform LeftFirePoint, RightFirePoint;

    private bool leftHand=false;
    public bool deathBool = false;

    public Ray ray;


    public GameObject bulletMark;
    public GameObject reticle;
    public GameObject target;

    public LayerMask canBeShot;

    public AudioClip playerShoot;
    public AudioClip playerDeath;
    AudioSource audio;
    public AudioSource bgaudio;
    Vector3 pos = new Vector3(200, 200, 0);

    public Text scoreCounter;
    public int score;

    public GameObject deathPanel;

    // Start is called before the first frame update
    private void Start()
    {
        score = 20;
        audio = GetComponent<AudioSource>();
        swanPos = t1;
        scoreCounter.text = score.ToString();
    }
    public void SetShootBool()
    {
        shootTrue = true;
    }


    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        if (deathBool)
        {
            Death();
        }
       // if (player.transform.position.y < lowerlimit)
       // {
          //  player.transform.position = swanPos.position;
       // }
    }
    public void Shoot()
    {
        decScore();

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Transform t_spawn = cam.transform;
        RaycastHit t_hit = new RaycastHit();

        if (Physics.Raycast(t_spawn.position, t_spawn.forward, out t_hit, 500f, canBeShot))
        {
            Vector3 colisionPoint = t_hit.point;
            if (leftHand)
            {

                leftHand = false;
                destinationV = colisionPoint - LeftFirePoint.transform.position;
                InstantiateProjectile(LeftFirePoint);

                anim.SetTrigger("AttackLeft");
            }
            else
            {
                leftHand = true;
                destinationV = colisionPoint - RightFirePoint.transform.position;
                InstantiateProjectile(RightFirePoint);

                anim.SetTrigger("AttackRight");
            }
        }
        else
        {
            if (leftHand)
            {
                leftHand = false;
                destinationV = ray.GetPoint(1000) - LeftFirePoint.transform.position;
                InstantiateProjectile(LeftFirePoint);
                anim.SetTrigger("AttackLeft");

            }
            else
            {
                leftHand = true;
                destinationV = ray.GetPoint(1000) - RightFirePoint.transform.position;
                InstantiateProjectile(RightFirePoint);
                anim.SetTrigger("AttackRight");

            }
        }

    }

    private void decScore()
    {
        score -= 1;
        scoreCounter.text = score.ToString();
        if (score == 0)
        {
            deathPanel.gameObject.GetComponentInChildren<Text>().text = "You ran out of energy";
            deathPanel.SetActive(true);
        }
    }

    public void incScore(int i)
    {
        score += i;
        scoreCounter.text = score.ToString();
    }

    void InstantiateProjectile(Transform firePoint)
    {
        
    audio.PlayOneShot(playerShoot);
    var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
    projectileObj.GetComponent<Rigidbody>().velocity = destinationV.normalized * projectileSpeed;

    iTween.PunchPosition(projectileObj, new Vector3(Random.Range(arcRange, arcRange), Random.Range(arcRange, arcRange), 0), Random.Range(0.5f, 2));

    Destroy(projectileObj, 5f);
    var muzzleObj = Instantiate(muzzle, firePoint.position, Quaternion.identity) as GameObject;
    Destroy(muzzleObj,2);
    }

    public void Restart()
    {
        SceneManager.LoadScene("LVL1");
    }

    public void Death()
    {
        reticle.SetActive(false);
        bgaudio.Stop();
        //audio.clip = playerDeath;
        audio.PlayOneShot(playerDeath);
       // anim.SetTrigger("Death");
        player_anim.SetTrigger("Death");
        StartCoroutine("Done");
       
    }
    IEnumerator Done()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(2);
        playerHands.SetActive(false);

        deathPanel.gameObject.GetComponentInChildren<Text>().text = "You Died!";
        deathPanel.SetActive(true);
    }
}