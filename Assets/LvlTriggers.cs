using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LvlTriggers : MonoBehaviour
{
    private string ObjName;
    public Text textObj;
    bool first = false;
    bool t1, t2, t3, t4, t5, t6, t7, t0, t8;

    public Transform player;

    public FPSShooter fps;

    // Start is called before the first frame update
    void Start()
    {
        ObjName = gameObject.name;
        Debug.Log("name : " + ObjName);
        if (ObjName == "T1")
        {
            textObj.text = "LEVEL 1";
            StartCoroutine(FadeTextToFullAlpha(1f, textObj, 3));
            //StartCoroutine(FirstText(5));
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
                textObj.text = "Click/Drag on the RIGHT end of the screen to move the CAMERA";
                StartCoroutine(FadeTextToFullAlpha(1f, textObj, 4));
               // ImgL.gameObject.SetActive(true);
               // StartCoroutine(SetActive(ImgL.gameObject, 6));
                t1 = true;
                fps.swanPos = fps.t1;
            }
            if (ObjName == "T2" && !t2)
            {
                textObj.text = "Click on the CENTER of the screen to SHOOT";
                StartCoroutine(FadeTextToFullAlpha(1f, textObj, 5));
                //ImgC.gameObject.SetActive(true);
              //  StartCoroutine(SetActive(ImgC.gameObject, 6));
                t2 = true;
                fps.swanPos = fps.t2;
            }
            if (ObjName == "T3" && !t3)
            {
                textObj.text = "Drag your finger on the LEFT end of the screen to JUMP.\n(Drag your finger upwards on the LEFT end)";
                StartCoroutine(FadeTextToFullAlpha(1f, textObj, 5));
               // ImgR.gameObject.SetActive(true);
               // StartCoroutine(SetActive(ImgR.gameObject, 6));
                t3 = true;
                fps.swanPos = fps.t3;
            }
            if (ObjName == "T4" && !t4)
            {
                textObj.text = "Remember, you CANNOT shoot while running.\nStand still to shoot.";
                StartCoroutine(FadeTextToFullAlpha(1f, textObj, 5));
                t4 = true;
                fps.swanPos = fps.t4;
            }
            if (ObjName == "T5" && !t5)
            {
                textObj.text = "You can avoid ENEMY BULLETS by getting out of the way.";
                StartCoroutine(FadeTextToFullAlpha(1f, textObj, 5));
                t5 = true;
                fps.swanPos = fps.t5;
            }
            if (ObjName == "T6" && !t6)
            {
                textObj.text = "Health Elixir improves your health! Don't miss them.";
                StartCoroutine(FadeTextToFullAlpha(1f, textObj, 5));
                t6 = true;
                fps.swanPos = fps.t6;
            }
            if (ObjName == "T7" && !t7)
            {
                textObj.text = "EXCEPTIONAL! You have completed the TUTORIAL!\nBest of Luck for the rest of the game ;-)";
                StartCoroutine(FadeTextToFullAlpha(1f, textObj, 3));
                t7 = true;

                //LVL complete
                StartCoroutine(LoadScene("LVL1", 4));
            }
            if (ObjName == "T8" && !t8)
            {
                textObj.text = "The blood overlay on the screen indicates your health.\nIt turns more dense/opaque with each damage.\n";
                StartCoroutine(FadeTextToFullAlpha(1f, textObj, 6));
                t7 = true;
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
        textObj.text = "Click/Drag on the LEFT end of the screen to move the PLAYER";
        StartCoroutine(FadeTextToFullAlpha(1f, textObj, 5));
        //ImgR.gameObject.SetActive(true);
       // StartCoroutine(SetActive(ImgR.gameObject, 6));
        t0 = true;
    }

}
