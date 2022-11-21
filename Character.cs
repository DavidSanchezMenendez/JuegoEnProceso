using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
   public RagdollDeath ragdoll;
    public int vida = 100, vidaActual;
     public Collider mainCollider; //capsules collider
        public Collider[] col; // collider that are in child objects Ex - hips,spine etc..
        public Animator animator;//Animator commponent
        public Rigidbody[] rb; //rigidbody compoments that are in child object ex -hips,spine etc. 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Golpeado(int damage)
    {
        vida -= damage;
        
        if (vida<= 0)
        {
          
        }
    }
}
