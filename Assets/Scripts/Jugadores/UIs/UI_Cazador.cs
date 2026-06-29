using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Cazador : MonoBehaviour
{
    [Header("⁺‧₊˚ ཐི⋆ Barra vida ⋆ཋྀ ˚₊‧⁺")]
    [SerializeField] private Slider barraVida;

    [Header("⁺‧₊˚ ཐི⋆ Componentes ⋆ཋྀ ˚₊‧⁺")]
    [SerializeField] private Image iconoPasiva;
    [SerializeField] GameObject iconoEscudoInmune;
    // Texto [SerializeField] private  contadorQuesos;
    
    void Awake()
    {
        iconoEscudoInmune.SetActive(false);
    }


    public void ActualizarVida(int actual, int max) // se encarga el UIManager
    {
        barraVida.value = (float)actual / max;
    }


    // CORRUTINA DEL TIEMPO ACTIVO ACA!!!!!!!!!!!!!!!!!!!!!!!!!:
    public void MostrarInmunidad(bool Activar, float tiempo)
    {
        iconoEscudoInmune.SetActive(true);
    }

    // public void ActualizarContadorQuesosPasiva(float cuantos) {}
}
