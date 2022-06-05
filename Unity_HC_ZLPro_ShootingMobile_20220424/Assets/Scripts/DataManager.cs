using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace remiel
{
    public class DataManager : MonoBehaviour
    {
        // 新增部屬做的網址，有變更gas都要更新
        private string gasLink =  "https://script.google.com/macros/s/AKfycbzoduwYKFNQwqWjiaeIja7WEmH9D8it95IgW9SSnIwyYvzpxEmh2bwB2-BL8SLGVwBi_A/exec";
        private WWWForm form;

        private Button btnGetData;
        private Text textPlayerName;
        private TMP_InputField inputField;

        private void Start()
        {
            btnGetData = GameObject.Find("取得玩家資料按鈕").GetComponent<Button>();
            textPlayerName = GameObject.Find("玩家名稱").GetComponent<Text>();
            btnGetData.onClick.AddListener(GetGASData);

            inputField = GameObject.Find("更新玩家名稱").GetComponent<TMP_InputField>();
            inputField.onEndEdit.AddListener(SetGASData);
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

        private void SetGASData(string value)
        {
            form = new WWWForm();
            form.AddField("method", "設定");
            form.AddField("plauerName", inputField.text);

            StartCoroutine(StartSetGASData());
        }

        private IEnumerator StartSetGASData()
        {
            using (UnityWebRequest www = UnityWebRequest.Post(gasLink, form))
            {
                yield return www.SendWebRequest();
                textPlayerName.text = inputField.text;
                print(www.downloadHandler.text);
            }
        }
    }
}
