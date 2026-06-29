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

    public void ConfigurarInterfazNvl(string personajeP1, string personajeP2)
    {
        GameObject clonCazador = GameObject.FindWithTag("Cazador");
        GameObject clonSoporte = GameObject.FindWithTag("Soporte");

        // --- ⁺‧₊˚ ཐི⋆ JUGADOR UNO (LADO IZQUIERDO) ⋆ཋྀ ˚₊‧⁺ ---
        UIPrefabConfig configP1 = BuscarConfigPorNombre(personajeP1);
        if (configP1 != null && contenedorP1 != null)
        {
            GameObject uiInstanciadaP1 = Instantiate(configP1.prefabUI, contenedorP1);
            GameObject jugador1Target = configP1.esCazador ? clonCazador : clonSoporte;
            if (jugador1Target != null)
            {
                ConectarEventos(jugador1Target, uiInstanciadaP1, configP1.esCazador);
            }
        }
        // ✦•┈๑⋅⋯ ⋯⋅๑┈•✦✦•┈๑⋅⋯ ⋯⋅๑┈•✦✦•┈๑⋅⋯ ⋯⋅๑┈•✦✦•┈๑⋅⋯ ⋯⋅๑┈•✦

        // --- ⁺‧₊˚ ཐི⋆ JUGADOR DOS (LADO DERECHO) ⋆ཋྀ ˚₊‧⁺ ---
        UIPrefabConfig configP2 = BuscarConfigPorNombre(personajeP2);
        if (configP2 != null && contenedorP2 != null)
        {
            GameObject uiInstanciadaP2 = Instantiate(configP2.prefabUI, contenedorP2);
            GameObject jugador2Target = configP2.esCazador ? clonCazador : clonSoporte;
            if (jugador2Target != null)
            {
                ConectarEventos(jugador2Target, uiInstanciadaP2, configP2.esCazador);
            }
        }
        // ✦•┈๑⋅⋯ ⋯⋅๑┈•✦✦•┈๑⋅⋯ ⋯⋅๑┈•✦✦•┈๑⋅⋯ ⋯⋅๑┈•✦✦•┈๑⋅⋯ ⋯⋅๑┈•✦
    }

    private void ConectarEventos(GameObject jugador, GameObject uiInstanciada, bool esCazador)
    {
        Health scriptVida = jugador.GetComponent<Health>();
        if (scriptVida == null) return;

        if (esCazador)
        {
            UI_Cazador uiCazador = uiInstanciada.GetComponent<UI_Cazador>();
            if (uiCazador != null)
            {
                // hacer que la UI de cazador consulte la vida actual y actualizar
            }
        }
        else
        {
            UI_Soporte uiSoporte = uiInstanciada.GetComponent<UI_Soporte>();
            if (uiSoporte != null)
            {
                // UI_Soporte.ActivarCooldownSilencio(5f);
            }
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