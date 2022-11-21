using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVida : MonoBehaviour
{
    public static PlayerVida pV;
    public RagdollDeath ragdoll;
    public float vida = 100, vidaActual;
    public Image barradeVida;
    public Player player;
    public float[] posicionPlayer = new float[3];
    

    // Start is called before the first frame update
    void Start()
    {
        vidaActual = vida;
        player = GetComponent<Player>();
        SaveVida vidaPlayer = SaveManager.LoadVida();
        vidaActual = vidaPlayer.vidaActual;
        vida = vidaPlayer.vida;
        barradeVida.fillAmount = vidaActual / vida;
        pV = this;
         //Player.p.transform.position = new Vector3(vidaPlayer.posicionPlayer[0], vidaPlayer.posicionPlayer[1], vidaPlayer.posicionPlayer[2]);//spawneamos al player
       
      

    }

    // Update is called once per frame
    void Update()
    {
       
        barradeVida.transform.LookAt(player.cam.transform);

        float floatvida = vidaActual / vida;
        if (floatvida == 1)
        {
            barradeVida.color = Color.green;
        }
        else if (floatvida < 0.75f && floatvida > 0.5f)
        {
            barradeVida.color = Color.yellow;
        }
        else if (floatvida < 0.5f)
        {
            barradeVida.color = Color.red;
        }
       
       
    }
    
    public void Golpeado(float damage)
    {
        vidaActual -= damage;
        barradeVida.fillAmount = vidaActual / vida;
        SaveManager.SaveVida(this);
        
        //Debug.Log(vida);
        if (vida <= 0)
        {
            //ragdoll.isRagdoll(true);
        }
    }
    public void GuardarPosicionPlayer()//Enviamos que guarde la posicion actual del Player, normalmenta al updatar las torretas op mejorar armas
    {
        posicionPlayer[0] = Player.p.transform.position.x;
        posicionPlayer[1] = Player.p.transform.position.y;
        posicionPlayer[2] = Player.p.transform.position.z;
        SaveManager.SaveVida(this);
    }

}
