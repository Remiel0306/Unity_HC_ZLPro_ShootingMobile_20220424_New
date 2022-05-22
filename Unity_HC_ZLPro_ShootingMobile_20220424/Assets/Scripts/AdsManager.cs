using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;   //�ޥ�Api
using Unity.VisualScripting;

namespace remiel
{
    /// <summary>
    /// ���U�ݼs�i���s���[�ݼs�i
    /// �ݧ��s�i��K�[�����^�X
    /// </summary>
    public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField, Header("�ݧ��s�i������"), Range(0, 1000)] private int addCoinValue = 100;
        private int coinPlayer;
        /// <summary>
        /// �s�i���s�K�[����
        /// </summary>
        private Button btnAdsAddCoin;

        private string gameIdAndroid = "4754887"; // ��x Adnroid ID
        private string gameIdIos = "4754886";     // ��x ios iD
        private string gameId;

        private string adsIdAndroid = "AddCoin";
        private string adsIdIos = "AddCoin";
        private string adsId;
        private Text textCoin;

        // ��l�Ʀ��\��k
        public void OnInitializationComplete()
        {
            print("<color=green>�s�i��l�Ʀ��\</color>");
        }

        // ��l�ƥ��Ѥ�k
        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            print("<color=red>�s�i��l�ƥ��ѡA��]:" + message + "</color>");
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            print("<color=yellow>�s�i���J���\</color>");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            print("<color=red>�s�i���J���ѡA��]:" + message + "</color>");
        }

        /// <summary>
        /// ���J�s�i
        /// </summary>
        private void LoadAds()
        {
            print("<color=blue>���J�s�i�AID:" + adsId + "</color>");
            Advertisement.Load(adsId, this);
            ShowAds();
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            print("<color=blue>�s�i��ܥ���" + placementId + "</color>");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            print("<color=blue>�s�i��ܶ}�l" + placementId + "</color>");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            print("<color=blue>�s�i����I��" + placementId + "</color>");
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
            textCoin = GameObject.Find("���a�����ƶq").GetComponent<Text>();
            btnAdsAddCoin = GameObject.Find("�s�i���s�K�[����").GetComponent<Button>();
            btnAdsAddCoin.onClick.AddListener(LoadAds);

            InitializedAds();

            // #if �����϶��P�_���A����F���~�|����Ӱ϶�
            //�p�G ���a �@�~�t�� �Oios �N���w�� ios �s�i
            //�_�h�p�G ���a �@�~�t�� �OAndroid �N���w�� Android �s�i

#if UNIY_IOS
            adsId = adsIdIos;
#elif UNITY_ANDROID
            adsId = adsIdAndroid;
#endif
            //PC����
            adsId = adsIdAndroid;
        }


        private void InitializedAds()
        {
            gameId = gameIdAndroid;
            Advertisement.Initialize(gameId, true, this);
        }


    }
}

