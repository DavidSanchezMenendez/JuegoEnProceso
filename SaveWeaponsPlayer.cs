using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]


public class SaveWeaponsPlayer
{

     
   public int weapon;




    public SaveWeaponsPlayer(Player playerWeapon)
    {
       
        weapon = playerWeapon.inventario;


    }


}