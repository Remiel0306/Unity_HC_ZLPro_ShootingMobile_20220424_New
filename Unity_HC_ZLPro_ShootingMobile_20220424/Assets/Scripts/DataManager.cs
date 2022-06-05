using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace remiel
{
    public class DataManager : MonoBehaviour
    {
        // 新增部屬做的網址，有變更gas都要更新
        private string gasLink = "https://script.google.com/macros/s/AKfycbzazl0klhkxSAlmVBuU5Z5drZdBBNynWCS9DqrOgZ1FGeUDzivCg-uH7v1Npyg3HYUeUw/exec";
        private WWWForm form;

        private Button btnGetData;
        private Text textPlayerName;

        private void Start()
        {
            btnGetData = GameObject.Find("取得玩家資料按鈕").GetComponent<Button>();
            textPlayerName = GameObject.Find("玩家名稱").GetComponent<Text>();
            btnGetData.onClick.AddListener(GetGASData);
        }

        /// <summary>
        /// 取的 GAS 資料
        /// </summary>
        private void GetGASData()
        {
            form = new WWWForm();
            form.AddField("method", "取得");

            StartCoroutine(StartGetGaASData());
        }

        private IEnumerator StartGetGaASData()
        {
            // 新增往也連線要求(gasLink，標單資料)
            using (UnityWebRequest  www = UnityWebRequest.Post(gasLink, form))
            {
                // 等待網頁連線要求
                yield return www.SendWebRequest();
                // 玩家名稱 = 連線要求下載的文字訊息
                textPlayerName.text = www.downloadHandler.text;
            }
        }
    }
}
