using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuenteDerrota : MonoBehaviour
{

    [Header("Botones")]
    [SerializeField] private Button botonReiniciarNvl;
    [SerializeField] private Button botonMenuInicial;
    [SerializeField] private Button botonSalir;

    public void Awake()
    {
        botonReiniciarNvl.onClick.AddListener(ClickBotonReiniciarNvl);
        botonMenuInicial.onClick.AddListener(ClickBotonMenuInicial);
        botonSalir.onClick.AddListener(ClickBotonSalir);
    }

    public void ClickBotonReiniciarNvl()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReiniciarNivelActual();
            Debug.Log("Reiniciando nivel actual...");
        }
        else
        {
            Debug.LogWarning("El GameManager no se encontró en la escena");
            SceneManager.LoadScene("MenuInicial");
        }
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