using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarPlayer : MonoBehaviour
{
    public Player Player;
    private void Start()
    {
        
    }
    private void Update()
    {
       
    }

    public void RotarMuñeco(float x, float z)
    {
        transform.Rotate(Vector3.up * x * 2);//para mirar a la derecha y izquierda y rotar el cuerpo 
    }
}
