using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{

    [Header(". ݁₊ ⊹ . ݁ Referencias y Variables  ݁ . ⊹ ₊ ݁.")]
    [SerializeField] private GameObject menuPausa;  
    [SerializeField] private Button botonReanudar;
    [SerializeField] private Button botonAjustes;
    [SerializeField] private Button botonMenuInicial;
    [SerializeField] private Button botonSalir;
    [SerializeField] private Transform canvasPadre;
    [SerializeField] private GameObject prefabAjustes;
    private bool enPausa = false;
    private GameObject instanciaAjustes;

    void Start()
    {
        if (botonReanudar != null)
        {
            botonReanudar.onClick.AddListener(Reanudar);
        }
        if(botonAjustes != null)
        {
            botonAjustes.onClick.AddListener(AbrirAjustes);
        }
        if (botonSalir != null)
        {
            botonSalir.onClick.AddListener(Salir);
        }
        if(botonMenuInicial != null)
        {
            botonMenuInicial.onClick.AddListener(VolverMenuInicial);
        }

        
        if (menuPausa != null) menuPausa.SetActive(false); 

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (enPausa)
            {
                Despausar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Pausar()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f; // congela el tiempo (todo para)
        enPausa = true;
    }

    public void Despausar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;  // timeScale maneja la velocidad del tiempo en el juego
        enPausa = false;
    }

    public void Reanudar()
    {
        Time.timeScale = 1f; // reaviva el tiempo 
        SceneManager.LoadScene("MenuInicial");
    }

    public void AbrirAjustes()
    {
        menuPausa.SetActive(false);

        if (instanciaAjustes != null)
        {
            instanciaAjustes.SetActive(true);
            instanciaAjustes.transform.SetAsLastSibling();
            return;
        }

        if (prefabAjustes != null && canvasPadre != null)
        {
            instanciaAjustes = Instantiate(prefabAjustes, canvasPadre);
            instanciaAjustes.transform.SetAsLastSibling();

            Ajustes scriptAjustes = instanciaAjustes.GetComponent<Ajustes>();
            if (scriptAjustes != null)
            {
                scriptAjustes.ConfigurarMenuPausa(this);
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
        menuPausa.SetActive(true);
    }

    public void VolverMenuInicial()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MenuInicial");
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }


}
