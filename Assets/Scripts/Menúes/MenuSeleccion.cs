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
    [SerializeField] private Button jugar;
    private bool selecP1Confirmada = false;
    private bool selecP2Confirmada = false;

    void Start()
    {

        if (confirmarSeleccionInicial != null) confirmarSeleccionInicial.onClick.AddListener(ConfirmarSeleccion);
        if (confirmarSelecP1 != null) confirmarSelecP1.onClick.AddListener(ConfirmarSelecP1);
        if (confirmarSelecP2 != null) confirmarSelecP2.onClick.AddListener(ConfirmarSelecP2);
        if (jugar != null) jugar.onClick.AddListener(Jugar);
        if(primerVisual != null) primerVisual.SetActive(true);
        if (segundaVisual != null) segundaVisual.SetActive(false);
        if (tercerVisual != null) tercerVisual.SetActive(false);
    }

    void ConfirmarSeleccion()
    {
        if (primerVisual != null) primerVisual.SetActive(false);
        if (segundaVisual != null) segundaVisual.SetActive(true);
    }

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

    void Jugar()
    {
        SceneManager.LoadScene("NivelUno");
    }

}
