using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace remiel
{
    public class DamageManager : MonoBehaviour
    {
        [SerializeField, Header("��q"), Range(0, 100)] private float hp = 200;
        [SerializeField, Header("�����S��")] private GameObject goVFXHit;

        private float hpMax;

        private string nameBullet = "�l�u";
        public Image imgHp;

        private void Awake()
        {
            hpMax = hp;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name.Contains(nameBullet))
            {
                Damage(collision.contacts[0].point);
            }
        }

        private void Damage(Vector3 posHit)
        {
            hp -= 20;
            imgHp.fillAmount = hp / hpMax;

            // �s�u.�ͦ�(�S��.�����y��.����)
            PhotonNetwork.Instantiate(goVFXHit.name, posHit, Quaternion.identity);
        }
    }
}
