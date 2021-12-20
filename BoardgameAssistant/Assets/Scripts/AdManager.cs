using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    float adTimer = 0;
    bool showedAd = false;
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize("4514641");
    }

    // Update is called once per frame
    void Update()
    {
        adTimer += Time.deltaTime;
        if (adTimer >= 4 && !showedAd)
        {
            if (Advertisement.isInitialized)
            {
               // Advertisement.Show("Interstitial_Android");
                showedAd = true;
            }
        }
    }
}
