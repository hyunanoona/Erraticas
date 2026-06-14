using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuInicial : MonoBehaviour
{
    [Header(". ݁₊ ⊹ . ݁ Referencias y Variables  ݁ . ⊹ ₊ ݁.")]
    [SerializeField] private Button botonJugar;
    [SerializeField] private Button botonAjustes;
    [SerializeField] private Button botonSalir;
    [SerializeField] private GameObject prefabAjustes;
    [SerializeField] private Transform canvasPadre;
    [SerializeField] private TMP_Text tituloTexto;

    private GameObject instanciaAjustes;

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

        if(botonAjustes != null)
        {
            botonAjustes.onClick.AddListener(AbrirAjustes);
        }
    }

    public void Jugar()
    {
        SceneManager.LoadScene("NivelUno");
    }

    public void AbrirAjustes()
    {

        AlternarVisibilidadMenu(false);

        if(instanciaAjustes != null)
        {
            instanciaAjustes.SetActive(true);
            instanciaAjustes.transform.SetAsLastSibling(); // Asegura que el panel de ajustes esté al frente
            return;
        }

        if(prefabAjustes != null && canvasPadre != null)
        {
            instanciaAjustes = Instantiate(prefabAjustes, canvasPadre);
            instanciaAjustes.transform.SetAsLastSibling(); 

            Ajustes scriptAjustes = instanciaAjustes.GetComponent<Ajustes>();
            if(scriptAjustes != null)
            {
                scriptAjustes.ConfigurarMenuPadre(this);
            }

            RectTransform rect = instanciaAjustes.GetComponent<RectTransform>();
            if(rect != null)
            {
                rect.localPosition = Vector3.zero;
                rect.localScale = Vector3.one;
            }

        }

    }

    public void ActivarMenuDesdeAjustes()
    {
        AlternarVisibilidadMenu(true);
    }

    public void AlternarVisibilidadMenu(bool visible)
    {
        if (botonJugar != null) botonJugar.gameObject.SetActive(visible);
        if (botonAjustes != null) botonAjustes.gameObject.SetActive(visible);
        if (botonSalir != null) botonSalir.gameObject.SetActive(visible);       
        if (tituloTexto != null) tituloTexto.gameObject.SetActive(visible);
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }

}
