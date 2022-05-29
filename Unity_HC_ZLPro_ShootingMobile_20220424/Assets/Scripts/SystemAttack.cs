using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Photon.Pun;

namespace remiel
{
    /// <summary>
    /// 攻擊系統
    /// </summary>
    public class SystemAttack : MonoBehaviourPun
    {
        [SerializeField] public Button btnFire; 
        [SerializeField, Header("子彈")] private GameObject goBullet;
        [SerializeField, Header("數量")] private int bulletCountMax = 3;
        [SerializeField, Header("子彈發射位置")] private Transform traFire;
        [SerializeField, Header("子彈發射速度"), Range(0, 3000)] private int speedFire = 500;

        private int bulletCountCurrent;

        private void Awake()
        {
            if (photonView.IsMine)
            {
                // 發射按鈕.點擊.添加監聽器(開槍方法) 按下發射按鈕執行開槍方法
                btnFire.onClick.AddListener(Fire);
            }

           
        }


        /// <summary>
        /// 開槍
        /// </summary>
        private void Fire()
        {
            // 站存子彈 = 速度.生成(物件名稱.座標.角度)
            GameObject tempBullet = PhotonNetwork.Instantiate(goBullet.name, traFire.position, Quaternion.identity);
            // 站存子彈.取的元件<鋼體>().添加動力(腳色的方向 * 速度)
            tempBullet.GetComponent<Rigidbody>().AddForce(transform.forward * speedFire);
        }
    }
}

