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

    // Configuracion del tiempo del item
    public float duracionEnMesa;
    public float tiempoDeSpaw;

    // Puntos para la barra de habilidad 
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
        if (yaFueAgarrado) return; // evita otra recoleccion si ya fue agarrado por el jugador

        print($"Objeto detectado: {other.gameObject.name} con Tag: '{other.tag}'. TagPermitido de este ítem es: '{TagPermitido}'"); //DE PRUEBA (BORRAR LUEGO)

        // logica para cuando es tocado por el soporte
        if (other.CompareTag("Soporte") && (TagPermitido == "Soporte" || TagPermitido == "Ambos"))
        {
            yaFueAgarrado = true; // evita otra recoleccion si ya fue agarrado por el jugador
            
            DatosSoporte soporte = other.GetComponent<DatosSoporte>(); // obtenemos el componente del soporte que lo toco

            if (soporte != null)
            {
                AplicarEfectoSoporte(soporte); // ejecutamos la logica de recoleccion del soporte
                Destroy(gameObject); // destruimos el item recolectable
            }
        }

        // logica para cuando es tocado por el cazador
        else if (other.CompareTag("Cazador") && (TagPermitido == "Cazador" || TagPermitido == "Ambos")) // solo se ejecuta si el item es para el cazador o para ambos
        {
            yaFueAgarrado = true; // evita otra recoleccion si ya fue agarrado por el jugador

            DatosCazador cazador = other.GetComponent<DatosCazador>(); // obtenemos el componente del cazador que lo toco
            
            if (cazador != null)
            {
                AplicarEfectoCazador(cazador); // ejecutamos la logica de recoleccion del cazador
                Destroy(gameObject); // destruimos el item recolectable
            }
        }

    }

/*
    //  •• <<────────────────≪•◦ Recolección ◦•≫────────────────>> ••

    private void RecoleccionCazador(DatosCazador cazador) 
    {
        //cazador.CargarBarraHabilidad(puntosDeCarga); --> Carga la habiliadad pasiva
        AplicarEfectoCazador(cazador);
        Destroy(gameObject);
    }

    private void RecoleccionSoporte(DatosSoporte soporte)
    {
        AplicarEfectoSoporte(soporte);
        Destroy(gameObject);
    }
*/

    //  •• <<────────────────≪•◦ Efectos Obstaculos ◦•≫────────────────>> ••

    // Estan vacios ya que dps los obstaculos y quesos tendran su logica de daño o bufos
    protected virtual void AplicarEfectoCazador(DatosCazador cazador) { }
    protected virtual void AplicarEfectoSoporte(DatosSoporte soporte) { }
}
