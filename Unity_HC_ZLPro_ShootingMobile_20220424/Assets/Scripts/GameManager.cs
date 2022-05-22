using System.Collections;
using System.Collections.Generic;   // 引用 系統及和一般(資料結構, List, ArrayList...)
using System.Linq;  // 引用 系統諮詢語言(資料轉換 API)
using Photon.Pun;
using UnityEngine;

namespace remiel
{
    /// <summary>
    /// 遊戲管理器
    /// 判斷如果是連線進入得玩家
    /// 就生成角色物件(戰士)
    /// </summary>
    public class GameManager : MonoBehaviourPun
    {
        [SerializeField, Header("角色物件")] private GameObject goCharacter;
        [SerializeField, Header("玩家生成物件")] private Transform[] traSpawmPoint;

        [SerializeField]private List<Transform> traSpawmPointList;


        private void Awake()
		{
            traSpawmPointList = new List<Transform>();  // 新增 清單物件
            traSpawmPointList = traSpawmPoint.ToList(); // 鄭烈轉為清單資料結構
            
            // 如果是連線進入的玩家就在伺服器生成及色物件
            //if (photonView.IsMine) 
            //{
                int indexRandom = Random.Range(0, traSpawmPointList.Count); // 取得隨機清單(0, 清單的長度)
                Transform tra = traSpawmPointList[indexRandom];             // 取的隨機座標

                // Photon 伺服器進行生成(物件.名稱,座標,角度);
                PhotonNetwork.Instantiate(goCharacter.name, Vector3.zero, Quaternion.identity);

                traSpawmPointList.RemoveAt(indexRandom);   // 刪除已經取得過的生成座標資料
            //}
		}
	}
}