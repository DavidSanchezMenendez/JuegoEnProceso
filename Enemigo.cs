using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemigo : MonoBehaviour
{
    public GameObject moneda;
    public static Enemigo e;
    public Collider mainColaider;
    public SistemaXP XP;
    public Camera cam;
    public PlayerVida vidaPlayer;
    public Player player;
    public Torreta torreta;
    public List<Torreta2> torreta2 = new List<Torreta2>();
    public GameObject detectarPlayer,ragdoll;
    public NavMeshAgent navMeshAgent;
    public CharacterController enemigo;
    public Image barraDeVida,imagenVidaDireccion;
    
    public Material dissolve;


    public float vida = 100, vidaActual,floatvida;
    RaycastHit hit;

    
    // Start is called before the first frame up    date
    void Start()
    {
        vidaActual = vida;
        navMeshAgent = GetComponent<NavMeshAgent>();

        enemigo = GetComponent<CharacterController>();
       
   
    }

    // Update is called once per frame
    void Update()
    {

        if (vida<=0)
        {
            dissolve.SetFloat("Vector1_E088A191", 0.66f);
        }
       
        imagenVidaDireccion.transform.LookAt(cam.transform);//para que la barra de vida siga a la camara y se vea bien aunque roten
        
       


        navMeshAgent.destination = player.transform.position;
        


        }
    public void RecibirDisparo(int damage,Transform procedenciaDisparo)
    {
        vidaActual -= damage;
        Vector3 HookShootDirection = (procedenciaDisparo.transform.position - transform.position).normalized;//la direccion a la que tiene que ir
       
        Vector3 inersia = -HookShootDirection *2 ;
        inersia.y = 0;

        enemigo.Move(inersia);

        imagenVidaDireccion.gameObject.SetActive(true);
        barraDeVida.fillAmount = vidaActual / vida;
        floatvida = vidaActual / vida;
        if (floatvida == 1)
        {
            barraDeVida.color = Color.green;
        }
        else if (floatvida < 0.75f && floatvida > 0.5f)
        {
            barraDeVida.color = Color.yellow;
        }
        else if (floatvida < 0.5f)
        {
            barraDeVida.color = Color.red;
        }


        if (vidaActual<=0)
        {
            
            XP.GetEXP();
            torreta.Enemigos.Remove(this.gameObject);
            for (int i = 0; i < torreta2.Count; i++)
            {
                torreta2[i].Enemigos.Remove(this.gameObject);
            }
            
            player.Enemigos.Remove(this.gameObject);
           
           



            player.escojerNuevoEnemigo = false;//cuando un enemigo muere cambia el bool de Player para que pueda acceder a buscar un nuevo enemigo, sin esto se quedaría mirando a un enemigo ya meurto
            
           
            player.apuntando = false;
           

            gameObject.SetActive(false);
            Destroy(this.gameObject);
            for (int i = 0; i < 5; i++)
            {
               Instantiate(moneda, transform.position, Quaternion.identity);
            }
            
            




        }


    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.transform.tag == "PlayerHit")
        {
            vidaPlayer.Golpeado(1f);
            Debug.Log("hola");
        }
    }

   
}

