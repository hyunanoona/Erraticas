using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaMortal : MonoBehaviour
{

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
            if(GameManager.Instance != null) GameManager.Instance.EstablecerDerrota();

        }
    }

}
