using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuesoBase : MonoBehaviour
{
    //------------------------------VARIABLES-------------------------------//

    //Configuracion basica del queso    
    public string nombreQueso = "Queso Pategras";
    public int puntosQueOtorga;
    public float duracionEnMesada = 5f;                 // Valor de prueba
    public float tiempoDeSpawn = 5f;                    // Valor de prueba
    private bool yaFueAgarrado = false;                 // Para evitar que se recolecte mas de una vez (temas de los frames)
    protected virtual string TagPermitido => "Cazador"; //Por defecto ya que el queso solo lo agarra el Cazador

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

        if (other.CompareTag(TagPermitido))
        {
            yaFueAgarrado = true;

            // Si lo agarra el Cazador 
            if (TagPermitido == "Cazador")
            {
                DatosCazador cazador = other.GetComponent<DatosCazador>();
                if (cazador != null)
                {
                    RecolectarCazador(cazador);
                }
            }

            // Si lo agarra el Soporte
            else if (TagPermitido == "Soporte")
            {
                DatosSoporte soporte = other.GetComponent<DatosSoporte>();
                if (soporte != null)
                {
                    RecolectarSoporte(soporte);
                }
            }
        }
    }

    private void RecolectarCazador(DatosCazador cazador)
    {
        AplicarEfectoCazador(cazador);
        Destroy(gameObject);
    }

    private void RecolectarSoporte(DatosSoporte soporte){}


    //------------------------------EFECTOS-------------------------------//

    protected virtual void AplicarEfectoCazador(DatosCazador cazador)
    {
        GameManager.Instance.SumarPuntos(puntosQueOtorga);
    }
}