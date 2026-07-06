using UnityEngine;
using UnityEngine.UI;
using System.Collections; // --> Necesario para las corrutinas, lo puso flor
using TMPro;

public class UI_Cazador : MonoBehaviour
{
    [Header("⁺‧₊˚ ཐི⋆ Componentes de Barra ⋆ཋྀ ˚₊‧⁺")]
    [SerializeField] private Slider barraVida;
    [SerializeField] private Image escudoInmune;

    [Header("⁺‧₊˚ ཐི⋆ Habilidad Pasiva ⋆ཋྀ ˚₊‧⁺")]
    [SerializeField] private Slider barraHabilidadVioleta;
    [SerializeField] private Image imageFillHabilidad; 

    [Header("⁺‧₊˚ ཐི⋆ Componentes ⋆ཋྀ ˚₊‧⁺")]
    [SerializeField] private Image iconoPasiva;

    private DatosCazador personajeVigilado;
    private bool estaTitilando = false;

    void Awake()
    {
        if (escudoInmune != null) escudoInmune.gameObject.SetActive(false);
    }

    void Start()
    {
        // se intenta encontrar el personaje vigilado al inicio para asignarle la UI
        personajeVigilado = Object.FindFirstObjectByType<DatosCazador>();
        if (personajeVigilado != null) personajeVigilado.AsignarUI(this);
    }

    void Update()
    {
        if (personajeVigilado == null)
        {
            personajeVigilado = Object.FindFirstObjectByType<DatosCazador>();
            if (personajeVigilado != null) personajeVigilado.AsignarUI(this);
        }

        // Solo parpadea el componente Image del relleno (fill)
        if (estaTitilando && imageFillHabilidad != null)
        {
            // se apaga y prende el componente Image (el fill)
            imageFillHabilidad.enabled = (Mathf.FloorToInt(Time.time * 8f) % 2 == 0);
        }
    }

    public void ActualizarVida(int actual, int max)
    {
        if (barraVida != null) barraVida.value = (float)actual / max;
    }

    public void MostrarInmunidad(bool esInmune)
    {
        if (escudoInmune != null) escudoInmune.gameObject.SetActive(esInmune);
    }

    public void SetearLlenadoHabilidad(float porcentaje, bool debeTitilar = false)
    {
        if (barraHabilidadVioleta != null)
        {
            barraHabilidadVioleta.value = Mathf.Clamp01(porcentaje);
        }

        estaTitilando = debeTitilar;

        if (!debeTitilar && imageFillHabilidad != null)
        {
            imageFillHabilidad.enabled = true;
        }
    }
}