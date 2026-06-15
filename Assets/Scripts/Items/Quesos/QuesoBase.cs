using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuesoBase : MonoBehaviour
{
    //------------------------------VARIABLES-------------------------------//

    //Configuracion basica del queso    
    public string nombreQueso;
    public int puntosQueOtorga;
    public float duracionEnMesada = 5f; // Valor de prueba
    public float tiempoDeSpawn = 5f;    // Valor de prueba
    private bool yaFueAgarrado = false; // Para evitar que se recolecte mas de una vez (temas de los frames)

    //Chequea entre "Soporte" y "Cazador"
    protected abstract string TagPermitido { get; }

    //------------------------------CONTADOR--------------------------------//
  
    private void Start()
    {StartCoroutine(ContadorDesaparecer());}

    private IEnumerator ContadorDesaparecer()
    {
        yield return new WaitForSeconds(duracionEnMesada);

        if (!yaFueAgarrado)
        {Destroy(gameObject);}
    }

    //------------------------------RECOLECCION-----------------------------//

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (yaFueAgarrado) return;

        // Verifica si el objeto que colisiona tiene el tag permitido (Soporte o Cazador)
        if (other.CompareTag(TagPermitido))
        {
            yaFueAgarrado = true;

            // Si el queso es para soporte
            if (TagPermitido == "Soporte")
            {
                DatosSoporte soporte = other.GetComponent<DatosSoporte>();
                if (soporte != null)
                {
                    RecolectarSoporte(soporte);
                }
            }

            // Si el queso es para cazador
            else if (TagPermitido == "Cazador")
            {
                DatosCazador cazador = other.GetComponent<DatosCazador>();
                if (cazador != null)
                {
                    RecolectarCazador(cazador);
                }
            }
        }
    }

    private void RecolectarSoporte(DatosSoporte soporte)
    {
        // Asumiendo que tu script DatosSoporte maneja los puntos, por ejemplo:
        // soporte.SumarPuntos(puntosQueOtorga);

        AplicarEfectoSoporte(soporte);
        Destroy(gameObject);
    }

    private void RecolectarCazador(DatosCazador cazador)
    {
        // Asumiendo que tu script DatosCazador maneja los puntos, por ejemplo:
        // cazador.SumarPuntos(puntosQueOtorga);

        AplicarEfectoCazador(cazador);
        Destroy(gameObject);
    }

    //------------------------------EFECTOS-------------------------------//

    // Para usar dentro de las clases hijas
    protected virtual void AplicarEfectoSoporte(DatosSoporte soporte) { }
    protected virtual void AplicarEfectoCazador(DatosCazador cazador) { }
}
