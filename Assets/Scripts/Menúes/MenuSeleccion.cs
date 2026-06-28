using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class DetalleTipoRol
{
    public string nombreTipo;
    public Sprite imagenRataMenu;
    [TextArea(2, 4)] // cuadro cómodo para escribir desde el inspector
    public string descripcionTipo;
}

public class MenuSeleccion : MonoBehaviour
{
[Header(". ݁₊ ⊹ . ݁ Referencias y Variables  ݁ . ⊹ ₊ ݁.")]

    [SerializeField] private GameObject primerVisual;
    [SerializeField] private GameObject segundaVisual;
    [SerializeField] private GameObject tercerVisual;
    [Header("⊹ ࣪ ˖ Botones ⊹ ࣪ ˖")]
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

    [Header("────୨ Componentes de Imagen UI ৎ────")]
    [SerializeField] private Image p1FotoUI;
    [SerializeField] private Image p2FotoUI;

    [Header("────୨ Textos TMP ৎ────")]

    [SerializeField] private TextMeshProUGUI p1Rol;
    [SerializeField] private TextMeshProUGUI p2Rol;
    [SerializeField] private TextMeshProUGUI p1TipoTexto;
    [SerializeField] private TextMeshProUGUI p2TipoTexto;
    [SerializeField] private TextMeshProUGUI p1DescripcionTexto; 
    [SerializeField] private TextMeshProUGUI p2DescripcionTexto;

    [Header(". ݁₊ ⊹ . ݁  ݁Datos de Selección . ⊹ ₊ ݁.")]

    public string ratita1Rol = "Cazador";
    public string ratita2Rol = "Soporte";
    public string ratita1TipoRol;
    public string ratita2TipoRol;
    private bool selecP1Confirmada = false;
    private bool selecP2Confirmada = false;
    [SerializeField] private List<DetalleTipoRol> opcionesCazador;  // CONFIGURAR DESDE INSPECTOR
    [SerializeField] private List<DetalleTipoRol> opcionesSoporte;

    // Listas: reflejos e índices ˖ ࣪⊹
    private List<DetalleTipoRol> listaActualP1;
    private List<DetalleTipoRol> listaActualP2;
    private int indiceP1 = 0;
    private int indiceP2 = 0;



    void Start()
    { 
        // Listeners de Botones ⋆˚࿔⋆˚࿔
        if (confirmarSeleccionInicial != null) confirmarSeleccionInicial.onClick.AddListener(ConfirmarSeleccionInicial);
        if (siguienteSeleccionInicialDer != null) siguienteSeleccionInicialDer.onClick.AddListener(SiguienteSeleccionInicial);
        if (siguienteSeleccionInicialIzq != null) siguienteSeleccionInicialIzq.onClick.AddListener(SiguienteSeleccionInicial);
        
        // Listeners P1 (Der/Izq modifican el índice) ⋆˚࿔⋆˚࿔
        if (siguienteSelecP1Der != null) siguienteSelecP1Der.onClick.AddListener(() => CambiarIndiceP1(1));
        if (siguienteSelecP1Izq != null) siguienteSelecP1Izq.onClick.AddListener(() => CambiarIndiceP1(-1));
        
        // Listeners P2 (Der/Izq modifican el índice) ⋆˚࿔⋆˚࿔ 
        if (siguienteSelecP2Der != null) siguienteSelecP2Der.onClick.AddListener(() => CambiarIndiceP2(1));
        if (siguienteSelecP2Izq != null) siguienteSelecP2Izq.onClick.AddListener(() => CambiarIndiceP2(-1));
        
        if (confirmarSelecP1 != null) confirmarSelecP1.onClick.AddListener(ConfirmarSelecP1);
        if (confirmarSelecP2 != null) confirmarSelecP2.onClick.AddListener(ConfirmarSelecP2);
        if (jugar != null) jugar.onClick.AddListener(Jugar);

        // Estado inicial ⋆˚࿔⋆˚࿔
        if (primerVisual != null) primerVisual.SetActive(true);
        if (segundaVisual != null) segundaVisual.SetActive(false);
        if (tercerVisual != null) tercerVisual.SetActive(false);

        ActualizarTextoRol();
    }

    void Update()
    {
        if (primerVisual.activeSelf)
                {
                    TocoSiguienteSeleccionInicial();
                }
                else if (segundaVisual.activeSelf)
                {
                    TocoSiguienteSeleccionP1();
                    TocoSiguienteSeleccionP2();
                }
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

        listaActualP1 = (ratita1Rol == "Cazador") ? opcionesCazador : opcionesSoporte; // pregunta si es V -> opcionesCazador, else opcionesSoporte
        listaActualP2 = (ratita2Rol == "Cazador") ? opcionesCazador : opcionesSoporte;

        indiceP1 = 0;
        indiceP2 = 0;
        ActualizarTextoTipoP1();
        ActualizarTextoTipoP2();

        GameManager.Instance.RolJugadores(ratita1Rol, ratita2Rol);
    }

//                   . ݁₊ ⊹ . ݁ Segunda visual, selección del tipo de rol ݁ . ⊹ ₊ ݁.

    void TocoSiguienteSeleccionP1()
    {
        if (selecP1Confirmada) return; 
        if (Input.GetKeyDown(KeyCode.A)) CambiarIndiceP1(-1);
        if (Input.GetKeyDown(KeyCode.D)) CambiarIndiceP1(1);
    }

    void TocoSiguienteSeleccionP2()
    {
        if (selecP2Confirmada) return;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) CambiarIndiceP2(-1);
        if (Input.GetKeyDown(KeyCode.RightArrow)) CambiarIndiceP2(1);
    }

    void CambiarIndiceP1(int direccion)
    {
        if (selecP1Confirmada || listaActualP1 == null) return;

        indiceP1 += direccion;
        if (indiceP1 < 0) indiceP1 = listaActualP1.Count - 1; // Bucle si va hacia atras
        if (indiceP1 >= listaActualP1.Count) indiceP1 = 0;    // Bucle si va hacia adelante

        ActualizarTextoTipoP1();
    }

    void CambiarIndiceP2(int direccion)
    {
        if (selecP2Confirmada || listaActualP2 == null) return;
        indiceP2 += direccion;
        if (indiceP2 < 0) indiceP2 = listaActualP2.Count - 1;
        if (indiceP2 >= listaActualP2.Count) indiceP2 = 0;

        ActualizarTextoTipoP2();
    }

    void ActualizarTextoTipoP1()
    {
        if (listaActualP1 != null && listaActualP1.Count > 0)
            {
                DetalleTipoRol seleccion = listaActualP1[indiceP1];
                if (p1TipoTexto != null) p1TipoTexto.text = seleccion.nombreTipo; // seteo el nombre y la descrip. 
                if (p1DescripcionTexto != null) p1DescripcionTexto.text = seleccion.descripcionTipo;
                if (p1FotoUI != null && seleccion.imagenRataMenu != null)
                {
                    p1FotoUI.sprite = seleccion.imagenRataMenu;
                }
            }
    }
void ActualizarTextoTipoP2()
    {
        if (listaActualP2 != null && listaActualP2.Count > 0)
            {
                DetalleTipoRol seleccion = listaActualP2[indiceP2];
                if (p2TipoTexto != null) p2TipoTexto.text = seleccion.nombreTipo;
                if (p2DescripcionTexto != null) p2DescripcionTexto.text = seleccion.descripcionTipo;
                if (p2FotoUI != null && seleccion.imagenRataMenu != null)
                {
                    p2FotoUI.sprite = seleccion.imagenRataMenu;
                }
            }
    }

    void ConfirmarSelecP1()
    {
        selecP1Confirmada = true;
        ratita1TipoRol = listaActualP1[indiceP1].nombreTipo; // guardo el nombre para darselo al gameManager tipo string
        VerificarConfirmaciones();
    }

    void ConfirmarSelecP2()
    {
        selecP2Confirmada = true;
        ratita2TipoRol = listaActualP2[indiceP2].nombreTipo;
        VerificarConfirmaciones();
    }

    void VerificarConfirmaciones()
    {
        if (selecP1Confirmada && selecP2Confirmada)
        {
            GameManager.Instance.GuardarSeleccionMenu(ratita1TipoRol, ratita2TipoRol);
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
