using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace remiel
{
    public class DestroySelf : MonoBehaviourPun
    {
        [SerializeField, Header("�R���ɶ�"), Range(0, 10)] private float timeDestroy = 5;
        [SerializeField, Header("�O�_�ݭn�I����R��")] private bool collisionDestroy;

        private void Awake()
        {
            Invoke("DestoryDelay", timeDestroy);
        }

        /// <summary>
        /// ����R����k
        /// </summary>
        private void DestroyDelay()
        {
            // �s�u.�R��(�C������) ��R�����A����������
            PhotonNetwork.Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collisionDestroy)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
