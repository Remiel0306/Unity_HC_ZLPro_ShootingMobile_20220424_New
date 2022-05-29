using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

/// <summary>
/// �j�U�޲z��
/// ���a���U��ԫ��s��}�l�ǰt�ж�
/// </summary>
// MonoBehaviourPunCallbacks �s�u�\��^�G���O
// �n�J�j�U��^�G�ګ��w���ٸ�
public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField, Header("�s�u���e��")]private GameObject goConnectView;
    [SerializeField, Header("��ԫ��s")] private Button btnBattle;
    [SerializeField, Header("�s�u�H��")] private Text textCountPlayer;
    [SerializeField, Header("�s�u�̤j�H��"), Range(0, 20)] private byte maxCountPlayer = 3;

    //�����s��{�����q���y�{
    // 1.���Ѥ��}����k
    // 2. ���s�b�I����]�w�I�s����k
    //����ƥ�:����C���ɰ���@���A��l�Ƴ]�w
    private void Awake()
    {
        // �ù�.�]�w�ѪR��(��,�e,�O�_���ù�)
        Screen.SetResolution(1024, 576, false);
        //�s�u���ϥγ]�w   
        PhotonNetwork.ConnectUsingSettings();
    }

    //�s�u�ܱ���x�A�b����ConnectUsingSettings��|�۰ʳs�u
    //override���\�����O����
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("<color=yellow>1. �w�g�i�J����x</color>");
        // Photon�s�u�A�[�J�j�U
        PhotonNetwork.JoinLobby();
    }

    //�s�u�ܤj�U���\��|���榹��k
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("<color=blue>2. ���\�s�u�j�U</color>");

        //��ԫ��s.���� = �Ұ�
        btnBattle.interactable = true;
    }

    //�}�l�s�u���
    public void StartConnect()
    {
        print("<color=brown>3. �}�l�s�u...</color>");

        goConnectView.SetActive(true);

        //Photon�s�u���H���[�J�ж�
        PhotonNetwork.JoinRandomRoom();
    }

    //�[�J�H���ж�����
    //��]:1.�~��ɭP 2.�٨S���ж�
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        print("<color=red>4. �[�J�H���ж�����</color>");

        RoomOptions ro = new RoomOptions(); //�s�W�ж��]�w����
        ro.MaxPlayers = maxCountPlayer;                  //���w�ж��̤j�H��
        PhotonNetwork.CreateRoom("", ro);   //�إߩж��õ����ж�����
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("<color=wite>5. �}�Ъ̶i�J�ж�</color>");
        int currrentCount = PhotonNetwork.CurrentRoom.PlayerCount;
        int maxCount = PhotonNetwork.CurrentRoom.MaxPlayers;

        textCountPlayer.text = "�s�u�H��" + currrentCount + " / " + maxCount;
        LoadGameSence(currrentCount, maxCount);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        print("<color=wite>6. ���a�i�J�ж�</color>");
        int currrentCount = PhotonNetwork.CurrentRoom.PlayerCount;
        int maxCount = PhotonNetwork.CurrentRoom.MaxPlayers;

        textCountPlayer.text = "�s�u�H��" + currrentCount + " / " + maxCount;
        LoadGameSence(currrentCount, maxCount);
    }

    /// <summary>
    /// ���J���a����
    /// </summary>
    private void LoadGameSence(int currrentCount, int maxCount)
    {
        if (currrentCount == maxCount)
        {
            // �z�LPhotom�s�u���a ���J���w���� "�C������"
            // ����������bBuild setting��
            PhotonNetwork.LoadLevel("�C������");
        }
    }
}
