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
        [SerializeField, Header("��q"), Range(0, 100)] private float hp = 200;
        [SerializeField, Header("�����S��")] private GameObject goVFXHit;
        [SerializeField, Header("���ѵۦ⾹")] private Shader shaderCissolve;

        private float hpMax;
        
        private string nameBullet = "�l�u";
                
        // �ҫ��Ҧ������V����A�̭��]�t����y
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

            // ���o�l����"��"�����
            smr = GetComponentsInChildren<SkinnedMeshRenderer>();
            // �s�W ���ѵۦ�� ����y
            Material materialDissolve = new Material(shaderCissolve);
            // �Q�ΰj��ᤩ�Ҧ��J���� ���ѧ���y
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

            // �s�u.�ͦ�(�S��.�����y��.����)
            PhotonNetwork.Instantiate(goVFXHit.name, posHit, Quaternion.identity);
            // �p�G ��q<=0 �N�z�LRPC �P�B�Ҧ��H�o����i�榺�`��k
            if (hp <= 0) photonView.RPC("Dead", RpcTarget.All);

        }

        // �ݭn�P�B����k�����K�[PunRPC �ݩ� Remote Procedure Call ���ݵ{���I�s
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

            float valueDissolve = 5;                                     // ���ѼƭȰ_�l��                   

            for(int i = 0; i < 20; i++)                                  // �j����滼��
            {
                valueDissolve -= 0.3f;                                   // ���ѼƭȻ���0.3f
                materialDissolve.SetFloat("dissvolve", valueDissolve);   // ��s�ۦ��ƨt�A�`�N�n����
                yield return new WaitForSeconds(0.08f);                  // ����

            }
        }
    }
}