using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    public static Moneda m;
    public Vector3 direccion;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        m = this;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
         
      
        direccion = (Player.p.transform.position-transform.position );

        rb.velocity = direccion ;//de decimos que la velocidad sea la resta de la posicion del player menos la de la moneda lo que hace que la moneda tenga la velocidad que le separa de el player lo que a la vez mueve la moneda hasta que se queda quieta en el 0,0,0 lo que hace que no se mueva una vez al lado del player
    }
}
