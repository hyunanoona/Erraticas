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
        if (yaFueAgarrado) return;

        // Si lo toca el soporte 
        if (other.CompareTag("Soporte"))
        {
            // Si el queso no es para el Soporte ni para ambos lo ignora por completo
            if (TagPermitido != "Soporte" && TagPermitido != "Ambos") return;

            DatosSoporte soporte = other.GetComponent<DatosSoporte>() ?? other.GetComponentInParent<DatosSoporte>();
            if (soporte != null)
            {
                yaFueAgarrado = true;
                print($"¡{other.gameObject.name} (Soporte) recolectó con exito: {nombreItem}!");
                AplicarEfectoSoporte(soporte);
                Destroy(gameObject);
            }
        }

        // Si lo toca el cazador
        else if (other.CompareTag("Cazador"))
        {
            // Si el queso no es para el Cazador ni para ambos lo ignora por completo
            if (TagPermitido != "Cazador" && TagPermitido != "Ambos") return;

            DatosCazador cazador = other.GetComponent<DatosCazador>() ?? other.GetComponentInParent<DatosCazador>();
            if (cazador != null)
            {
                yaFueAgarrado = true;
                print($"¡{other.gameObject.name} (Cazador) recolectó con éxito: {nombreItem}!");
                AplicarEfectoCazador(cazador);
                Destroy(gameObject);
            }
        }
    }

    //  •• <<────────────────≪•◦ Efectos Obstaculos ◦•≫────────────────>> ••

    // Estan vacios ya que dps los obstaculos y quesos tendran su logica de daño o bufos
    protected virtual void AplicarEfectoCazador(DatosCazador cazador) { }
    protected virtual void AplicarEfectoSoporte(DatosSoporte soporte) { }
}
