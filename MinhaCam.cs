using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MinhaCam : MonoBehaviour
{

    // variavei para as cameras
    public CinemachineVirtualCamera vcam;
    public Camera cam;

    void Awake()
    {
        Instantiate(cam);
        Instantiate(vcam);
    }
}
