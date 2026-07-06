using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


public class UI_Soporte : MonoBehaviour
{
    [Header("⁺‧₊˚ ཐི⋆ Componentes Comunes ⋆ཋྀ ˚₊‧⁺")]
    [SerializeField] private Image iconoHabilidad1;
    [SerializeField] private Image iconoHabilidad2;
    [SerializeField] private GameObject contenedorSilencio; 
    [SerializeField] protected TextMeshProUGUI textoContadorHab1;
    [SerializeField] protected TextMeshProUGUI textoContadorHab2;


    [Header("⁺‧₊˚ ཐི⋆ Configuración de Quesos por Rol ⋆ཋྀ ˚₊‧⁺")]
    [Tooltip("Escribir exactamente: Roquefort, Mozzarella, Provoleta o Cremoso")]
    [SerializeField] private string tipoQuesoHabilidad1; 
    [SerializeField] private string tipoQuesoHabilidad2;

    [Header("⁺‧₊˚ ཐི⋆ Ajustes limite ⋆ཋྀ ˚₊‧⁺")]
    [SerializeField] protected int limiteQuesosHab1 = 4;
    [SerializeField] protected int limiteQuesosHab2 = 4;

    // ⁺‧₊˚ ཐི⋆ Referencia al Personaje ⋆ཋྀ ˚₊‧⁺
    private DatosSoporte datosSoporte; 

    private Coroutine corrutinaSilencio;

    void Awake()
    {
        if (contenedorSilencio != null) contenedorSilencio.gameObject.SetActive(false);
    }

    void Start()
    {
        GameObject jugadorSoporte = GameObject.FindWithTag("Soporte"); 

        if (jugadorSoporte != null)
        {
            datosSoporte = jugadorSoporte.GetComponent<DatosSoporte>();
        }
        else
        {
            Debug.LogWarning("No hay ningun GameObject con el tag 'Soporte'");
        }
        ActualizarVisualizadores();
    }

    public void ActualizarVisualizadores()
    {
        if (datosSoporte == null) return;

        int quesosActualesHab1 = datosSoporte.ObtenerCantidadQueso(tipoQuesoHabilidad1);
        int quesosActualesHab2 = datosSoporte.ObtenerCantidadQueso(tipoQuesoHabilidad2);

        if (textoContadorHab1 != null)
        {
            textoContadorHab1.text = $"{quesosActualesHab1}/{limiteQuesosHab1}";
        }
        if (textoContadorHab2 != null)
        {
            textoContadorHab2.text = $"{quesosActualesHab2}/{limiteQuesosHab2}";
        }
    }

    // Métodos de validación directo con la data real
    public bool PuedeUsarHabilidad1() => datosSoporte != null && datosSoporte.ObtenerCantidadQueso(tipoQuesoHabilidad1) >= limiteQuesosHab1;
    public bool PuedeUsarHabilidad2() => datosSoporte != null && datosSoporte.ObtenerCantidadQueso(tipoQuesoHabilidad2) >= limiteQuesosHab2;
    
    // ⁺‧₊˚ ཐི⋆ Silenciar habilidades visualmente ⋆ཋྀ ˚₊‧⁺

    public void ActivarCoolDownSilencio(float duracion)
    {
        if (corrutinaSilencio != null)
        {
            StopCoroutine(corrutinaSilencio);
        }
        corrutinaSilencio = StartCoroutine(RutinaRelojSilencio(duracion));
    }

    private IEnumerator RutinaRelojSilencio(float duracion)
    {
        if (contenedorSilencio == null) yield break;
        contenedorSilencio.gameObject.SetActive(true);
        
        float tiempoPasado = 0f;

        while (tiempoPasado < duracion)
        {
            tiempoPasado += Time.deltaTime;
            yield return null;
        }

        contenedorSilencio.gameObject.SetActive(false);

    }

}
