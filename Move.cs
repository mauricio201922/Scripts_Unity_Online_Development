using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Move : MonoBehaviour
{

    // speed Move
    private float speed = 5;

    // Player
    private PhotonView pv;

    // UI
    public GameObject cameraUI;
    public Text name;

    // Balas
    public GameObject posBala; 
    public GameObject bala;

    // Transform do objeto q a camera tem q seguir
    public static Transform alvo;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        name.text = pv.Owner.NickName;

        if (pv.IsMine)
        {
            alvo = transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pv.IsMine)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(new Vector3(0, -300 * Time.deltaTime, 0));
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(new Vector3(0, 300 * Time.deltaTime, 0));
            }

            // Destacando o jogador
            name.color = Color.red;

            // Se pressionar o botão Space, irá atirar
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //pv.RPC("Tiro", RpcTarget.All);
                Tiro();
            }

        }

        // deixando o canvas sempre de frente à câmera
        //cameraUI.gameObject.transform.LookAt(MinhaCam.cam.transform);

    }

    public void Tiro()
    {
        PhotonNetwork.Instantiate(bala.name, posBala.transform.position, transform.rotation, 0);
    }
}
