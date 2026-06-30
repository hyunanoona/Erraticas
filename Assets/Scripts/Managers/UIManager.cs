using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UIPrefabConfig
{
    public string nombreID;
    public GameObject prefabUI;
    public bool esCazador;
}

public class UIManager : MonoBehaviour
{
    [Header("⁺‧₊˚ ཐི⋆ Catalogo de UIs ⋆ཋྀ ˚₊‧⁺")]
    [SerializeField] private List<UIPrefabConfig> catalogoUIs;

    [Header("⁺‧₊˚ ཐི⋆ Contenedores en Pantalla ⋆ཋྀ ˚₊‧⁺")]
    [SerializeField] private Transform contenedorP1; 
    [SerializeField] private Transform contenedorP2; 

    // ⁺‧₊˚ ཐི⋆ Variables del Cazador ⋆ཋྀ ˚₊‧⁺
    private Health vidaCazador;
    private UI_Cazador uiCazador;
    private bool ultimoEstadoInmune = false;
    private int ultimaVidaCazador = -1;

    // ⁺‧₊˚ ཐི⋆ Variables del Soporte ⋆ཋྀ ˚₊‧⁺
    private JugadorController controladorSoporte;
    private UI_Soporte uiSoporte;
    private bool estabaSilenciado = false;

    public void ConfigurarInterfazNvl(string personajeP1, string personajeP2)
    {
        GameObject clonCazador = GameObject.FindWithTag("Cazador");
        GameObject clonSoporte = GameObject.FindWithTag("Soporte");

        // --- ⁺‧₊˚ ཐི⋆ JUGADOR UNO (LADO IZQUIERDO) ⋆ཋྀ ˚₊‧⁺ ---
        UIPrefabConfig configP1 = BuscarConfigPorNombre(personajeP1);
        if (configP1 != null && contenedorP1 != null)
        {
            GameObject uiInstanciadaP1 = Instantiate(configP1.prefabUI, contenedorP1);

            // configuración del espacio
            RectTransform rect = uiInstanciadaP1.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.pivot = new Vector2(0.5f, 0.5f);
                rect.anchoredPosition = Vector2.zero;
                rect.sizeDelta = Vector2.zero;
                rect.localScale = Vector3.one;
            }

            if (configP1.esCazador)
            {
                uiCazador = uiInstanciadaP1.GetComponent<UI_Cazador>();
                if (clonCazador != null) vidaCazador = clonCazador.GetComponent<Health>();
            }
            else
            {
                uiSoporte = uiInstanciadaP1.GetComponent<UI_Soporte>();
                if (clonSoporte != null) controladorSoporte = clonSoporte.GetComponent<JugadorController>();
            }
        }

        // --- ⁺‧₊˚ ཐི⋆ JUGADOR DOS (LADO DERECHO) ⋆ཋྀ ˚₊‧⁺ ---
        UIPrefabConfig configP2 = BuscarConfigPorNombre(personajeP2);
        if (configP2 != null && contenedorP2 != null)
        {
            GameObject uiInstanciadaP2 = Instantiate(configP2.prefabUI, contenedorP2);

            // configuración del espacio
            RectTransform rect = uiInstanciadaP2.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.pivot = new Vector2(0.5f, 0.5f);
                rect.anchoredPosition = Vector2.zero;
                rect.sizeDelta = Vector2.zero;
                rect.localScale = Vector3.one;
            }

            if (configP2.esCazador)
            {
                uiCazador = uiInstanciadaP2.GetComponent<UI_Cazador>();
                if (clonCazador != null) vidaCazador = clonCazador.GetComponent<Health>();
            }
            else
            {
                uiSoporte = uiInstanciadaP2.GetComponent<UI_Soporte>();
                if (clonSoporte != null) controladorSoporte = clonSoporte.GetComponent<JugadorController>();
            }
        }
    }

    void Update()
    {
        // --- ⁺‧₊˚ ཐི⋆ ACTUALIZACION DEL ESTADO DEL CAZADOR ⋆ཋྀ ˚₊‧⁺ ---
        if (vidaCazador != null && uiCazador != null)
        {
            if (vidaCazador.health != ultimaVidaCazador)
            {
                ultimaVidaCazador = vidaCazador.health;
                uiCazador.ActualizarVida(ultimaVidaCazador, vidaCazador.maxHealth);
            }
            if(vidaCazador.esInmune != ultimoEstadoInmune)
            {
                ultimoEstadoInmune = vidaCazador.esInmune;
                uiCazador.MostrarInmunidad(ultimoEstadoInmune);
            }
        }

        // --- ⁺‧₊˚ ཐི⋆ ACTUALIZACION DEL ESTADO DEL SOPORTE ⋆ཋྀ ˚₊‧⁺ ---
        if (controladorSoporte != null && uiSoporte != null)
        {
            bool estaSilenciadoAhora = controladorSoporte.TiempoSilenciadoRestante > 0f;
            if (estaSilenciadoAhora && !estabaSilenciado)
            {
                uiSoporte.ActivarCoolDownSilencio(controladorSoporte.TiempoSilenciadoRestante);
            }
            estabaSilenciado = estaSilenciadoAhora;
        }
    }

    private UIPrefabConfig BuscarConfigPorNombre(string nombre)
    {
        foreach (var config in catalogoUIs)
        {
            if (config.nombreID == nombre) return config;
        }
        return null;
    }
}