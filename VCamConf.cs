using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VCamConf : MonoBehaviour
{

    private CinemachineVirtualCamera vCam;

    // Start is called before the first frame update
    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        vCam.m_Follow = Move.alvo.transform;
        vCam.m_LookAt = Move.alvo.transform;
    }
}
