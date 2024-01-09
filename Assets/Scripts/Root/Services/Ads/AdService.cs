using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdService : IAdService
{
    private InterstitialAd _interstitialAd;
    private RewardedAd _rewardedAd;

    private string _rewardedAdKey = "";
    private string interstitialAdKey = "";

    public void Init()
    {
        MobileAds.Initialize(status => { });
    }

    public void DownloadInterstitial()
    {
        AdRequest adRequest = new AdRequest();

        InterstitialAd.Load(interstitialAdKey, adRequest, (interstitialAd, loadAdError) => 
        {
            _interstitialAd = interstitialAd;
        });
    }

    public RewardedAd DownloadRewarded()
    {
        RewardedAd rewardedAd1 = null;
        AdRequest adRequest = new AdRequest();

        RewardedAd.Load(_rewardedAdKey, adRequest, (rewardedAd, loadAdError) => 
        {
            rewardedAd1 = rewardedAd;
        });

        return rewardedAd1;
    }

    public void ShowRewardedAd()
    {
        RewardedAd rewardedAd = DownloadRewarded();

        if (rewardedAd.CanShowAd())
            rewardedAd.Show((reward) => 
            { 
                
            });
    }

    public void ShowInterstitialAd()
    {
        if (_interstitialAd.CanShowAd())
            _interstitialAd.Show();
    }
}
