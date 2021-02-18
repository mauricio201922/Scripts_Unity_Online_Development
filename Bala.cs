using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bala : MonoBehaviourPunCallbacks
{

    private Rigidbody bala;

    // Start is called before the first frame update
    void Start()
    {
        bala = GetComponent<Rigidbody>();
        bala.AddRelativeForce(Vector3.forward * 10, ForceMode.Impulse);
        Destroy(this.gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetComponent<PhotonView>().IsMine)
        {
            this.GetComponent<PhotonView>().RPC("MataBala", RpcTarget.All);
        }
    }

    [PunRPC]
    public void MataBala()
    {
        Destroy(this.gameObject);
    }

}
