using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameServer : MonoBehaviourPunCallbacks
{

    // UI
    public GameObject PainelL, PainelS;
    public InputField nome, room;
    public Text nick;

    // Objetos para instanciar
    public GameObject pontoRespawn, player;

    public static GameServer inst;

    private void Awake()
    {
        if(inst == null)
        {
            inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Login()
    {
        PhotonNetwork.NickName = nome.text;
        PhotonNetwork.ConnectUsingSettings();
        PainelL.SetActive(false);
        PainelS.SetActive(true);
    }

    public void CriaSala()
    {
        PhotonNetwork.JoinOrCreateRoom(room.text, new RoomOptions(), TypedLobby.Default);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado!");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby!");
        //PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Desconectado");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Nao entrou em nenhuma sala!!");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Entrou na sala!");
        print(PhotonNetwork.CurrentRoom.Name);
        print(PhotonNetwork.CurrentRoom.PlayerCount);
        //nick.text = PhotonNetwork.NickName;

        //PainelS.SetActive(false);
        //PhotonNetwork.Instantiate(player.name, new Vector3(1, 1, -5), Quaternion.identity, 0);

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(1);
        }
    }
}
