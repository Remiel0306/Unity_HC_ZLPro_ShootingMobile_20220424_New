using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace remiel
{
    /// <summary>
    /// 攻擊系統
    /// </summary>
    public class SystemAttack : MonoBehaviour
    {
        [SerializeField] public Button btnFire; 
        [SerializeField, Header("子彈")] private GameObject goBullet;
        [SerializeField, Header("數量")] private int bulletCountMax = 3;
        [SerializeField, Header("子彈發射位置")] private Transform traFire;
        [SerializeField, Header("子彈發射速度"), Range(0, 3000)] private int speedFire = 500;

        private int bulletCountCurrent;

        private void Awake()
        {
            // 發射按鈕.點擊.添加監聽器(開槍方法) 按下發射按鈕執行開槍方法
            btnFire.onClick.AddListener(Fire);
        }


        /// <summary>
        /// 開槍
        /// </summary>
        private void Fire()
        {
            Instantiate(goBullet, traFire.position, Quaternion.identity);
        }
    }
}

