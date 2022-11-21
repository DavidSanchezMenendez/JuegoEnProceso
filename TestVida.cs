using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestVida : MonoBehaviour
{
    public Image barraDeVida;
    public float vida = 100, vidaActual;
    // Start is called before the first frame update
    public Camera cam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        barraDeVida.fillAmount = vidaActual / vida;
       
        barraDeVida.transform.LookAt(new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z));
    }
}
