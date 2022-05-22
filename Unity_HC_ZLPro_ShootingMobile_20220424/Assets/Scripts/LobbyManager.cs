using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

/// <summary>
/// 大廳管理器
/// 玩家按下對戰按鈕後開始匹配房間
/// </summary>
// MonoBehaviourPunCallbacks 連線功能回乎類別
// 登入大廳後回乎我指定的稱號
public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField, Header("連線中畫面")]private GameObject goConnectView;
    [SerializeField, Header("對戰按鈕")] private Button btnBattle;
    [SerializeField, Header("連線人數")] private Text textCountPlayer;

    //讓按鈕跟程式溝通的流程
    // 1.提供公開的方法
    // 2. 按鈕在點擊後設定呼叫此方法
    //喚醒事件:撥放遊戲時執行一次，初始化設定
    private void Awake()
    {
        // 螢幕.設定解析度(長,寬,是否全螢幕)
        Screen.SetResolution(1024, 576, false);
        //連線的使用設定   
        PhotonNetwork.ConnectUsingSettings();
    }

    //連線至控制台，在執行ConnectUsingSettings後會自動連線
    //override允許父類別成員
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("<color=yellow>1. 已經進入控制台</color>");
        // Photon連線，加入大廳
        PhotonNetwork.JoinLobby();
    }

    //連線至大廳成功後會執行此方法
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("<color=blue>2. 成功連線大廳</color>");

        //對戰按鈕.互動 = 啟動
        btnBattle.interactable = true;
    }

    //開始連線對戰
    public void StartConnect()
    {
        print("<color=brown>3. 開始連線...</color>");

        goConnectView.SetActive(true);

        //Photon連線的隨機加入房間
        PhotonNetwork.JoinRandomRoom();
    }

    //加入隨機房間失敗
    //原因:1.品質導致 2.還沒有房間
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        print("<color=red>4. 加入隨機房間失敗</color>");

        RoomOptions ro = new RoomOptions(); //新增房間設定物件
        ro.MaxPlayers = 2;                  //指定房間最大人數
        PhotonNetwork.CreateRoom("", ro);   //建立房間並給予房間物件
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("<color=wite>5. 開房者進入房間</color>");
        int currrentCount = PhotonNetwork.CurrentRoom.PlayerCount;
        int maxCount = PhotonNetwork.CurrentRoom.MaxPlayers;

        textCountPlayer.text = "連線人數" + currrentCount + " / " + maxCount;
        LoadGameSence(currrentCount, maxCount);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        print("<color=wite>6. 玩家進入房間</color>");
        int currrentCount = PhotonNetwork.CurrentRoom.PlayerCount;
        int maxCount = PhotonNetwork.CurrentRoom.MaxPlayers;

        textCountPlayer.text = "連線人數" + currrentCount + " / " + maxCount;
        LoadGameSence(currrentCount, maxCount);
    }

    /// <summary>
    /// 載入玩家場景
    /// </summary>
    private void LoadGameSence(int currrentCount, int maxCount)
    {
        if (currrentCount == maxCount)
        {
            // 透過Photom連線玩家 載入指定場景 "遊戲場景"
            // 場景必須放在Build setting裡
            PhotonNetwork.LoadLevel("遊戲場景");
        }
    }
}
