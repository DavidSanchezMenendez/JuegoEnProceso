


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class SaveDataTorretas
{
    public int numeroTorretas;
    public int dañoTorreta;
    public float[] posicionTorreta = new float[3];

    public List<float> posicionTorreta1 = new List<float>();

    public SaveDataTorretas(SistemadeSpawnTorretas TorretasSave)
    {
        numeroTorretas = TorretasSave.a;
        for (int i = 0; i < numeroTorretas; i++)
        {
            posicionTorreta1.Add(TorretasSave.torretas[i].transform.position.x);//añadimos a la List la posicion x
            posicionTorreta1.Add(TorretasSave.torretas[i].transform.position.y);//añadimos a la List la posicion y
            posicionTorreta1.Add(TorretasSave.torretas[i].transform.position.z);//añadimos a la List la posicion z
        }

        



        //torretas = TorretasSave.torretas;

        


    }

}

