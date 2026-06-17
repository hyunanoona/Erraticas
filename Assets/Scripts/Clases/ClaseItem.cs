using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
    ╭━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╮
      ClaseItem ᝰ
        Se usara como estructura base para hacer los items recolectables para el jugador como son 
        los quesos y tambien los obstaculos que debera esquivar el mismo.
    ╰━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╯
*/

public abstract class ClaseItem : MonoBehaviour
{

    //  •• <<────────────────≪•◦ Variables ◦•≫────────────────>> ••

    //Configuracion del item
    public string nombreItem;

    // Configuracion de la interaccion
    public float duracionEnMesa;
    public float tiempoDeSpaw;

    // Configuracion de los puntos 
    public int puntosQueOtorga;
    public int puntosQueResta;
    public float puntosDeCarga;

    // Estado del item
    protected bool yaFueAgarrado = false;
    protected abstract string TagPermitido { get; }


    //  •• <<────────────────≪•◦ Tiempo ◦•≫────────────────>> ••

    private void Start()
    {
        StartCoroutine(ContadorDesapararecer());
    }

    private IEnumerator ContadorDesapararecer()
    {
        yield return new WaitForSeconds(duracionEnMesa);
        if (!yaFueAgarrado)
        {
            Destroy(gameObject);
        }
    }


    //  •• <<────────────────≪•◦ Detección Fisica ◦•≫────────────────>> ••


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si ya se esta por destruirse el item, evita otra recoleccion
        if (yaFueAgarrado) return;

        // Si el queso pide el tag "cazador" o el obstaculo pide "Ambos"
        // en caso de chocar con un queso o obstaculo se agarrara igual
        if (other.CompareTag(TagPermitido) || TagPermitido == "Ambos")
        {
            yaFueAgarrado = true;

            // Se filtra entre los dos tags que tiene ("cazador" y "Ambos")
            if (other.CompareTag("Cazador"))
            {
                DatosCazador cazador = other.GetComponent<DatosCazador>();
                if (cazador != null)
                {
                    RecoleccionCazador(cazador);
                }
            }

            else if (other.CompareTag("Soporte"))
            { 
                DatosSoporte soporte = other.GetComponent<DatosSoporte>();
                if (soporte != null)
                {
                    RecoleccionSoporte(soporte);
                }
            }
        }

    }

    //  •• <<────────────────≪•◦ Recolección ◦•≫────────────────>> ••

    private void RecoleccionCazador(DatosCazador cazador) 
    {
        //Puntaje
        if (GameManager.Instance != null) 
        { 
            GameManager.Instance.SumarPuntos(puntosQueOtorga);
            GameManager.Instance.SumarPuntos(-puntosQueResta);
        }

        //Recoleccion
        //cazador.CargarBarraHabilidad(puntosDeCarga);
        Destroy(gameObject);
    }

    private void RecoleccionSoporte(DatosSoporte soporte)
    {
        //Puntaje
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SumarPuntos(-puntosQueResta);
        }

        //Recoleccion
        //soporte.CargarBarraHabilidad(puntosDeCarga);
        Destroy(gameObject);
    }
}
