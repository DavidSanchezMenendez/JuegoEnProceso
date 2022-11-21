using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SistemadeSpawnTorretas : MonoBehaviour
{
    public static SistemadeSpawnTorretas ssT;
    public GameObject spawnTowerUI,torreta,spawnerposition,quetorretaes,torretaspawneada;
    public List<GameObject> torretas = new List<GameObject>();
    
    public Enemigo enemigo;
    
    public  List<ArmasDisparar> arma = new List<ArmasDisparar>();
   
    public int a =0;
    double e;

    public bool guardar = true;
    public int x = 0, y = 1, z = 2;

    // Start is called before the first frame update
    void Start()
    {
        ssT = this;
        if (guardar)
        {
            SaveDataTorretas torretaData = SaveManager.LoadTorretasData();

           

            for (int i = 0; i < torretaData.numeroTorretas; i++)
            {
               
                
                for (int e = 0; e < 1; e++)
                {
                    a++;
                    torreta = Instantiate(torreta, new Vector3(torretaData.posicionTorreta1[x], torretaData.posicionTorreta1[y], torretaData.posicionTorreta1[z]), Quaternion.identity);
                    spawnTowerUI.gameObject.SetActive(false);
                   
                  
                    torreta.name = "Torreta " + a;

                    enemigo.torreta2.Add(GameObject.Find("Torreta " + a).GetComponent<Torreta2>());

                    torretas.Add(torreta);
                    arma.Add(GameObject.Find("Torreta " + a).GetComponent<Torreta2>().armas);//iniciamos un array para que cada torreta tenga un daño indeopenidente a las demas
                 
                    x +=3;
                    y+=3;
                    z+=3;
                    

                }
                
            }
            SaveManager.SaveDataTorreta(this);


        }
        else
        {
            SaveManager.SaveDataTorreta(this);
        }
       

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Torreta")
        {
          
            spawnTowerUI.gameObject.SetActive(true);


        }

        spawnerposition = collision.gameObject;
        
      
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "TorretaSpawneada")
        {
            torreta = other.gameObject;
           


        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Torreta")
        {
            spawnTowerUI.gameObject.SetActive(false);

        }
    }


        public void SpawnearTorreta()
    {
        a++;
        //spawnerposition.gameObject.SetActive(false);
        //spawnerposition.tag = "Untagged";
        spawnTowerUI.gameObject.SetActive(false);
       
       // spawnTowerUI.gameObject.SetActive(false);
        torreta = Instantiate(torreta,new Vector3(spawnerposition.transform.position.x, spawnerposition.transform.position.y+2.6f, spawnerposition.transform.position.z), Quaternion.identity);
        
        torreta.name = "Torreta "+a;
       

        enemigo.torreta2.Add(GameObject.Find("Torreta " + a).GetComponent<Torreta2>());

        torretas.Add(torreta);

        GameObject.Find("Torreta " + a).GetComponent<Torreta2>().damage = 10;
       
        arma.Add(GameObject.Find("Torreta " + a).GetComponent<Torreta2>().armas);//iniciamos un array para que cada torreta tenga un daño indeopenidente a las demas
        
        SaveManager.SaveDataTorreta(this);
        //arma[a].Inicializar(out arma[a].arma, 0, 100);
        //PlayerVida.pV.GuardarPosicionPlayer();
        




       





    }


    
  
}
