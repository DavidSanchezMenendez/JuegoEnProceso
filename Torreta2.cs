using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Torreta2 : MonoBehaviour
{
    public static Torreta2 t2;
    SpawnEnemigos arrayEnemigos;
    public float x, y;
    public Joystick joystiackMove;
    public Camera cam;
    public Vector3 move, Distancia;
    public CharacterController controller;
    float RotacionInicial;
    public Transform player, EnemigoTransform, Disparos;
    protected float distancia, distanciamaxima = 0;
    public GameObject Enemys, spawnTowerUI, updateUI;

    public float gravedad;
    public RagdollDeath ragdollEnemigo;

    bool RangoEnemigo = false;

    public ParticleSystem disparoParticulas;

    public List<GameObject> Enemigos = new List<GameObject>();

    public string nombreTorreta;
    public bool disparar = true, puededisparar = true, dañoarma = true,oneTime=true;

    public ArmasDisparar armas;

    public int damage = 10;

    public List<int> damage1 = new List<int>();
    public List<Torreta2> torretasLista = new List<Torreta2>();
    public Text textDamage;

    float distance, distancia2 = 100;
    // Start is called before the first frame update
    void Awake()
    {

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 160;
    }
    void Start()
    {
        armas.Inicializar(out armas.arma, 0, 0);//daño al empezar la torreta

        RotacionInicial = 90f;

        //Armas = GetComponent<ArmasDisparar>();
        SaveDamage damageTorreta = SaveManager.LoadTorretasDamageData();


        armas.Modificador(ref armas.arma, 0, damageTorreta.damage);//daño al empezar la torreta
        
      

        

        torretasLista = GameObject.Find("Enemigo").GetComponent<Enemigo>().torreta2;
        nombreTorreta = this.GetComponent<Transform>().name;

        //Fumada durisima
        for (int i = 0; i <= GameObject.Find("Player").GetComponent<SistemadeSpawnTorretas>().a; i++)
        {
            damage1.Add(GameObject.Find("Torreta " + i).GetComponent<Torreta2>().damage);

        }

        for (int i = 0; i <= GameObject.Find("Player").GetComponent<SistemadeSpawnTorretas>().a; i++)
        {
           
          
           
            

            if (nombreTorreta == "Torreta " + i )
            {
                
                damage1 = damageTorreta.damageList;
                damage1[i] = damageTorreta.damageList[i];
                damage = damageTorreta.damageList[i];
                textDamage.text = damage1[i].ToString();
                //SaveManager.SaveDamageTorreta(this);
                armas.Inicializar(out armas.arma, 0, damage);
            }




          //damageTorreta.damageList = null;
           SaveManager.SaveDamageTorreta(this);




        }



    }

    // Update is called once per frame
    void Update()
    {
        //AtacarEnemigo();





        PuedeDisparar();


        //RotarPlayer();


    }



    public void Daño(int daño)
    {
        //armas.Inicializar(out armas.arma, 0, 50);
    }






    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            updateUI.gameObject.SetActive(false);
        }
    }






    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            updateUI.gameObject.SetActive(true);
        }
        if (collision.gameObject.tag == "Torreta")
        {
            collision.gameObject.SetActive(false);
            SistemadeSpawnTorretas.ssT.spawnTowerUI.SetActive(false);//CUando destruimos el spawner de torreta no hace el OncollisionExit entonces no se desactiva la UI, asi que justo cuando lo desactivamos descativamos la UI.
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Enemigo")
        {
            Enemigos.Add(other.gameObject);
        }




    }


    void OnTriggerExit(Collider other)//Este trigger exit sirve para que una vez se vaya del rango del enemigo al que tiene selecionado pueda volver a selecionar otro que este mas cerca
    {
        Enemigos.Remove(other.gameObject);
        if (other.gameObject.name == "Enemigo")
        {
            //disparoParticulas.Stop();//deja de disaprar una vez se va del rango del enemigo
            EnemigoTransform = other.gameObject.transform;
            EnemigoTransform.name = "Cube(Clone)";//cambiamos el nombre para que pueda escojer otro enemigo objetivo 

            //disparoParticulas.gameObject.SetActive(false);






        }




    }


    void OnTriggerStay(Collider collision)
    {


        //YA no se ni que hace esto no lo toques es algo de mirar o no al enemigo y que si no gire hacia donde vas help
        if (collision.gameObject.tag == "Enemigo")//Al començar y que se actibe el triger con el primer enemigo este se cambiara el nombre a Enemigo y no volvera a acceder hasta que el bool cambie, en este caso cuando el enemigo muera o salgamos del rango(colaider de nuestro player)
        {

            EnemigoTransform = collision.gameObject.transform;//El enemigo es el primero que a accedido al collider de nuestro Player
            EnemigoTransform.name = "Enemigo";//cambiamos el nombre a Enemigo
                                     //escojerNuevoEnemigo = true;//cambiamos el valor a true para que no vuelva a entrar en el if y no selecione mas de un enemigo a la vez porque si no los calulos fallan y apunta donde le da la gana


            disparoParticulas.gameObject.SetActive(true);
        }



        //MOVIMIENTO PARA SEGUIR Y MIRAR
        if (collision.gameObject.tag == "Enemigo")//si tenemos un gameobect llamado enemigo accedera y en este if es donde apunta al enemigo y dispara
        {
            disparoParticulas.gameObject.SetActive(true);

            // Enemigo = CalcularEnemigoMasCercano();
            EnemigoTransform = Enemigos[0].transform;
            Disparos.LookAt(EnemigoTransform);
            //Arma.eulerAngles = new Vector3(0f, Muñeco.eulerAngles.y, Muñeco.eulerAngles.z);//Indicamos que la rotacion de el muñeco sera la misma actual menos la x y la z que se la ponemos en 0 para que no rotea la altura del enemigo.
            RangoEnemigo = true;//Con este true hacemos que deje de mirar hacia donde se mueve y rote hacia la direcion del enemigo.

            Vector3 PosiciondeEnemigo = EnemigoTransform.position - player.transform.position;//Comprobamos la direcion en la cual el muñeco tiene al enemigo

            Quaternion rotation = Quaternion.LookRotation(PosiciondeEnemigo, Vector3.up);//Señalamos que mire en la rotacion de la poscion enemigo y adelante osea Vector.up
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rotation, 0.1f);//Hacemos el Slerp para que no rote instantaniamente
            player.eulerAngles = new Vector3(-90f, player.eulerAngles.y, 0f);//Indicamos que la rotacion de el muñeco sera la misma actual menos la x y la z que se la ponemos en 0 para que no rotea la altura del enemigo.





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

    Transform CalcularEnemigoMasCercano()
    {
        distancia2 = 100;


        for (int i = 0; i < Enemigos.Count; i++)
        {


            distance = Vector3.Distance(player.position, Enemigos[i].transform.position);
            if (distance < distancia2)
            {

                EnemigoTransform = Enemigos[i].transform;
                distancia2 = distance;
            }


        }
        return EnemigoTransform;
    }


    public void UpgradeTorreta()
    {
        
        int contador = 0;
        damage += 30;
        torretasLista = GameObject.Find("Enemigo").GetComponent<Enemigo>().torreta2;

        nombreTorreta = this.GetComponent<Transform>().name;
      
        //Fumada durisima

        if (damage1.Count <= GameObject.Find("Player").GetComponent<SistemadeSpawnTorretas>().a)
        {
            damage1.Add(damage);
        }
        
        for (int i = 0; i <= GameObject.Find("Player").GetComponent<SistemadeSpawnTorretas>().a; i++)
        {
            
               
            
            


            if (nombreTorreta == "Torreta "+i)
            {
                
                damage1[i] = damage;
                textDamage.text = damage1[i].ToString();
               

                armas.Inicializar(out armas.arma, 0,damage );
            }
               
               
         
            torretasLista[i].damage1 = damage1;
            torretasLista[i].damage1[i] = torretasLista[i].damage;
           












        }
        oneTime = false;

        armas.Inicializar(out armas.arma, 0, damage);
       
        SaveManager.SaveDamageTorreta(this);
        PlayerVida.pV.GuardarPosicionPlayer();


    }
}

/*
void OnTriggerExit(Collider collision)//Este trigger exit sirve para que una vez se vaya del rango del enemigo al que tiene selecionado pueda volver a selecionar otro que este mas cerca
{

    if (collision.gameObject.name == "Enemigo")
    {
        disparoParticulas.Stop();//deja de disaprar una vez se va del rango del enemigo
        Enemigo = collision.gameObject.transform;
        Enemigo.name = "Cube(Clone)";//cambiamos el nombre para que pueda escojer otro enemigo objetivo 
        escojerNuevoEnemigo = false;//dejamos que pueda acceder al if para selecionar a otro enemigo objetivo que si este dentro del collaider

        disparoParticulas.gameObject.SetActive(false);





    }



}


void OnTriggerStay(Collider collision)//antiguo Metodo
{


    ///SELECIONAR NUEVO OBJETIVO ENEMIGO PARA DISPARAR
    if (!escojerNuevoEnemigo && collision.transform.name != "Player")//Al començar y que se actibe el triger con el primer enemigo este se cambiara el nombre a Enemigo y no volvera a acceder hasta que el bool cambie, en este caso cuando el enemigo muera o salgamos del rango(colaider de nuestro player)
    {

        Enemigo = collision.gameObject.transform;//El enemigo es el primero que a accedido al collider de nuestro Player
        Enemigo.name = "EnemigoTorreta";//cambiamos el nombre a Enemigo
        escojerNuevoEnemigo = true;//cambiamos el valor a true para que no vuelva a entrar en el if y no selecione mas de un enemigo a la vez porque si no los calulos fallan y apunta donde le da la gana
        disparar = true;//empieza a disparar nuetras particulas de disparo

        disparoParticulas.gameObject.SetActive(true);
    }


    //MOVIMIENTO PARA SEGUIR Y MIRAR
    if (collision.gameObject.name == "EnemigoTorreta")//si tenemos un gameobect llamado enemigo accedera y en este if es donde apunta al enemigo y dispara
    {
        Enemigo = collision.gameObject.transform;//


        Disparos.LookAt(Enemigo);
        //Arma.eulerAngles = new Vector3(0f, Muñeco.eulerAngles.y, Muñeco.eulerAngles.z);//Indicamos que la rotacion de el muñeco sera la misma actual menos la x y la z que se la ponemos en 0 para que no rotea la altura del enemigo.
        RangoEnemigo = true;//Con este true hacemos que deje de mirar hacia donde se mueve y rote hacia la direcion del enemigo.

        Vector3 PosiciondeEnemigo = Enemigo.position - player.transform.position;//Comprobamos la direcion en la cual el muñeco tiene al enemigo
        Quaternion rotation = Quaternion.LookRotation(PosiciondeEnemigo, Vector3.up);//Señalamos que mire en la rotacion de la poscion enemigo y adelante osea Vector.up
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rotation, 0.5f);//Hacemos el Slerp para que no rote instantaniamente
        player.eulerAngles = new Vector3(0f, player.eulerAngles.y, 0f);//Indicamos que la rotacion de el muñeco sera la misma actual menos la x y la z que se la ponemos en 0 para que no rotea la altura del enemigo.
        player.eulerAngles = new Vector3(-90f, player.eulerAngles.y, 0f);//Indicamos que la rotacion de el muñeco sera la misma actual menos la x y la z que se la ponemos en 0 para que no rotea la altura del enemigo.
        if (disparar)//Para que una vez tenga enemigo objetivo disapre, solo se accede una vez para que el Play no se repita 60 veces y se bugea.
        {
            disparoParticulas.Play();
            disparar = false;//Para que entre una sola vez, vuelve en true cuando cambia o encuentra un onjetios para que pueda volver a disparar
        }



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
}*/