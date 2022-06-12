using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace remiel
{
    public class DamageManager : MonoBehaviourPun
    {
        [SerializeField, Header("血量"), Range(0, 100)] private float hp = 200;
        [SerializeField, Header("擊中特效")] private GameObject goVFXHit;
        [SerializeField, Header("溶解著色器")] private Shader shaderCissolve;

        private float hpMax;
        
        private string nameBullet = "子彈";
                
        // 模型所有網格渲染元件，裡面包含材質球
        private SkinnedMeshRenderer[] smr;

        [HideInInspector] public Image imgHp;
        [HideInInspector] public TextMeshProUGUI textHp;

        private Material materialDissolve;
        private SystemControl systemControl;
        private SystemAttack systemAttack;

        private void Awake()
        {
            systemControl = GetComponent<SystemControl>();
            systemAttack = GetComponent<SystemAttack>();

            hpMax = hp;

            // 取得子物件"們"的原件
            smr = GetComponentsInChildren<SkinnedMeshRenderer>();
            // 新增 溶解著色氣 材質球
            materialDissolve = new Material(shaderCissolve);
            // 利用迴圈賦予所有仔物件 溶解材質球
            for(int i =0; i < 0; i++)
            {
                smr[i].material = materialDissolve;
            }

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

            // 連線.生成(特效.擊中座標.角度)
            PhotonNetwork.Instantiate(goVFXHit.name, posHit, Quaternion.identity);
            // 如果 血量<=0 就透過RPC 同步所有人得物件進行死亡方法
            if (hp <= 0) photonView.RPC("Dead", RpcTarget.All);
                       
        }

        // 需要同步的方法必須添加PunRPC 屬性 Remote Procedure Call 遠端程式呼叫
        [PunRPC]
        private void Dead()
        {
            StartCoroutine(Dissolve());
        }

        private IEnumerator Dissolve()
        {
            systemControl.enabled = false;
            systemAttack.enabled = false;
            systemControl.traDirectionIcon.gameObject.SetActive(false);

            float valueDissolve = 5;                                     // 溶解數值起始值                   

            for(int i = 0; i < 30; i++)                                  // 迴圈執行遞減
            {
                valueDissolve -= 0.3f;                                   // 溶解數值遞減0.3f
                materialDissolve.SetFloat("dissolve", valueDissolve);   // 更新著色氣數系，注意要控制
                yield return new WaitForSeconds(0.08f);                  // 等待
            }

            ReturnToLobby();
        }

        private void ReturnToLobby()
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.LeaveRoom();              // 離開房間
                PhotonNetwork.LoadLevel("遊戲大廳");     // 回到大廳場景
            }
        }
    }
}