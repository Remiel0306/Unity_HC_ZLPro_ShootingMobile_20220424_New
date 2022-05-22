using System.Collections;
using System.Collections.Generic;   // �ޥ� �t�ΤΩM�@��(��Ƶ��c, List, ArrayList...)
using System.Linq;  // �ޥ� �t�οԸ߻y��(����ഫ API)
using Photon.Pun;
using UnityEngine;

namespace remiel
{
    /// <summary>
    /// �C���޲z��
    /// �P�_�p�G�O�s�u�i�J�o���a
    /// �N�ͦ����⪫��(�Ԥh)
    /// </summary>
    public class GameManager : MonoBehaviourPun
    {
        [SerializeField, Header("���⪫��")] private GameObject goCharacter;
        [SerializeField, Header("���a�ͦ�����")] private Transform[] traSpawmPoint;

        [SerializeField]private List<Transform> traSpawmPointList;


        private void Awake()
		{
            traSpawmPointList = new List<Transform>();  // �s�W �M�檫��
            traSpawmPointList = traSpawmPoint.ToList(); // �G�P�ର�M���Ƶ��c
            
            // �p�G�O�s�u�i�J�����a�N�b���A���ͦ��Φ⪫��
            //if (photonView.IsMine) 
            //{
                int indexRandom = Random.Range(0, traSpawmPointList.Count); // ���o�H���M��(0, �M�檺����)
                Transform tra = traSpawmPointList[indexRandom];             // �����H���y��

                // Photon ���A���i��ͦ�(����.�W��,�y��,����);
                PhotonNetwork.Instantiate(goCharacter.name, Vector3.zero, Quaternion.identity);

                traSpawmPointList.RemoveAt(indexRandom);   // �R���w�g���o�L���ͦ��y�и��
            //}
		}
	}
}