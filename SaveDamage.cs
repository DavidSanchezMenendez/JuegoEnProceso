using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class SaveDamage
    {

        public int damage;
    public List<int> damageList = new List<int>();


        public SaveDamage(Torreta2 torretaDamage)
        {
            damage = torretaDamage.damage;
        damageList = torretaDamage.damage1;
        }

    }

