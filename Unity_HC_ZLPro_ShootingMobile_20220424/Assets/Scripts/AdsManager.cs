using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;   //引用Api
using Unity.VisualScripting;

namespace remiel
{
    /// <summary>
    /// 按下看廣告按鈕後觀看廣告
    /// 看完廣告後添加金幣回饋
    /// </summary>
    public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField, Header("看完廣告的金幣"), Range(0, 1000)] private int addCoinValue = 100;
        private int coinPlayer;
        /// <summary>
        /// 廣告按鈕添加金幣
        /// </summary>
        private Button btnAdsAddCoin;

        private string gameIdAndroid = "4754887"; // 後台 Adnroid ID
        private string gameIdIos = "4754886";     // 後台 ios iD
        private string gameId;

        private string adsIdAndroid = "AddCoin";
        private string adsIdIos = "AddCoin";
        private string adsId;
        private Text textCoin;

        // 初始化成功方法
        public void OnInitializationComplete()
        {
            print("<color=green>廣告初始化成功</color>");
        }

        // 初始化失敗方法
        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            print("<color=red>廣告初始化失敗，原因:" + message + "</color>");
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            print("<color=yellow>廣告載入成功</color>");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            print("<color=red>廣告載入失敗，原因:" + message + "</color>");
        }

        /// <summary>
        /// 載入廣告
        /// </summary>
        private void LoadAds()
        {
            print("<color=blue>載入廣告，ID:" + adsId + "</color>");
            Advertisement.Load(adsId, this);
            ShowAds();
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            print("<color=blue>廣告顯示失敗" + placementId + "</color>");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            print("<color=blue>廣告顯示開始" + placementId + "</color>");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            print("<color=blue>廣告顯示點擊" + placementId + "</color>");
            coinPlayer += addCoinValue; 
            textCoin.text = coinPlayer.ToString();
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            throw new System.NotImplementedException();
        }

        private void ShowAds()
        {
            Advertisement.Show(adsId, this);
        }

        private void Awake()
        {
            textCoin = GameObject.Find("玩家金幣數量").GetComponent<Text>();
            btnAdsAddCoin = GameObject.Find("廣告按鈕添加金幣").GetComponent<Button>();
            btnAdsAddCoin.onClick.AddListener(LoadAds);

            InitializedAds();

            // #if 城市區塊判斷式，條件達成才會執行該區塊
            //如果 玩家 作業系統 是ios 就指定為 ios 廣告
            //否則如果 玩家 作業系統 是Android 就指定為 Android 廣告

#if UNIY_IOS
            adsId = adsIdIos;
#elif UNITY_ANDROID
            adsId = adsIdAndroid;
#endif
            //PC測試
            adsId = adsIdAndroid;
        }


        private void InitializedAds()
        {
            gameId = gameIdAndroid;
            Advertisement.Initialize(gameId, true, this);
        }


    }
}

