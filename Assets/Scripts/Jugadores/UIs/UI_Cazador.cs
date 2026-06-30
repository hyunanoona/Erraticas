using UnityEngine;
using UnityEngine.UI;
using System.Collections; // --> Necesario para las corrutinas, lo puso flor
using TMPro;

public class UI_Cazador : MonoBehaviour
{
    [Header("⁺‧₊˚ ཐི⋆ Componentes de Barra ⋆ཋྀ ˚₊‧⁺")]
    [SerializeField] private Slider barraVida;
    [SerializeField] private Image escudoInmune;

    //------------------------------------------------- agregado de flor //
    [Header("⁺‧₊˚ ཐི⋆ Habilidad Pasiva ⋆ཋྀ ˚₊‧⁺")]
    [SerializeField] private Slider barraHabilidad;
    [SerializeField] private Image fillBarraHabilidad; 

    //[Tooltip("Tipo de queso que requiere este cazador")]
    //[SerializeField] private string tipoDeQueso = "queso_pategras";
    [Tooltip("Cantidad de quesos necesarios para activar la habilidad")]
    [SerializeField] private int quesosMaximosRequeridos = 5;

    private Coroutine corrutinaTitileo;
    private bool estaTitilando = false;
    //------------------------------------------------- agregado de flor //

    [Header("⁺‧₊˚ ཐི⋆ Componentes ⋆ཋྀ ˚₊‧⁺")]
    [SerializeField] private Image iconoPasiva;

    void Awake()
    {
        escudoInmune.gameObject.SetActive(false);
        ActualizarContadorQuesosPasiva(0); // --> lo puso flor
    }

    public void ActualizarVida(int actual, int max) 
    {
        barraVida.value = (float)actual / max;
    }

    public void MostrarInmunidad(bool esInmune)
    {
        escudoInmune.gameObject.SetActive(esInmune);
    }



    //------------------------------------------------- agregado de flor //
    public void ActualizarContadorQuesosPasiva(int cantidadActual)
    {
        // Se calcula el porcentaje y actualizar la barrita 
        float porcentaje = (float)cantidadActual / quesosMaximosRequeridos;
        barraHabilidad.value = Mathf.Clamp01(porcentaje);


        // Maneja el titileo 
        if (cantidadActual >= quesosMaximosRequeridos)
        {
            if (!estaTitilando)
            {
                corrutinaTitileo = StartCoroutine(TitilarBarraAmarilla());
            }
        }
        else
        {
            // Si la habilidad se gasta
            if (estaTitilando)
            {
                StopCoroutine(corrutinaTitileo);
                estaTitilando = false;
                fillBarraHabilidad.color = Color.yellow; // Volver al color base fijo
            }
        }
    }

    private IEnumerator TitilarBarraAmarilla()
    {
        estaTitilando = true;
        Color colorOriginal = Color.yellow;
        Color colorOculto = new Color(colorOriginal.r, colorOriginal.g, colorOriginal.b, 0.2f); // Casi transparente

        while (estaTitilando)
        {
            fillBarraHabilidad.color = colorOculto;
            yield return new WaitForSeconds(0.2f); // Velocidad del parpadeo
            fillBarraHabilidad.color = colorOriginal;
            yield return new WaitForSeconds(0.2f);
        }
    }

    //------------------------------------------------- agregado de flor //
}
