using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdMan : MonoBehaviour
{
    private string gameId = "4725895";
    string mySurfacingId = "Rewarded_Android";
    bool testMode = false;  // set to false to view actual ads
    public FPSShooter fps;
    public Health hth;


    private void Awake()
    {
        InitialiseAds();
    }
    // Start is called before the first frame update
    void Start()
    {
       // Advertisement.AddListener(this);
    }

    private void InitialiseAds()
    {
        Advertisement.Initialize(gameId, testMode);
    }

    public void ShowInterstitialAd()
    {
        Debug.Log(string.Format("Platform is {0}supported\nUnity Ads {1}initialized", Advertisement.isSupported ? "" : "not ", Advertisement.isInitialized ? "" : "not "));
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady("Interstitial_Android"))
        {
            Advertisement.Show("Interstitial_Android");
            // Replace mySurfacingId with the ID of the placements you wish to display as shown in your Unity Dashboard.
        }
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }

    public void ShowRewardedVideo1()
    {
        Debug.Log(string.Format("Platform is {0}supported\nUnity Ads {1}initialized", Advertisement.isSupported ? "" : "not ", Advertisement.isInitialized ? "" : "not "));
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady("Rewarded_Android"))
        {
            Advertisement.Show("Rewarded_Android");
            Debug.Log("Ad code is Rewarded_Android");
        }
        else
        {
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
    }
    public void ShowRewardedVideo2()
    {
        Debug.Log(string.Format("Platform is {0}supported\nUnity Ads {1}initialized", Advertisement.isSupported ? "" : "not ", Advertisement.isInitialized ? "" : "not "));
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady("RewardedRevive"))
        {
            Advertisement.Show("RewardedRevive");
            Debug.Log("Ad code is RewardedRevive" );
        }
        else
        {
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
    }
    public void ShowBanner()
    {
        //Debug.Log(string.Format("Platform is {0}supported\nUnity Ads {1}initialized", Advertisement.isSupported ? "" : "not ", Advertisement.isInitialized ? "" : "not "));
        if (Advertisement.IsReady("Banner_Android"))
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show("Banner_Android");
            Debug.Log("Banner function accessed");

        }
        else
        {
            StartCoroutine(RepeatShowBanner());
        }
    }

    IEnumerator RepeatShowBanner()
    {
        yield return new WaitForSeconds(0.5f);
        ShowBanner();
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }
    public void InitialiseAd()
    {
        Advertisement.Initialize(gameId, testMode);
        Debug.Log(string.Format("Platform is {0}supported\nUnity Ads {1}initialized", Advertisement.isSupported ? "" : "not ", Advertisement.isInitialized ? "" : "not "));
    }
    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string adCode, ShowResult showResult)
    {
        Debug.Log("Ad code is  " + adCode);
        if (adCode == "RewardedRevive")
        {
            
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
                hth.ReviveSequence();
                fps.uic.UnlockAM4();
                Debug.Log("Reward successfull ");
            }
        else if (showResult == ShowResult.Skipped)
        {
                Debug.Log("Reward skipped ");
            }
        else if (showResult == ShowResult.Failed)
        {
                Debug.Log("Reward error ");
            }
        }
        else
        {
            // Define conditional logic for each ad completion status:
            if (showResult == ShowResult.Finished)
            {
                fps.IncreaseScoreReward();
                fps.uic.UnlockAM4();
                Debug.Log("Reward successfull ");
            }
            else if (showResult == ShowResult.Skipped)
            {
                Debug.Log("Reward skipped ");
            }
            else if (showResult == ShowResult.Failed)
            {
                Debug.Log("Reward error ");
            }
        }
    }

    public void OnUnityAdsReady(string surfacingId)
    {
        // If the ready Ad Unit or legacy Placement is rewarded, show the ad:
        if (surfacingId == mySurfacingId)
        {
            // Optional actions to take when theAd Unit or legacy Placement becomes ready (for example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string surfacingId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}
