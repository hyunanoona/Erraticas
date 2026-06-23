using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSeleccion : MonoBehaviour
{
[Header(". ݁₊ ⊹ . ݁ Referencias y Variables  ݁ . ⊹ ₊ ݁.")]

    [SerializeField] private GameObject primerVisual;
    [SerializeField] private GameObject segundaVisual;
    [SerializeField] private GameObject tercerVisual;
    [SerializeField] private Button confirmarSeleccionInicial; 
    [SerializeField] private Button confirmarSelecP1;
    [SerializeField] private Button confirmarSelecP2;
    [SerializeField] private Button siguienteSeleccionInicialDer;
    [SerializeField] private Button siguienteSeleccionInicialIzq;
    [SerializeField] private Button siguienteSelecP1Der;
    [SerializeField] private Button siguienteSelecP1Izq;
    [SerializeField] private Button siguienteSelecP2Der;
    [SerializeField] private Button siguienteSelecP2Izq;
    [SerializeField] private Button jugar;
    [SerializeField] private TextMeshProUGUI p1Rol;
    [SerializeField] private TextMeshProUGUI p2Rol;
    public string ratita1Rol = "Cazador";
    public string ratita2Rol = "Soporte";
    public string ratita1TipoRol;
    public string ratita2TipoRol;
    private bool selecP1Confirmada = false;
    private bool selecP2Confirmada = false;

    void Start()
    {

        if (confirmarSeleccionInicial != null) confirmarSeleccionInicial.onClick.AddListener(ConfirmarSeleccionInicial);
        if (siguienteSeleccionInicialDer != null) siguienteSeleccionInicialDer.onClick.AddListener(SiguienteSeleccionInicial);
        if (siguienteSeleccionInicialIzq != null) siguienteSeleccionInicialIzq.onClick.AddListener(SiguienteSeleccionInicial);
        /*
                if (siguienteSelecP1Der != null) siguienteSelecP1Der.onClick.AddListener(SiguienteSelecP1);
        if (siguienteSelecP1Izq != null) siguienteSelecP1Izq.onClick.AddListener(SiguienteSelecP1);
        if (siguienteSelecP2Der != null) siguienteSelecP2Der.onClick.AddListener(SiguienteSelecP2);
        if (siguienteSelecP2Izq != null) siguienteSelecP2Izq.onClick.AddListener(SiguienteSelecP2);
        */

        if (confirmarSelecP1 != null) confirmarSelecP1.onClick.AddListener(ConfirmarSelecP1);
        if (confirmarSelecP2 != null) confirmarSelecP2.onClick.AddListener(ConfirmarSelecP2);
        if (jugar != null) jugar.onClick.AddListener(Jugar);
        if(primerVisual != null) primerVisual.SetActive(true);
        if (segundaVisual != null) segundaVisual.SetActive(false);
        if (tercerVisual != null) tercerVisual.SetActive(false);
    }

    void Update()
    {
        TocoSiguienteSeleccionInicial();
        /*
                TocoSiguienteSeleccionP1();
        TocoSiguienteSeleccionP2();
        */

    }

//                   . ݁₊ ⊹ . ݁ Primera visual, selección inicial del rol ݁ . ⊹ ₊ ݁.

    void TocoSiguienteSeleccionInicial()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            SiguienteSeleccionInicial();
        }
    }



    void SiguienteSeleccionInicial()
    {
        if (ratita1Rol == "Cazador")
        {
            ratita1Rol = "Soporte";
            ratita2Rol = "Cazador";
        }
        else 
        {
            ratita1Rol = "Cazador";
            ratita2Rol = "Soporte";
        }

        ActualizarTextoRol();
    }

    void ActualizarTextoRol()
    {
        if (p1Rol != null && p2Rol != null)
        {
            p1Rol.text = ratita1Rol;
            p2Rol.text = ratita2Rol;
        }
    }

    void ConfirmarSeleccionInicial()
    {
        if (primerVisual != null) primerVisual.SetActive(false);
        if (segundaVisual != null) segundaVisual.SetActive(true);
        GameManager.Instance.RolJugadores(ratita1Rol, ratita2Rol);
    }

//                   . ݁₊ ⊹ . ݁ Segunda visual, selección del tipo de rol ݁ . ⊹ ₊ ݁.


/*

    void TocoSiguienteSeleccionP1()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            SiguienteSelecP1();
        }
    }

    void TocoSiguienteSeleccionP2()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            SiguienteSelecP2();
        }
    }
    void SiguienteSelecP1()
    {
        if (ratita1Rol == "Cazador")
        {
            PasarASiguienteDelTipoCazadorP1();
        }
        else if (ratita1Rol == "Soporte")
        {
            PasarASiguienteDelTipoSoporteP1();
        }
    }

    void SiguienteSelecP2()
    {
        if (ratita1Rol == "Cazador")
        {
            PasarASiguienteDelTipoCazadorP2();
        }
        else if (ratita1Rol == "Soporte")
        {
            PasarASiguienteDelTipoSoporteP2();
        }
    }

    PasarASiguienteDelTipoCazadorP1()
    {
        if (seleccionActualP1 == "Cunty")
        {
            
        }
    }

*/

    void ConfirmarSelecP1()
    {
        selecP1Confirmada = true;
        VerificarConfirmaciones();
    }

    void ConfirmarSelecP2()
    {
        selecP2Confirmada = true;
        VerificarConfirmaciones();
    }

    void VerificarConfirmaciones()
    {
        if (selecP1Confirmada && selecP2Confirmada)
        {
            if (segundaVisual != null) segundaVisual.SetActive(false);
            if (tercerVisual != null) tercerVisual.SetActive(true);
        }
    }

    //                   . ݁₊ ⊹ . ݁ Tercera visual, comenzar el juego ݁ . ⊹ ₊ ݁.

    void Jugar()
    {
        SceneManager.LoadScene("NivelUno");
    }

}
