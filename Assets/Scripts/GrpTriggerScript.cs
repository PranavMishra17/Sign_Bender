using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GrpTriggerScript : MonoBehaviour
{
    private string ObjName;
    public Text textObj;
    bool first = false;
    bool t1, t2, t3, t4, t5, t6, t7, t0, t8, t9;
    public Health health;

    public Transform player;

    public FPSShooter fps;
    public GameObject transition;

    // Start is called before the first frame update
    void Start()
    {
        ObjName = gameObject.name;
        if (ObjName == "T1")
        {
            textObj.text = "Welcome!\nUse the buttons below to start playing!";
            StartCoroutine(FadeTextToFullAlpha(1f, textObj, 6));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
   
    public IEnumerator FadeTextToFullAlpha(float t, Text i, int time)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        StartCoroutine(FadeTextToZeroAlpha(1f, textObj, time));

    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i, int time)
    {
        yield return new WaitForSeconds(time);
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        
    }
    public void OnTriggerEnter(Collider co)
    {
        if (co.tag == "Player")
        {
            if (ObjName == "T1" && !t1)
            {
                textObj.text = "The Joystick button on the RIGHT end of the screen helps you move the CAMERA \nThe Joystick button on the LEFT end of the screen helps you move the PLAYER";
                StartCoroutine(FadeTextToFullAlpha(1f, textObj, 15));
                t1 = true;
                fps.swanPos = fps.t1;
            }
            if (ObjName == "T2" && !t2)
            {
                textObj.text = "The J Button lets you jump. \nThe Fire button lets you shoot at the middle of the screen.";
                StartCoroutine(FadeTextToFullAlpha(1f, textObj, 15));
                t2 = true;
                fps.swanPos = fps.t2;
            }
            if (ObjName == "T3" && !t3)
            {
                textObj.text = "Great! \nNow try to aim and shoot at the target board";
                StartCoroutine(FadeTextToFullAlpha(1f, textObj, 12));
                t3 = true;
                fps.swanPos = fps.t3;
            }
            if (ObjName == "T7" && !t7)
            {
                textObj.text = "Use the Pause button at the top right to pause the game or navigate through settings.";
                StartCoroutine(FadeTextToFullAlpha(1f, textObj, 12));
                t7 = true;
                fps.swanPos = fps.t7;
            }
            if (ObjName == "T5" && !t5)
            {
                textObj.text = "Damage suffered are displayed via the blood overlay.\nHealth bar only appears when you are hurt.";
                StartCoroutine(FadeTextToFullAlpha(1f, textObj, 12));
                t5 = true;
                fps.swanPos = fps.t5;
                health.ReduceHealth();
            }
            if (ObjName == "T6" && !t6)
            {
                textObj.text = "Health Capsules like this improves your health! Don't miss them.";
                StartCoroutine(FadeTextToFullAlpha(1f, textObj, 12));
                t6 = true;
                fps.swanPos = fps.t6;
            }
            if (ObjName == "T9" && !t9)
            {
                transition.SetActive(true);
                t9 = true;
                //fps.uic.UnlockAM5();
                StartCoroutine(LoadScene( "LVL1", 6));

                //LVL complete
            }
            if (ObjName == "T4" && !t4)
            {
                textObj.text = "Shoot and Destroy the robot.";
                StartCoroutine(FadeTextToFullAlpha(1f, textObj, 10));
                t4 = true;
                fps.swanPos = fps.t4;

                //LVL complete
            }
        }
        if (co.tag == "PlayerBullet")
        {

            if (ObjName == "T8" && !t8)
            {
                textObj.text = "Bullseye!\n";
                StartCoroutine(FadeTextToFullAlpha(1f, textObj, 5));
                t8 = true;
            }
        }

    }

    public IEnumerator SetActive(GameObject go, int time)
    {
        yield return new WaitForSeconds(time);
        go.SetActive(false);
    }
    public IEnumerator LoadScene(string nextScene, int time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName: nextScene);
    }
    
    public IEnumerator FirstText(int time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log(" in 2nd text ");
        textObj.text = "Click/Drag the joystick on the LEFT end of the screen to move the PLAYER";
        StartCoroutine(FadeTextToFullAlpha(1f, textObj, 5));
        t0 = true;
    }
}
