using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnEnemigos : MonoBehaviour
{

    public List<Transform> spawners = new List<Transform>();
    public GameObject enemigos;
    public int numeroEnemigos = 1;



    float tiempo, tiempoespera = 5f;
   

    private void Start()
    {
        
        
    }
    private void Update()
    {
        if (Time.time > tiempo )
        {
            tiempo = Time.time+ tiempoespera;
            Debug.Log("a");
            //GenerarEnemigos();
        }
        
    }
    void GenerarEnemigos() 
    {
        for (int i = 0; i <= spawners.Count-1; i++)
        {
            
            for (int e = 0; e < numeroEnemigos; e++)
            {
                Instantiate(enemigos, spawners[i].position, Quaternion.identity);
            }
        }
    }
   public void GenerarEnemigos2()
    {
        for (int i = 0; i <= spawners.Count - 1; i++)
        {

            for (int e = 0; e < numeroEnemigos; e++)
            {
                Instantiate(enemigos, spawners[i].position, Quaternion.identity);
            }
        }
    }

}
