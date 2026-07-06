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

    [Header("⁺‧₊˚ ཐི⋆ Componentes ⋆ཋྀ ˚₊‧⁺")]
    [SerializeField] private Image iconoPasiva;

    // 🌟 Almacenamos el personaje que esta interfaz va a vigilar
    private DatosCazador personajeVigilado;
    private bool estaTitilando = false; // 🌟 Controla si el Update debe hacer parpadear la barra

    void Awake()
    {
        if (escudoInmune != null) escudoInmune.gameObject.SetActive(false);
    }

    void Start()
    {
        // la UI busca al Cazador en la escena
        personajeVigilado = Object.FindFirstObjectByType<DatosCazador>();

        if (personajeVigilado != null)
        {
            personajeVigilado.AsignarUI(this);
        }
    }

    void Update()
    {
        if (personajeVigilado == null)
        {
            personajeVigilado = Object.FindFirstObjectByType<DatosCazador>();
            if (personajeVigilado != null)
            {
                personajeVigilado.AsignarUI(this);
            }
        }

        // 🌟 SI DEBE TITILAR: Apaga y prende el GameObject del Slider usando el reloj interno del juego
        if (estaTitilando && barraHabilidadVioleta != null)
        {
            // Alterna de visibilidad rápido (frecuencia multiplicada por 8f para que sea dinámico)
            barraHabilidadVioleta.gameObject.SetActive(Mathf.FloorToInt(Time.time * 8f) % 2 == 0);
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

    // 🌟 MODIFICADO: Ahora recibe si tiene que activar el modo parpadeo o no
    public void SetearLlenadoHabilidad(float porcentaje, bool debeTitilar = false)
    {
        if (barraHabilidadVioleta != null)
        {
            barraHabilidadVioleta.value = Mathf.Clamp01(porcentaje);
        }

        estaTitilando = debeTitilar;

        // Si ya terminó el tiempo y no debe titilar más, nos aseguramos de que quede visible
        if (!debeTitilar && barraHabilidadVioleta != null)
        {
            barraHabilidadVioleta.gameObject.SetActive(true);
        }
    }
}