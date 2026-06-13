using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{

    [Header(". ݁₊ ⊹ . ݁ Referencias y Variables  ݁ . ⊹ ₊ ݁.")]
    [SerializeField] private GameObject menuPausa;  // ACORDARSE DE ARRASTRAR LA UI DE PAUSA
    private bool enPausa = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (enPausa)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Reanudar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;  // timeScale maneja la velocidad del tiempo en el juego
        enPausa = false;
    }

    public void Pausar()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f; // congela el tiempo (todo para)
        enPausa = true;
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f; // reaviva el tiempo 
        SceneManager.LoadScene("MenuInicial");
    }


}
