using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemigos : MonoBehaviour
{
    public List<GameObject> Enemigos = new List<GameObject>();
    // Start is called before the first frame update
    public Object Enemigo;
    void Start()
    {
        Enemigos = GenerarEnemigos(20);
    }

    // Update is called once per frame
    
    public List<GameObject> GenerarEnemigos(int numeroEnemigos)
    {

        for (int i = 0; i <= numeroEnemigos; i++)
        {
            Enemigos.Add(Instantiate(Enemigo) as GameObject);
        }
        return Enemigos;
    }
    public void EliminarEnemigos()
    {
        for (int i = 0; i <= Enemigos.Count; i++)
        {
            Destroy(Enemigos[i]);
        }
    }
}
