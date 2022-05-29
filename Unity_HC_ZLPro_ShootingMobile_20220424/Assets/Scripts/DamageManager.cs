using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace remiel
{
    public class DamageManager : MonoBehaviourPun
    {
        [SerializeField, Header("��q"), Range(0, 100)] private float hp = 200;
        [SerializeField, Header("�����S��")] private GameObject goVFXHit;

        private float hpMax;

        private string nameBullet = "�l�u";
        [HideInInspector] public Image imgHp;
        [HideInInspector] public Text textHp;

        private void Awake()
        {
            hpMax = hp;

            if (photonView.IsMine) textHp.text = hp.ToString();
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

            hp = Mathf.Clamp(hp, 0, hpMax);
            textHp.text = hp.ToString();

            // �s�u.�ͦ�(�S��.�����y��.����)
            PhotonNetwork.Instantiate(goVFXHit.name, posHit, Quaternion.identity);
        }
    }
}
