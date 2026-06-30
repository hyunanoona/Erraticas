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

    // ⁺‧₊˚ ཐི⋆ VARIABLES JUGADOR 1 (LADO IZQUIERDO) ⋆ཋྀ ˚₊‧⁺
    private UI_Cazador uiCazadorP1;
    private Health vidaCazadorP1;
    private int ultimaVidaCazadorP1 = -1;
    private bool ultimoInmuneCazadorP1 = false;

    private UI_Soporte uiSoporteP1;
    private JugadorController controladorSoporteP1;
    private bool estabaSilenciadoP1 = false;

    // ⁺‧₊˚ ཐི⋆ VARIABLES JUGADOR 2 (LADO DERECHO) ⋆ཋྀ ˚₊‧⁺
    private UI_Cazador uiCazadorP2;
    private Health vidaCazadorP2;
    private int ultimaVidaCazadorP2 = -1;
    private bool ultimoInmuneCazadorP2 = false;

    private UI_Soporte uiSoporteP2;
    private JugadorController controladorSoporteP2;
    private bool estabaSilenciadoP2 = false;


    public void ConfigurarInterfazNvl(string personajeP1, string personajeP2)
    {
        // Buscamos los clones en la escena de manera segura
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
                // rect.anchorMin = Vector2.zero;
                // rect.anchorMax = Vector2.one;
                // rect.pivot = new Vector2(0.5f, 0.5f);
                rect.anchoredPosition = Vector2.zero;
                // rect.sizeDelta = Vector2.zero;
                rect.localScale = Vector3.one;
            }

            // Dependiendo de qué eligió el P1, le asignamos sus referencias del lado P1
            if (configP1.esCazador)
            {
                uiCazadorP1 = uiInstanciadaP1.GetComponent<UI_Cazador>();
                if (clonCazador != null) vidaCazadorP1 = clonCazador.GetComponent<Health>();
            }
            else
            {
                uiSoporteP1 = uiInstanciadaP1.GetComponent<UI_Soporte>();
                if (clonSoporte != null) controladorSoporteP1 = clonSoporte.GetComponent<JugadorController>();
            }
        }

        // --- ⁺‧₊˚ ཐི⋆ JUGADOR DOS (LADO DERECHO) ⋆ཋྀ ˚₊‧⁺ ---
        UIPrefabConfig configP2 = BuscarConfigPorNombre(personajeP2);
        if (configP2 != null && contenedorP2 != null)
        {
            GameObject uiInstanciadaP2 = Instantiate(configP2.prefabUI, contenedorP2);

            RectTransform rect = uiInstanciadaP2.GetComponent<RectTransform>();
            if (rect != null)
            {
                // rect.anchorMin = Vector2.zero;
                // rect.anchorMax = Vector2.one;
                // rect.pivot = new Vector2(0.5f, 0.5f);
                rect.anchoredPosition = Vector2.zero;
                // rect.sizeDelta = Vector2.zero;
                rect.localScale = Vector3.one;
            }

            // Dependiendo de qué eligió el P2, le asignamos sus referencias del lado P2
            if (configP2.esCazador)
            {
                uiCazadorP2 = uiInstanciadaP2.GetComponent<UI_Cazador>();
                if (clonCazador != null) vidaCazadorP2 = clonCazador.GetComponent<Health>();
            }
            else
            {
                uiSoporteP2 = uiInstanciadaP2.GetComponent<UI_Soporte>();
                if (clonSoporte != null) controladorSoporteP2 = clonSoporte.GetComponent<JugadorController>();
            }
        }
    }

    void Update()
    {
        // ------------------ ACTUALIZACION JUGADOR 1 (IZQUIERDA) ------------------
        // Si el P1 es Cazador...
        if (uiCazadorP1 != null && vidaCazadorP1 != null)
        {
            if (vidaCazadorP1.health != ultimaVidaCazadorP1)
            {
                ultimaVidaCazadorP1 = vidaCazadorP1.health;
                uiCazadorP1.ActualizarVida(ultimaVidaCazadorP1, vidaCazadorP1.maxHealth);
            }
            if (vidaCazadorP1.esInmune != ultimoInmuneCazadorP1)
            {
                ultimoInmuneCazadorP1 = vidaCazadorP1.esInmune;
                uiCazadorP1.MostrarInmunidad(ultimoInmuneCazadorP1);
            }
        }
        // Si el P1 es Soporte...
        else if (uiSoporteP1 != null && controladorSoporteP1 != null)
        {
            bool estaSilenciadoAhora = controladorSoporteP1.TiempoSilenciadoRestante > 0f;
            if (estaSilenciadoAhora && !estabaSilenciadoP1)
            {
                uiSoporteP1.ActivarCoolDownSilencio(controladorSoporteP1.TiempoSilenciadoRestante);
            }
            estabaSilenciadoP1 = estaSilenciadoAhora;
        }

        // ------------------ ACTUALIZACION JUGADOR 2 (DERECHA) ------------------
        // Si el P2 es Cazador...
        if (uiCazadorP2 != null && vidaCazadorP2 != null)
        {
            if (vidaCazadorP2.health != ultimaVidaCazadorP2)
            {
                ultimaVidaCazadorP2 = vidaCazadorP2.health;
                uiCazadorP2.ActualizarVida(ultimaVidaCazadorP2, vidaCazadorP2.maxHealth);
            }
            if (vidaCazadorP2.esInmune != ultimoInmuneCazadorP2)
            {
                ultimoInmuneCazadorP2 = vidaCazadorP2.esInmune;
                uiCazadorP2.MostrarInmunidad(ultimoInmuneCazadorP2);
            }
        }
        // Si el P2 es Soporte...
        else if (uiSoporteP2 != null && controladorSoporteP2 != null)
        {
            bool estaSilenciadoAhora = controladorSoporteP2.TiempoSilenciadoRestante > 0f;
            if (estaSilenciadoAhora && !estabaSilenciadoP2)
            {
                uiSoporteP2.ActivarCoolDownSilencio(controladorSoporteP2.TiempoSilenciadoRestante);
            }
            estabaSilenciadoP2 = estaSilenciadoAhora;
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