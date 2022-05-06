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
    public Slider volumeSlider;
    public AudioSource audsrc;
    public AudioClip menumusic;
    Scene currentscene;
    string sceneName;

    // Start is called before the first frame update
    void Start()
    {

        currentscene = SceneManager.GetActiveScene();
        sceneName = currentscene.name;
        if (sceneName == "HomeMenu")
        {
            audsrc.clip = menumusic;
            audsrc.Play();
            audsrc.loop = true;
            audsrc.volume = 0.7f;
        }
            mainText.text = "";
        InitializePlayGamesLogin();



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
    void InitializePlayGamesLogin()
    {
        var config = new PlayGamesClientConfiguration.Builder()
            // Requests an ID token be generated.  
            // This OAuth token can be used to
            // identify the player to other services such as Firebase.
            .RequestIdToken()
            .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    public void postToLeaderboard(int newScore, int deathScore)
    {
        Social.ReportScore(newScore, GPGSIds.leaderboard_sign_smash_leaderboard, (bool success) => {
            if (success)
            {
                Debug.Log("Posted to LB");
                if (deathScore==0)
                {
                    UnlockAM1();
                }
                else if (deathScore > 9)
                {
                    UnlockAM2();
                }
            }
            else Debug.Log("Coudn't post to LB");
        });
    }
    public void ShowLB()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_sign_smash_leaderboard);
        }
        else { AuthenticateUser();
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_sign_smash_leaderboard);
        }
            
    }
    public void ShowAM()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        }
        else { AuthenticateUser();
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        }
       
    }
    public void UnlockAM1()
    {
        PlayGamesPlatform.Instance.UnlockAchievement("CgkIsbq28q4cEAIQAg");
    }
    public void UnlockAM2()
    {
        PlayGamesPlatform.Instance.UnlockAchievement("CgkIsbq28q4cEAIQAw");
    }
    public void UnlockAM3()
    {
        PlayGamesPlatform.Instance.UnlockAchievement("CgkIsbq28q4cEAIQBA");
    }
    public void UnlockAM4()
    {
        PlayGamesPlatform.Instance.UnlockAchievement("CgkIsbq28q4cEAIQBQ");
    }
    public void UnlockAM5()
    {
        PlayGamesPlatform.Instance.UnlockAchievement("CgkIsbq28q4cEAIQBg");
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
            if (once)
            {
                mainText.text = "We recommend you complete the tutorial first.\nPress Play again if you have already.";
                StartCoroutine("fadeText");
                once = false;
            }
            else StartCoroutine("loadLevel");
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
            SceneManager.LoadScene("Tutorial");
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
    public void VolumeSlider()
    {
        //audsrc.GetComponent<Vol>().ChangeVol(volumeSlider.value);
        audsrc.volume = volumeSlider.value;
        Debug.Log("volumeSlider function called");
    }
}
