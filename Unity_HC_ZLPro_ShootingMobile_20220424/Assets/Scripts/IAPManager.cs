using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using UnityEngine.Video;

namespace remiel
{
    public class IAPManager : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        [SerializeField, Header("�ʶR�ֽ����s")] private IAPButton iapBuySkinRed;
        [SerializeField, Header("�ʶR���ܰT��")] private Text textIAPTip;


        private bool hasSkinRed;

        private void Awake()
        {
            // ����ֽ����ʫ��s �ʶR���\�� �K�[��ť�� (�ʶR���\��k)
            iapBuySkinRed.onPurchaseComplete.AddListener(PurchaseCompleteSkinRed);
            // ����ֽ����ʫ��s �ʶR���ѫ� �K�[��ť��(�ʶR���Ѥ�k)
            iapBuySkinRed.onPurchaseFailed.AddListener(PurchaseFailedSkinRed);
        }

        /// <summary>
        /// �ʶR���\
        /// </summary>
        private void PurchaseCompleteSkinRed(Product product)
        {
            textIAPTip.text = product.ToString() + "�ʶR���\:";

            hasSkinRed = true;

            Invoke("HiddenIAPTip", 2);
        }

        /// <summary>
        /// �ʶR����
        /// </summary>
        private void PurchaseFailedSkinRed(Product product, PurchaseFailureReason reason)
        {
            textIAPTip.text = product.ToString() + "�ʶR���ѡA��]:" + reason;

            Invoke("HiddenIAPTip", 2);
        }
        
        /// <summary>
        /// ���ä��ʴ��ܰT��
        /// </summary>
        private void HiddenIAPTip()
        {
            textIAPTip.text = "";
        }
    }
}
