using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Photon.Pun;

namespace remiel
{
    /// <summary>
    /// �����t��
    /// </summary>
    public class SystemAttack : MonoBehaviourPun
    {
        [SerializeField] public Button btnFire; 
        [SerializeField, Header("�l�u")] private GameObject goBullet;
        [SerializeField, Header("�ƶq")] private int bulletCountMax = 3;
        [SerializeField, Header("�l�u�o�g��m")] private Transform traFire;
        [SerializeField, Header("�l�u�o�g�t��"), Range(0, 3000)] private int speedFire = 500;

        private int bulletCountCurrent;

        private void Awake()
        {
            if (photonView.IsMine)
            {
                // �o�g���s.�I��.�K�[��ť��(�}�j��k) ���U�o�g���s����}�j��k
                btnFire.onClick.AddListener(Fire);
            }

           
        }


        /// <summary>
        /// �}�j
        /// </summary>
        private void Fire()
        {
            // ���s�l�u = �t��.�ͦ�(����W��.�y��.����)
            GameObject tempBullet = PhotonNetwork.Instantiate(goBullet.name, traFire.position, Quaternion.identity);
            // ���s�l�u.��������<����>().�K�[�ʤO(�}�⪺��V * �t��)
            tempBullet.GetComponent<Rigidbody>().AddForce(transform.forward * speedFire);
        }
    }
}

