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
        // �s�W���ݰ������}�A���ܧ�gas���n��s
        private string gasLink =  "https://script.google.com/macros/s/AKfycbzoduwYKFNQwqWjiaeIja7WEmH9D8it95IgW9SSnIwyYvzpxEmh2bwB2-BL8SLGVwBi_A/exec";
        private WWWForm form;

        private Button btnGetData;
        private Text textPlayerName;
        private TMP_InputField inputField;

        private void Start()
        {
            btnGetData = GameObject.Find("���o���a��ƫ��s").GetComponent<Button>();
            textPlayerName = GameObject.Find("���a�W��").GetComponent<Text>();
            btnGetData.onClick.AddListener(GetGASData);

            inputField = GameObject.Find("��s���a�W��").GetComponent<TMP_InputField>();
            inputField.onEndEdit.AddListener(SetGASData);
        }

        /// <summary>
        /// ���� GAS ���
        /// </summary>
        private void GetGASData()
        {
            form = new WWWForm();
            form.AddField("method", "���o");

            StartCoroutine(StartGetGaASData());
        }

        private IEnumerator StartGetGaASData()
        {
            // �s�W���]�s�u�n�D(gasLink�A�г���)
            using (UnityWebRequest  www = UnityWebRequest.Post(gasLink, form))
            {
                // ���ݺ����s�u�n�D
                yield return www.SendWebRequest();
                // ���a�W�� = �s�u�n�D�U������r�T��
                textPlayerName.text = www.downloadHandler.text;
            }
        }

        private void SetGASData(string value)
        {
            form = new WWWForm();
            form.AddField("method", "�]�w");
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
