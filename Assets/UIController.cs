using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject settingPanel;
    public GameObject menuPanel;
    bool once = true;
    public Text mainText;
    // Start is called before the first frame update
    void Start()
    {
        AuthenticateUser();
        mainText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AuthenticateUser()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Signed in");
                mainText.text = "Signed in!";
                StartCoroutine("fadeText");
                //
            }
            else
            {
                mainText.text = "Coudn't sign in to Google Play Services";
                StartCoroutine("fadeText");
            }
        }
        );
    }

    public void postToLeaderboard(int newScore)
    {
        Social.ReportScore(newScore, GPGSIds.leaderboard_sign_smash_leaderboard, (bool success) => {
            if (success)
            {
                Debug.Log("Posted to LB");
            }
            else Debug.Log("Coudn't post to LB");
        });
    }
    public static void ShowLB()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_sign_smash_leaderboard);
    }
    public static void ShowAM()
    {
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }
    public static void UnlockAM1()
    {
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }
    public void LoadLVL()
    {
        if (Social.localUser.authenticated)
        {
            if (once)
            {
                mainText.text = "We recommend you complete the tutorial first.\nPress Play again if you have already.";
                StartCoroutine("fadeText");
                once = false;
            }
            else StartCoroutine("loadLevel");
        }
        else
        {
           
            mainText.text = "Logging in to Google Play Services";
            StartCoroutine("fadeText");
            AuthenticateUser();
        }
    }
    public void LoadTut()
    {
        if (Social.localUser.authenticated)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            mainText.text = "Logging in to Google Play Services";
            StartCoroutine("fadeText");
            AuthenticateUser();
        }
    }

    public void QuitApp()
    {
        StartCoroutine("appQuit");
        Debug.Log("Quiting Game");
    }
    IEnumerator fadeText()
    {
        yield return new WaitForSeconds(6f);
        mainText.text = "";
    }
    IEnumerator loadLevel()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("LVL1");
    }
    IEnumerator appQuit()
    {
        yield return new WaitForSeconds(3f);
        Application.Quit();
    }
    public void OpenSettings()
    {
        settingPanel.SetActive(true);
        menuPanel.SetActive(false);
    }
    public void CloseSettings()
    {
        settingPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

}
