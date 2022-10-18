using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulasDisparo : MonoBehaviour
{
   
    // Start is called before the first frame update
    public ParticleSystem particulasDisparo;
    List<ParticleCollisionEvent> colisionBalas = new List<ParticleCollisionEvent>();
    public GameObject EnemigoGolpeado;
    public Enemigo enemigo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.name.Equals("Enemigo") || other.gameObject.name.Equals("Cube(Clone)"))
        {
            //int hits = particulasDisparo.GetCollisionEvents(other, colisionBalas);
            Debug.Log("hit");
            enemigo = other.gameObject.GetComponent<Enemigo>();//Coje el script de cada enemigo que golpe entonces si golepa a 1 ese 1 recibira su daño a parte si golpea a enemigo 3 este recibira el daño
                                                               //Debug.Log(other.gameObject.name);
            enemigo.RecibirDisparo(33);
        }
        


        
        
        
        
       
    }
}
