using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace remiel
{
    public class DamageManager : MonoBehaviour
    {
        [SerializeField, Header("血量"), Range(0, 100)] private float hp = 200;
        [SerializeField, Header("擊中特效")] private GameObject goVFXHit;

        private float hpMax;

        private string nameBullet = "子彈";
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

            // 連線.生成(特效.擊中座標.角度)
            PhotonNetwork.Instantiate(goVFXHit.name, posHit, Quaternion.identity);
        }
    }
}
