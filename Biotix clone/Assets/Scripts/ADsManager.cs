using UnityEngine;
using UnityEngine.Advertisements;

public class ADsManager : MonoBehaviour, IUnityAdsListener
{

    string gameId = "4129403";
    string mySurfacingId = "rewardedVideo";
    bool testMode = false;

    // Initialize the Ads listener and service:
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
        GameManager.OnEndLevel += ShowRewardedVideo;
    }

    public void ShowRewardedVideo()
    {

        Advertisement.Show();
        GameManager.SetPause(true);
        print("ready");
    }


    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished) { }
        else if (showResult == ShowResult.Skipped) { }
        else if (showResult == ShowResult.Failed) { }

        //GameManager.SetPause(false);
    }

    public void OnUnityAdsReady(string surfacingId) { }

    public void OnUnityAdsDidError(string message) { }

    public void OnUnityAdsDidStart(string surfacingId) { }


    public void OnDestroy()
        => Advertisement.RemoveListener(this);
}