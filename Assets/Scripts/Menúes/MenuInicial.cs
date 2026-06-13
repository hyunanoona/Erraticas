using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicial : MonoBehaviour
{
    [Header(". ݁₊ ⊹ . ݁ Referencias y Variables  ݁ . ⊹ ₊ ݁.")]
    [SerializeField] private Button botonJugar;
    [SerializeField] private Button botonSalir;

    void Start()
    {
        if (botonJugar != null)
        {
            botonJugar.onClick.AddListener(Jugar);
        }

        if (botonSalir != null)
        {
            botonSalir.onClick.AddListener(Salir);
        }
    }
    public void Jugar()
    {
        SceneManager.LoadScene("NivelUno");
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }


}
