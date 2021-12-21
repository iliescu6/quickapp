using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsShowListener
{
    [SerializeField] float requiredAdTimeWait;
    float adTimer = 0;
    bool showedAd = false;

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize("4514641",true);

        
    }

    // Update is called once per frame
    void Update()
    {
        adTimer += Time.deltaTime;
        if (adTimer >= requiredAdTimeWait && !showedAd)
        {
            Advertisement.Load("Interstitial_Android");
            if (Advertisement.isInitialized)
            {
                 Advertisement.Show("Interstitial_Android");
                showedAd = true;
            }
        }
    }
    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == "Interstitial_Android" && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("great succes");
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        throw new System.NotImplementedException();
    }
}
