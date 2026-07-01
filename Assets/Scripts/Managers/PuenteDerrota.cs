using UnityEngine;
using UnityEngine.SceneManagement;

public class PuenteDerrota : MonoBehaviour
{
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