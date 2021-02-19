using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;

public class PERSONAGEM : MonoBehaviour
{

    #region Variaveis
  
	public Vector3 dirMoveDesejada;
	public float velRotDesejada = 5.5f;
	public Animator anim;
	public float Speed;

	public Camera cam;

    private CharacterController heroiCh;
    
    public CinemachineFreeLook vcam;

    #region Floats
    /* Floats */
    [SerializeField]
    private float gravidade = 9f;
    [SerializeField]
    private float puloForca;
    [SerializeField]
    private float dist;
    /* ****** */
    #endregion

    #region Booleans
    // Booleans
    [SerializeField]
    private bool armado;
    private bool isModoAtir = false;
    #endregion

    // PhotonView
    public PhotonView pv;
    public static Transform alvo;

    #endregion

    private Camera camC;
    private CinemachineFreeLook cmvC;

    private float limite;

    void Awake()
    {
        pv = GetComponent<PhotonView>();

        if (pv.IsMine)
        {
            camC = Instantiate(cam);
            cmvC = Instantiate(vcam);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator> ();
      
        heroiCh = GetComponent<CharacterController>();
        armado = false;

        if (pv.IsMine)
        {
            alvo = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Movimento sem Arma
        MovimentoComOuSemArma();
        //Pulo
        Pulo();
        //No Ar
        Queda();
        //Gravidade
        Gravidade();

        if(Input.GetKeyDown(KeyCode.Q) && !armado)
        {
            armado = true;
            anim.SetLayerWeight(1, 1);
            anim.SetTrigger("arma");

        }

        if (Input.GetKeyDown(KeyCode.E) && armado && !isModoAtir)
        {
            armado = false;
            anim.SetTrigger("desarma");
        }

        if (armado)
        {
            isModoAtir = true;
            // Preparando para atirar
            if (Input.GetMouseButton(1))
            {
                if(limite < 1)
                {
                    limite += Time.deltaTime * 5;
                }

                anim.SetLayerWeight(2, limite);
            }
            else
            {
                isModoAtir = false;
                if(limite > 0)
                {
                    limite -= Time.deltaTime * 5;
                }
                anim.SetLayerWeight(2, limite);
            }
        }
        
    }

    public void CalculaPesoArmado()
    {
        anim.SetLayerWeight(1, 0);
    }

    void MovimentoComOuSemArma()
    {
        
        MovimentoSimples();
        anim.SetFloat("Movimento", dirMoveDesejada.magnitude, 0.1f, Time.deltaTime);

    }



    void DirecaoMoveFree(Vector3 mov)
    {
        Quaternion rot;

        if(mov.magnitude > 0 )
        {
            Quaternion novaDir = Quaternion.LookRotation(mov);
            rot = Quaternion.Slerp(transform.rotation,novaDir,Time.deltaTime * velRotDesejada);
            transform.rotation = new Quaternion(0,rot.y,0,rot.w);
        }
    }

    void DirecaoMoveArmado()
    {
        transform.eulerAngles = new Vector3(0, vcam.m_XAxis.Value, 0);
    }

    void MovimentoSimples()
    {
        if(heroiCh.isGrounded)
        {
            var hori = Input.GetAxis("Horizontal");
            var vert = Input.GetAxis("Vertical");               

            Vector3 frente = camC.transform.forward;
            Vector3 direita = camC.transform.right;

            frente.Normalize ();
            direita.Normalize ();

            dirMoveDesejada = frente * vert + direita * hori;
            dirMoveDesejada *= Speed;

            if (!isModoAtir)
            {
                DirecaoMoveFree(dirMoveDesejada);
            }
            else
            {
                anim.SetFloat("x", hori, 0.01f, Time.deltaTime);
                anim.SetFloat("y", vert, 0.01f, Time.deltaTime);

                DirecaoMoveArmado();
            }

        }
        else
        {
            anim.SetFloat("Movimento",0, 1f, Time.deltaTime * 10);  
        }

        
    }

    void Queda()
    {
        if(!heroiCh.isGrounded)
        {

            RaycastHit hit;
            if(!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, dist))
            {
                if (armado)
                {
                    anim.SetBool("NoAr_armado", true);
                }
                else
                {
                    anim.SetBool("NoAr", true);
                }
            }
            
        }
        else if(heroiCh.isGrounded)
        {
            if(armado)
            {
                anim.SetBool("NoAr_armado",false); 
            }
            else
            {
                anim.SetBool("NoAr",false); 
            }
            
        }

    }

    void Gravidade()
    {
            dirMoveDesejada.y -= gravidade * Time.deltaTime;
            heroiCh.Move(dirMoveDesejada * Time.deltaTime);    
    }


    void Pulo()
    {
        if (dirMoveDesejada.x != 0 || dirMoveDesejada.z != 0)
        {
            if (heroiCh.isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {

                    if (!armado)
                    {
                        dirMoveDesejada.y = puloForca;
                        anim.SetTrigger("Pulo");
                    }
                }
            }
        }
        else if(dirMoveDesejada.magnitude == 0)
        {
            if (heroiCh.isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    
                    if (armado)
                    {
                        dirMoveDesejada.y = puloForca;
                        anim.SetTrigger("PuloParadoArmo");
                    }
                }
            }
        }
    }


}
