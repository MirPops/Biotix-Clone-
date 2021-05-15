using UnityEngine;
using UnityEngine.Advertisements;

public class ADsManager : MonoBehaviour, IUnityAdsListener
{

    string gameId = "4129403";
    //string mySurfacingId = "rewardedVideo";
    bool testMode = false;

    // Initialize the Ads listener and service:
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);

        //Time.timeScale = 4;
    }

    public static void ShowRewardedVideo()
    {
        Advertisement.Show();
        GameManager.SetPause(true);
    }


    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        GameManager.SetPause(false);
        GameManager.LoadNextLVL();
    }

    public void OnUnityAdsReady(string surfacingId) { }

    public void OnUnityAdsDidError(string message) { }

    public void OnUnityAdsDidStart(string surfacingId) { }


    public void OnDestroy()
        => Advertisement.RemoveListener(this);
}