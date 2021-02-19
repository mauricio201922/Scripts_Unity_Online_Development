using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VCamConf : MonoBehaviour
{

    private CinemachineFreeLook vCam;

    // Start is called before the first frame update
    void Start()
    {
        vCam = GetComponent<CinemachineFreeLook>();
        vCam.m_Follow = PERSONAGEM.alvo.transform;
        vCam.m_LookAt = PERSONAGEM.alvo.transform.GetChild(2);
        //vCam.m_LookAt = Move.alvo.transform;
    }
}
