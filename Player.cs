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
    public Vector3 move, Distancia;
    public CharacterController controller;
    float RotacionInicial;
    public Transform player, Enemigo, Disparos;
    protected float distancia,distanciamaxima=0;
    public GameObject Enemys,m4,escopeta,pistola,substitutoEnemgio;

    public float  gravedad;
    public RagdollDeath ragdollEnemigo;
    public Animator anim;
    bool RangoEnemigo = false;

    public ParticleSystem disparoParticulas;

    public List<GameObject> Enemigos = new List<GameObject>();
    
    public List<float> distanciatest = new List<float>();
    public bool disparar = true,puededisparar=true;
    public bool escojerNuevoEnemigo = false;
    public bool apuntando = false;
    int moneda = 0;
    public int inventario,damageWeapon;
    public ArmasDisparar Armas;
    float distance, distancia2 = 100;
    public static Player p;
    public Text cantidadMonedas;
    // Start is called before the first frame update
    void Awake()
    {

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 160;
    }
    void Start()
    {
        p = this;
        Armas.Inicializar(out Armas.arma, 0, 10);//daño al iniciar el player
        anim = gameObject.GetComponent<Animator>();
        //ragdollEnemigo = GameObject.Find("Muñeco2").GetComponent<RagdollDeath>();
        RotacionInicial = 90f;

        //Armas = GetComponent<ArmasDisparar>();

        SaveWeaponsPlayer saveWeapons = SaveManager.LoadWeaponsStats();
        inventario = saveWeapons.weapon;

    }

    // Update is called once per frame
    void Update()
    {
        //AtacarEnemigo();
        MovimientoJoystick();
        
        CambiarArma();
        

        PuedeDisparar();


        //RotarPlayer();
        
       
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

        move = Derecha * x  + Adelante * y ;//para que cambie la direcion con la camara
        move.y = -4f;
        controller.Move(move * 12 * Time.deltaTime);

        //Muñeco.transform.rotation = Quaternion.LookRotation(Vector3.forward, 2f);
        if (x != 0 && !apuntando || y != 0 && !apuntando)
        {
            if (!RangoEnemigo)
            {
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(new Vector3(move.x,0,move.z)), 0.1f);

            }


        }




    }
    




    

    void CambiarArma()
    {

        switch (inventario)
        {
            case 1://M4
                pistola.SetActive(false);
                escopeta.SetActive(false);
                m4.SetActive(true);
                damageWeapon = 25;
                Armas.Modificador(ref Armas.arma, 7, damageWeapon);


                var shape = disparoParticulas.shape;//Cambiar el angulo del cono para que las balas de escopeta salgan disparadas
                shape.angle = 0f;


                //https://docs.unity3d.com/ScriptReference/ParticleSystem-emission.html
                ParticleSystem.EmissionModule velocidadDisparo = disparoParticulas.emission; //ParticleSystem.EmissionModule provides access to your particle system emission module so that you can manage its properties.
                velocidadDisparo.enabled = true;

                velocidadDisparo.rateOverTime = Armas.arma.cadencia;
                velocidadDisparo.SetBursts(
                   new ParticleSystem.Burst[]{
                new ParticleSystem.Burst(1f, 0f),

                   });

                SaveManager.SaveWeaponsStats(this);

                break;
            case 2://Escopeta
                
                    m4.SetActive(false);
                    pistola.SetActive(false);
                    escopeta.SetActive(true);
                damageWeapon = 100;
                Armas.Modificador(ref Armas.arma, 0, damageWeapon);

                    var shape1 = disparoParticulas.shape;//Cambiar el angulo del cono para que las balas de escopeta salgan disparadas
                    shape1.angle = 6f;


                    //https://docs.unity3d.com/ScriptReference/ParticleSystem-emission.html
                    ParticleSystem.EmissionModule velocidadDisparo1 = disparoParticulas.emission; //ParticleSystem.EmissionModule provides access to your particle system emission module so that you can manage its properties.
                velocidadDisparo1.enabled = true;

                velocidadDisparo1.rateOverTime = Armas.arma.cadencia;
                velocidadDisparo1.SetBursts(
                        new ParticleSystem.Burst[]{
                new ParticleSystem.Burst(1f, 7f),

                        });
                SaveManager.SaveWeaponsStats(this);
                break;
            case 3://Pistola
               
                    pistola.SetActive(true);
                    escopeta.SetActive(false);
                    m4.SetActive(false);
                damageWeapon = 10;
                Armas.Modificador(ref Armas.arma, 4, damageWeapon);

                    var shape2 = disparoParticulas.shape;//Cambiar el angulo del cono para que las balas de escopeta salgan disparadas
                    shape2.angle = 0f;


                    //https://docs.unity3d.com/ScriptReference/ParticleSystem-emission.html
                    ParticleSystem.EmissionModule velocidadDisparo2 = disparoParticulas.emission; //ParticleSystem.EmissionModule provides access to your particle system emission module so that you can manage its properties.
                velocidadDisparo2.enabled = true;

                velocidadDisparo2.rateOverTime = Armas.arma.cadencia;
                velocidadDisparo2.SetBursts(
                       new ParticleSystem.Burst[]{
                new ParticleSystem.Burst(1f, 0f),

                       });
                SaveManager.SaveWeaponsStats(this);


                break;
            default:
                break;
        }
      





        
        





        
    
       

        





    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Enemigo")
        {
            Enemigos.Add(other.gameObject);
        }
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Moneda")
        {
            moneda++;
            Destroy(collision.gameObject);
            cantidadMonedas.text = moneda.ToString();
        }
    }


    void OnTriggerExit(Collider collision)//Este trigger exit sirve para que una vez se vaya del rango del enemigo al que tiene selecionado pueda volver a selecionar otro que este mas cerca
    {
        Enemigos.Remove(collision.gameObject);
        if (collision.gameObject.tag == "Enemigo" )
        {
           //disparoParticulas.Stop();//deja de disaprar una vez se va del rango del enemigo
            Enemigo = collision.gameObject.transform;
            Enemigo.name = "Cube(Clone)";//cambiamos el nombre para que pueda escojer otro enemigo objetivo 
            escojerNuevoEnemigo = false;//dejamos que pueda acceder al if para selecionar a otro enemigo objetivo que si este dentro del collaider
            apuntando = false;
            //disparoParticulas.gameObject.SetActive(false);
            





        }



    }


    void OnTriggerStay(Collider collision)
    {


        //YA no se ni que hace esto no lo toques es algo de mirar o no al enemigo y que si no gire hacia donde vas help
        if (collision.gameObject.tag == "Enemigo")//Al començar y que se actibe el triger con el primer enemigo este se cambiara el nombre a Enemigo y no volvera a acceder hasta que el bool cambie, en este caso cuando el enemigo muera o salgamos del rango(colaider de nuestro ()er)
        {

            Enemigo = collision.gameObject.transform;//El enemigo es el primero que a accedido al collider de nuestro Player
            Enemigo.name = "Enemigo";//cambiamos el nombre a Enemigo
            //escojerNuevoEnemigo = true;//cambiamos el valor a true para que no vuelva a entrar en el if y no selecione mas de un enemigo a la vez porque si no los calulos fallan y apunta donde le da la gana
           
            apuntando = true;
            disparoParticulas.gameObject.SetActive(true);
        }

        

        //MOVIMIENTO PARA SEGUIR Y MIRAR
        if (collision.gameObject.tag == "Enemigo")//si tenemos un gameobect llamado enemigo accedera y en este if es donde apunta al enemigo y dispara
        {
            disparoParticulas.gameObject.SetActive(true);
            //Enemigo = collision.gameObject.transform;//
            Enemigo = CalcularEnemigoMasCercano();

            Disparos.LookAt(Enemigo);
            //Arma.eulerAngles = new Vector3(0f, Muñeco.eulerAngles.y, Muñeco.eulerAngles.z);//Indicamos que la rotacion de el muñeco sera la misma actual menos la x y la z que se la ponemos en 0 para que no rotea la altura del enemigo.
            RangoEnemigo = true;//Con este true hacemos que deje de mirar hacia donde se mueve y rote hacia la direcion del enemigo.

            Vector3 PosiciondeEnemigo = Enemigo.position - player.transform.position;//Comprobamos la direcion en la cual el muñeco tiene al enemigo
          
            Quaternion rotation = Quaternion.LookRotation(PosiciondeEnemigo, Vector3.up);//Señalamos que mire en la rotacion de la poscion enemigo y adelante osea Vector.up
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rotation, 0.2f);//Hacemos el Slerp para que no rote instantaniamente
            player.eulerAngles = new Vector3(0f, player.eulerAngles.y, 0f);//Indicamos que la rotacion de el muñeco sera la misma actual menos la x y la z que se la ponemos en 0 para que no rotea la altura del enemigo.
            CambiarArma();
            



        }
        else
        {
            RangoEnemigo = false;


        }
    }
    void PuedeDisparar()//Para que el particle pueda hacer el play y el stop ya que si en el update ponemos el play se bugea y no funciona
    {
        if (Enemigos.Count == 0)//SI en la array no tenemos enemigos significa que no estara dispararndo a nadie entonces stop y descativamos el gameobject
        {
            disparoParticulas.gameObject.SetActive(false);
            disparoParticulas.Stop();
            puededisparar = true;//para que cuando haya algun enemigo pueda volver a disparar
        }
        else//Si no significa que hay almenos 1 enemigo entoces disparara activamos el gameobject y luego que se active una sola vez el play
        {
            disparoParticulas.gameObject.SetActive(true);
            if (puededisparar)//que solo se active una vez mientras haya enemigos 
            {
                disparoParticulas.Play();
                puededisparar = false;//para que entre 1 sola vez

            }
        }
    }
    void GenerarEnemigos(int numeroEnemigos)
    {

       


    }
    Transform CalcularEnemigoMasCercano()
    {
        distancia2 = 100;


        for (int i =0; i < Enemigos.Count; i++)
        {


            distance = Vector3.Distance(player.position, Enemigos[i].transform.position);
                if (distance < distancia2)
                {

                   Enemigo = Enemigos[i].transform;
                distancia2 = distance;
            }
                
                
            }
        return Enemigo;
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

        velocidadDisparo.SetBursts(
                new ParticleSystem.Burst[]{
                new ParticleSystem.Burst(1f, 0f),

                });
    }*/

