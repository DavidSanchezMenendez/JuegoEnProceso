using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    

public class Inventario : MonoBehaviour
{
    // Start is called before the first frame update
    public static Inventario iN;
    public Button inventario, m4, escopeta, pistola,ok;
 
    public int selectArma;

    void Start()
    {
        iN = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AbrirInventario()
    {
        inventario.gameObject.SetActive(true);
        Time.timeScale = 0;
        

    }
    public void SelecionarM4()
    {
        //player.inventario = 1;
        selectArma = 1;
    }
    public void SelecionarEscopeta()
    {
        // player.inventario = 2;
        selectArma = 2;
    }
    public void SelecionarPistola()
    {
        //player.inventario = 3;
        selectArma = 3;
    }

    public void OK()
    {
        Time.timeScale = 1;
        Player.p.inventario = selectArma;
        
    }
}
