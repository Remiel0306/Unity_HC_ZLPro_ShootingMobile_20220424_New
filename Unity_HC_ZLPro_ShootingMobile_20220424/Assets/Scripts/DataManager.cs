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
        // �s�W���ݰ������}�A���ܧ�gas���n��s
        private string gasLink = "https://script.google.com/macros/s/AKfycbzazl0klhkxSAlmVBuU5Z5drZdBBNynWCS9DqrOgZ1FGeUDzivCg-uH7v1Npyg3HYUeUw/exec";
        private WWWForm form;

        private Button btnGetData;
        private Text textPlayerName;

        private void Start()
        {
            btnGetData = GameObject.Find("���o���a��ƫ��s").GetComponent<Button>();
            textPlayerName = GameObject.Find("���a�W��").GetComponent<Text>();
            btnGetData.onClick.AddListener(GetGASData);
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
    }
}
