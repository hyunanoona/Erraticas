using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


// MENU INICIAL Y DE PAUSA SE PODRIAN UNIFICAR EN MENU SIMPLEMENTE; CON LA DIFERENCIA DEL TEXTO EN 
// JUGAR Y REANUDAR, HACER VERSATIL EL CAMBIO DE ESCENARIO. SEGUN EN QUÉ ESCENA TE ENCUENTRES TE LLEVA A UNA O LA OTRA. 
// ?? ?  pensarlo.

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

    void Update()
    {
        MenuInvisible();
    }

    public void Jugar()
    {
        SceneManager.LoadScene("NivelUno");
    }

    public void AbrirAjustes()
    {
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
            RectTransform rect = instanciaAjustes.GetComponent<RectTransform>();
            if(rect != null)
            {
                rect.localPosition = Vector3.zero;
                rect.localScale = Vector3.one;
            }

        }

    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }

    public void MenuInvisible()
    {
        if (instanciaAjustes.activeSelf)
        {
            botonJugar.gameObject.SetActive(false);
            botonAjustes.gameObject.SetActive(false);
            botonSalir.gameObject.SetActive(false);
            tituloTexto.gameObject.SetActive(false);
        }
        else
        {
            botonJugar.gameObject.SetActive(true);
            botonAjustes.gameObject.SetActive(true);
            botonSalir.gameObject.SetActive(true);
            tituloTexto.gameObject.SetActive(true);
        }
    }
}
