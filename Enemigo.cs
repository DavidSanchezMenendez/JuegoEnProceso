using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemigo : MonoBehaviour
{
    public int vida = 100,vidaActual;
    public Player player;

    public NavMeshAgent navMeshAgent;

    
    
    // Start is called before the first frame update
    void Start()
    {
        vidaActual = vida;
        navMeshAgent = GetComponent<NavMeshAgent>();


        
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = player.transform.position;
    }
    public void RecibirDisparo(int daño)
    {
        vidaActual -= daño;
        Debug.Log(gameObject.name);
        if (vidaActual<=0)
        {
            
            gameObject.SetActive(false);
            Destroy(this.gameObject);
            player.escojerNuevoEnemigo = false;//cuando un enemigo muere cambia el bool de Player para que pueda acceder a buscar un nuevo enemigo, sin esto se quedaría mirando a un enemigo ya meurto
            player.disparoParticulas.Stop();
            player.apuntando = false;
            

        }


    }
}
