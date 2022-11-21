using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]


public class SaveData
{

    public int vida;
    public float XP, XPMAX,lvl;
    
     

    public SaveData (SistemaXP EXP)
    {
        XP = EXP.XP;
        XPMAX = EXP.levelUP;
        lvl = EXP.lvl;


    }
  

}


