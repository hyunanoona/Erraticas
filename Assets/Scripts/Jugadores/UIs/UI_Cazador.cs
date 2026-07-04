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
    }

    public void ActualizarVida(int actual, int max)
    {
        if (barraVida != null) barraVida.value = (float)actual / max;
    }

    public void MostrarInmunidad(bool esInmune)
    {
        if (escudoInmune != null) escudoInmune.gameObject.SetActive(esInmune);
    }

    // metodo para actualizar la barra de habilidad en la UI del cazador
    public void SetearLlenadoHabilidad(float porcentaje)
    {
        if (barraHabilidadVioleta != null)
        {
            barraHabilidadVioleta.value = Mathf.Clamp01(porcentaje);
        }
    }
}
