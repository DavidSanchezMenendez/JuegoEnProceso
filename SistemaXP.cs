using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SistemaXP : MonoBehaviour
{
    public float XP = 0, lvl=0;
    public float levelUP = 100;
    public float getEXP = 50;
    public Image barraEXP;
    public Text textEXP;

    // Start is called before the first frame update
    void Start()
    {
        SaveData expData = SaveManager.LoadEXPData();
        XP = expData.XP;
        levelUP = expData.XPMAX;
        lvl = expData.lvl;
        textEXP.text = "lvl " + lvl;
    }

    // Update is called once per frame
    void Update()
    {
        barraEXP.fillAmount = XP / levelUP;
        if (Input.GetKeyDown(KeyCode.G))
        {
            SaveData expData = SaveManager.LoadEXPData();
            XP = expData.XP;
            levelUP = expData.XPMAX;
            lvl = expData.lvl;
            textEXP.text = "lvl " + lvl;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SaveManager.SaveEXPData(this);
            Debug.Log("Datos Guardados");

        }
    }
    public void GetEXP()//cada vez que mata un enemigo
    {
        XP += getEXP;
        SaveManager.SaveEXPData(this);
        if (XP >= levelUP)
        {
            XP = 0;
            levelUP *= 1.3f;
            lvl++;
            textEXP.text = "lvl " + lvl;
            
           // Debug.Log("Datos Guardados");

        }
    }
}
