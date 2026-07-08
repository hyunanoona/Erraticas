using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaMortal : MonoBehaviour
{

    public AudioClip miau;

    void Awake()
    {
        gameObject.tag = "PlataformaAsesina";

        Collider2D col = GetComponent<Collider2D>();
        if (col == null)
        {
            col.isTrigger = true;
        }
        else {return;}
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Cazador") || otro.CompareTag("Soporte") || otro.GetComponent<InputJugador>() != null)
        {
            
                AudioSource miAudio = GetComponent<AudioSource>();  
                if (miau != null)
                    {
                        miAudio.PlayOneShot(miau);
                    }
            
            if(GameManager.Instance != null) GameManager.Instance.EstablecerDerrota();
        }
    }

}
