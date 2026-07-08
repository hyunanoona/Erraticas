using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuenteVictoria : MonoBehaviour
{
    [Header("⁺‧₊˚ ཐི⋆ Configuración de la UI de Victoria ⋆ཋྀ ˚₊‧⁺")]
    [SerializeField] private Button buttonMenuInicial;
    [SerializeField] private Button buttonSalir;

    private void Awake()
    {
            if (buttonMenuInicial != null) buttonMenuInicial.onClick.AddListener(ClickBotonMenuInicial);
            if (buttonSalir != null) buttonSalir.onClick.AddListener(ClickBotonSalir);
    }

    public void ClickBotonMenuInicial()
    {
        SceneManager.LoadScene("MenuInicial");
    }

    public void ClickBotonSalir()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego >⩊<...");
    }
}