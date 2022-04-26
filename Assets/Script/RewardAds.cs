using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;


public class RewardAds : MonoBehaviour
{
    private RewardedAd rewardedAd;          //������ ����
    //private InterstitialAd interstitial;    //���鱤��

    public void Start()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-3940256099942544~3347511713";    //�� ID(�ֵ�� Ȩ������ -> �� �������� Ȯ�ΰ���)
#elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
#else
            string appId = "unexpected_platform";
#endif
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        this.RequestRewardedAd();
    }

    // ������ ����
    private void RequestRewardedAd()
    {
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917";    //����ID(�ֵ�� Ȩ������ -> ���� �������� Ȯ�ΰ���)
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)  //���� ǥ�ǵɶ� ȣ��, ���� ����� �� ��� �Ǵ� ������ �����Ұ�
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)   //�����������ߴٸ� �ٽ� Ǯ���ٰ�
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        RequestRewardedAd();
    }

    public void HandleUserEarnedReward(object sender, Reward args)  //���������� ���� ����
    {
        string type = args.Type;    //�ֵ�� Ȩ������ -> ������� ���� -> ������ ��ǰ
        double amount = args.Amount;    ////�ֵ�� Ȩ������ -> ������� ���� -> ������ ����
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
        GameManager.instance.addDiamond();

    }

    public void ShowRewardedAd()    //���� ���
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
        else
        {
            Debug.Log("NOT Loaded Interstitial");
            RequestRewardedAd();
        }
    }
}
