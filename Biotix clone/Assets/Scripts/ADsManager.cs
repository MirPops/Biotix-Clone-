using UnityEngine;
using UnityEngine.Advertisements;

public class ADsManager : MonoBehaviour, IUnityAdsListener
{
    private string gameId = "4129403";
    private bool testMode = false;

    // Initialize the Ads listener and service:
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
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