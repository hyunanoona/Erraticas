using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UI_Soporte : MonoBehaviour
{
    [Header("⁺‧₊˚ ཐི⋆ Componentes ⋆ཋྀ ˚₊‧⁺")]
    [SerializeField] private Image iconoHabilidad1;
    [SerializeField] private Image iconoHabilidad2;
    [SerializeField] private GameObject contenedorSilencio; 

    private Coroutine corrutinaSilencio;

    void Awake()
    {
        if (contenedorSilencio != null) contenedorSilencio.gameObject.SetActive(false);
    }

    public void ActivarCoolDownSilencio(float duracion)
    {
        if (corrutinaSilencio != null)
        {
            StopCoroutine(corrutinaSilencio);
        }
        corrutinaSilencio = StartCoroutine(RutinaRelojSilencio(duracion));
    }

    private IEnumerator RutinaRelojSilencio(float duracion)
    {
        if (contenedorSilencio == null) yield break;
        contenedorSilencio.gameObject.SetActive(true);
        
        float tiempoPasado = 0f;

        while (tiempoPasado < duracion)
        {
            tiempoPasado += Time.deltaTime;
            yield return null;
        }

        contenedorSilencio.gameObject.SetActive(false);

    }

}
