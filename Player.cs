using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    SpawnEnemigos arrayEnemigos;
    public float x, y;
    public Joystick joystiackMove;
    public Camera cam;
    public Vector3 move,Distancia;
    public CharacterController controller;
    float RotacionInicial;
    public Transform Muñeco,Enemigo,Disparos;
    protected float distancia;
    public GameObject Enemys;

    float VelocidadY, gravedad;
    public RagdollDeath ragdollEnemigo;
    public Animator anim;
    bool RangoEnemigo=false;
    
    public ParticleSystem disparoParticulas;

    public List<GameObject> Enemigos = new List<GameObject>();
    public List<float> distanciatest = new List<float>();
    public bool disparar = false;
   public bool escojerNuevoEnemigo = false;
    public bool apuntando = false;

    ArmasDisparar Armas;

    // Start is called before the first frame update
    void Awake()
    {

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 160;
    }
    void Start()
    {
        
        anim = gameObject.GetComponent<Animator>();
        //ragdollEnemigo = GameObject.Find("Muñeco2").GetComponent<RagdollDeath>();
        RotacionInicial = 90f;
        
        Armas = GetComponent<ArmasDisparar>();

        GenerarEnemigos(20);
        

    }

    // Update is called once per frame
    void Update()
    {
        //AtacarEnemigo();
        MovimientoJoystick();
        Gravedad();

        
        
        


        //RotarPlayer();
        controller.Move(move * 12 * Time.deltaTime);
    }

    public void MovimientoJoystick()
    {

        x = joystiackMove.Horizontal + Input.GetAxis("Horizontal");
        y = joystiackMove.Vertical + Input.GetAxis("Vertical");
        anim.SetFloat("X", x);
        anim.SetFloat("Z", y);
        if (x == 0 && y == 0) // si el personaje esta quieto activamos a animacion idle
        {

            anim.SetBool("Quieto", true);
        }
        else
        {
            anim.SetBool("Quieto", false);
        }

        // Asignar el vector que debe moverse con los joystiks
        //Vector3 MoveVector = (Vector3.up * joystiackMove.Horizontal + Vector3.left * joystiackMove.Vertical);
        Vector3 Adelante = cam.transform.forward;
        Adelante.y = 0;
        Adelante.Normalize();

        Vector3 Derecha = cam.transform.right;
        Derecha.y = 0;
        Adelante.Normalize();

        move = Derecha * x * 2 + Adelante * y * 2;//para que cambie la direcion con la camara

       
        //Muñeco.transform.rotation = Quaternion.LookRotation(Vector3.forward, 2f);
        if (x != 0 && !apuntando|| y != 0 && !apuntando)
        {
            if (!RangoEnemigo)
            {
                Muñeco.transform.rotation = Quaternion.Slerp(Muñeco.transform.rotation, Quaternion.LookRotation(move), 0.1f);
                
            }
            

        }



       
    }
    void AtacarEnemigo()
    {


        distancia = Vector3.Distance(Enemigo.position, Muñeco.position);
                

                if (distancia < 25)
                {
                    

                    Disparos.LookAt(Enemigo);
                    //Arma.eulerAngles = new Vector3(0f, Muñeco.eulerAngles.y, Muñeco.eulerAngles.z);//Indicamos que la rotacion de el muñeco sera la misma actual menos la x y la z que se la ponemos en 0 para que no rotea la altura del enemigo.
                    RangoEnemigo = true;//Con este true hacemos que deje de mirar hacia donde se mueve y rote hacia la direcion del enemigo.

                    Vector3 PosiciondeEnemigo = Enemigo.position - Muñeco.transform.position;//Comprobamos la direcion en la cual el muñeco tiene al enemigo
                    Quaternion rotation = Quaternion.LookRotation(PosiciondeEnemigo, Vector3.up);//Señalamos que mire en la rotacion de la poscion enemigo y adelante osea Vector.up
                    Muñeco.transform.rotation = Quaternion.Slerp(Muñeco.transform.rotation, rotation, 0.5f);//Hacemos el Slerp para que no rote instantaniamente
                    Muñeco.eulerAngles = new Vector3(0f, Muñeco.eulerAngles.y, 0f);//Indicamos que la rotacion de el muñeco sera la misma actual menos la x y la z que se la ponemos en 0 para que no rotea la altura del enemigo.
                    Disparar();

                }
                else
                {
                    RangoEnemigo = false;
                    disparoParticulas.Play();







                }
            
         
        }


    

    public void Gravedad()
    {
        if (controller.isGrounded)
        {
            VelocidadY = 0;



        }




        gravedad = -60;
        VelocidadY += gravedad * Time.deltaTime;


        move.y = VelocidadY;
        
    }

    void Disparar()
    {

        //https://docs.unity3d.com/ScriptReference/ParticleSystem-emission.html
        ParticleSystem.EmissionModule velocidadDisparo = disparoParticulas.emission; //ParticleSystem.EmissionModule provides access to your particle system emission module so that you can manage its properties.
        velocidadDisparo.enabled = true;

        velocidadDisparo.rateOverTime = 10.0f;

        velocidadDisparo.SetBursts(
            new ParticleSystem.Burst[]{
                new ParticleSystem.Burst(1f, 30),

            });


        //DAÑO Y COLICIÓN CON EL ENEMIGO

    }


  

    void OnTriggerExit(Collider collision)//Este trigger exit sirve para que una vez se vaya del rango del enemigo al que tiene selecionado pueda volver a selecionar otro que este mas cerca
    {

        if (collision.gameObject.name == "Enemigo")
        {
            disparoParticulas.Stop();//deja de disaprar una vez se va del rango del enemigo
            Enemigo = collision.gameObject.transform;
            Enemigo.name = "Cube(Clone)";//cambiamos el nombre para que pueda escojer otro enemigo objetivo 
            escojerNuevoEnemigo = false;//dejamos que pueda acceder al if para selecionar a otro enemigo objetivo que si este dentro del collaider
            apuntando = false;
        }

            
        
    }


    void OnTriggerStay(Collider collision)
    {
        //SELECIONAR NUEVO OBJETIVO ENEMIGO PARA DISPARAR
        if (!escojerNuevoEnemigo && collision.transform.name!="Player")//Al començar y que se actibe el triger con el primer enemigo este se cambiara el nombre a Enemigo y no volvera a acceder hasta que el bool cambie, en este caso cuando el enemigo muera o salgamos del rango(colaider de nuestro player)
        {
            Enemigo = collision.gameObject.transform;//El enemigo es el primero que a accedido al collider de nuestro Player
            Enemigo.name = "Enemigo";//cambiamos el nombre a Enemigo
            escojerNuevoEnemigo = true;//cambiamos el valor a true para que no vuelva a entrar en el if y no selecione mas de un enemigo a la vez porque si no los calulos fallan y apunta donde le da la gana
            disparar = true;//empieza a disparar nuetras particulas de disparo
            apuntando = true;
        }
       
        
        //MOVIMIENTO PARA SEGUIR Y MI
        if (collision.gameObject.name == "Enemigo" )//si tenemos un gameobect llamado enemigo accedera y en este if es donde apunta al enemigo y dispara
        { 
            //Enemigo = collision.gameObject.transform;//
            
            Disparos.LookAt(Enemigo);
            //Arma.eulerAngles = new Vector3(0f, Muñeco.eulerAngles.y, Muñeco.eulerAngles.z);//Indicamos que la rotacion de el muñeco sera la misma actual menos la x y la z que se la ponemos en 0 para que no rotea la altura del enemigo.
            RangoEnemigo = true;//Con este true hacemos que deje de mirar hacia donde se mueve y rote hacia la direcion del enemigo.
            
            Vector3 PosiciondeEnemigo = Enemigo.position - Muñeco.transform.position;//Comprobamos la direcion en la cual el muñeco tiene al enemigo
            Quaternion rotation = Quaternion.LookRotation(PosiciondeEnemigo, Vector3.up);//Señalamos que mire en la rotacion de la poscion enemigo y adelante osea Vector.up
            Muñeco.transform.rotation = Quaternion.Slerp(Muñeco.transform.rotation, rotation, 0.5f);//Hacemos el Slerp para que no rote instantaniamente
            Muñeco.eulerAngles = new Vector3(0f, Muñeco.eulerAngles.y, 0f);//Indicamos que la rotacion de el muñeco sera la misma actual menos la x y la z que se la ponemos en 0 para que no rotea la altura del enemigo.
            Disparar();
            if (disparar)//Para que una vez tenga enemigo objetivo disapre, solo se accede una vez para que el Play no se repita 60 veces y se bugea.
            {
                disparoParticulas.Play();
                disparar = false;//Para que entre una sola vez, vuelve en true cuando cambia o encuentra un onjetios para que pueda volver a disparar
            }
            Debug.Log(Enemigo.name + "Enemigo");

            
        }
        else
        {
            RangoEnemigo = false;
            
        }
    }

        void  GenerarEnemigos(int numeroEnemigos){
       
        for (int i = 0; i <= numeroEnemigos; i++)
        {
            Instantiate(Enemys);
        }
        
    }


}
/*void AtacarEnemigo()
    {
        distanciatest.Clear();
        Debug.Log(Enemigos.Count);

       
        
        for (int i = 0; i < Enemigos.Count; i++)
        {
            
            distanciatest.Add(Vector3.Distance(Muñeco.position, Enemigos[i].transform.position));
            

            for (int p = 0; p < distanciatest.Count; p++)
            {
                
                //distancia = Vector3.Distance(Muñeco.position, Enemigo.position);


                if (distanciatest[p] <= 25)
                {

                    Disparos.LookAt(Enemigo);
                    //Arma.eulerAngles = new Vector3(0f, Muñeco.eulerAngles.y, Muñeco.eulerAngles.z);//Indicamos que la rotacion de el muñeco sera la misma actual menos la x y la z que se la ponemos en 0 para que no rotea la altura del enemigo.
                    RangoEnemigo = true;//Con este true hacemos que deje de mirar hacia donde se mueve y rote hacia la direcion del enemigo.

                    Vector3 PosiciondeEnemigo = Enemigo.position - Muñeco.transform.position;//Comprobamos la direcion en la cual el muñeco tiene al enemigo
                    Quaternion rotation = Quaternion.LookRotation(PosiciondeEnemigo, Vector3.up);//Señalamos que mire en la rotacion de la poscion enemigo y adelante osea Vector.up
                    Muñeco.transform.rotation = Quaternion.Slerp(Muñeco.transform.rotation, rotation, 0.5f);//Hacemos el Slerp para que no rote instantaniamente
                    Muñeco.eulerAngles = new Vector3(0f, Muñeco.eulerAngles.y, 0f);//Indicamos que la rotacion de el muñeco sera la misma actual menos la x y la z que se la ponemos en 0 para que no rotea la altura del enemigo.
                    Disparar();

                }
                else
                {
                    RangoEnemigo = false;
                    disparoParticulas.Play();







                }
            }
           Debug.Log(distanciatest.Count);
        }

        
    }*/

