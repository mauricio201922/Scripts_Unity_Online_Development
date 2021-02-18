using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CriaPlayer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(GameServer.inst.player.name, new Vector3(1, 1, -5), Quaternion.identity);
    }
}
