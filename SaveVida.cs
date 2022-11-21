using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]


public class SaveVida
{

    public float vida,vidaActual;
    public float[] posicionPlayer = new float[3];
    



    public SaveVida(PlayerVida playerVida)
    {
        vida = playerVida.vida;
        vidaActual = playerVida.vidaActual;
        posicionPlayer = playerVida.posicionPlayer;


    }


}