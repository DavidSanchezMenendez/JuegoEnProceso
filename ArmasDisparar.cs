using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmasDisparar : MonoBehaviour
{
    // Start is called before the first frame update
    public Enemigo enemigo;

    public struct Arma
    {
        public int cadencia;
        public int damage;
    }
    public Arma arma;
    void Start()
    {
        
    }
    private void Awake()
    {
       
        //Inicializar(out arma, 0, 1);
      
    }

    // Update is called once per frame
    void Update()
    {

        
    }

   public void Inicializar(out Arma a1,int cadencia,int damage)
    {
        a1.cadencia = cadencia;
        a1.damage = damage;
    }
    public void Modificador(ref Arma a1,int cadencia, int damage)
    {
        a1.cadencia = cadencia;
        a1.damage = damage;
    }
    public void OnParticleCollision(GameObject other)
    {
        
        if (other.gameObject.tag == "Enemigo")
        {
            
            //int hits = particulasDisparo.GetCollisionEvents(other, colisionBalas);

            enemigo = other.gameObject.GetComponent<Enemigo>();//Coje el script de cada enemigo que golpe entonces si golepa a 1 ese 1 recibira su daño a parte si golpea a enemigo 3 este recibira el daño
                                                               //Debug.Log(other.gameObject.name);
            enemigo.RecibirDisparo(arma.damage,this.gameObject.transform);
        }








    }
}
