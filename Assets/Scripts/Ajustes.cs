using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Ajustes : MonoBehaviour
{
    [Header(". ݁₊ ⊹ . ݁ Componentes de UI  ݁ . ⊹ ₊ ݁.")]
    [SerializeField] private Slider vol_SFX;
    [SerializeField] private Slider vol_Musica;
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private TMP_Dropdown graficos_Dropdown;
    [SerializeField] private Button botonVolver;
    [SerializeField] private AudioMixer audioMixer;  // CONFIGURAR EL AUDIOMIXER EN EL PROJECT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    void Start()
    {
        if(fullScreenToggle != null)
        {
            fullScreenToggle.isOn = Screen.fullScreen;
            fullScreenToggle.onValueChanged.AddListener(SetPantallaCompleta);
        }

        if (graficos_Dropdown != null)
        {
            graficos_Dropdown.value = QualitySettings.GetQualityLevel();
            graficos_Dropdown.onValueChanged.AddListener(SetCalidadGrafica);
        }

        if(audioMixer != null)
        {
            if (audioMixer.GetFloat("MusicaVol", out float volMusica))
            {
                vol_Musica.value = Mathf.Pow(10, volMusica / 20);
            }
            if(audioMixer.GetFloat("SFXVol", out float volSFX))
            {
                vol_SFX.value = Mathf.Pow(10, volSFX / 20);
            }
        }

        if(vol_Musica != null)
        {
            vol_Musica.onValueChanged.AddListener(SetVolumenMusica);
        }

        if(vol_SFX != null)
        {
            vol_SFX.onValueChanged.AddListener(SetVolumenSFX);
        }

        if (botonVolver != null)
        {
            botonVolver.onClick.AddListener(Volver);
        }
    }

    public void SetPantallaCompleta(bool esCompleta)
    {
        Screen.fullScreen = esCompleta;
        Debug.Log("Pantalla Completa: " + esCompleta);
    }

    public void SetCalidadGrafica(int indiceCalidad)
    {
        QualitySettings.SetQualityLevel(indiceCalidad);
        Debug.Log("Calidad Gráfica: " + indiceCalidad);
    }

    public void SetVolumenMusica(float valor)
    {
        float decibelios = Mathf.Log10(Mathf.Max(valor, 0.0001f)) * 20;
        audioMixer.SetFloat("MusicaVol", decibelios);
        Debug.Log("Volumen Música: " + valor);
    }

    public void SetVolumenSFX(float valor)
    {
        float decibelios = Mathf.Log10(Mathf.Max(valor, 0.0001f)) * 20;  // de esta forma transformé el valor del slider a decibelios
        audioMixer.SetFloat("SFXVol", decibelios);
        Debug.Log("Volumen SFX: " + valor);
    }

    public void Volver()
    {
        gameObject.SetActive(false);
    }

}
