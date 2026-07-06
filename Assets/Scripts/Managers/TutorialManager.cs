using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("⊹₊˚‧︵‿₊୨ Lista de descripciones ୧₊‿︵‧˚₊⊹ ")]
    [SerializeField] private List<GameObject> paginasTutorial;
    [Header("⊹₊˚‧︵‿₊୨ Componentes UI ୧₊‿︵‧˚₊⊹")]
    [SerializeField] private Button botonIzquierdo;
    [SerializeField] private Button botonDerecho;

    private int indiceActual = 0;

    private void Start()
    {
        if (botonIzquierdo != null) botonIzquierdo.onClick.AddListener(PaginaAnterior);
        if (botonDerecho != null) botonDerecho.onClick.AddListener(PaginaSiguiente);
        ActualizarTutorial();
    }

    public void PaginaSiguiente()
    {
        if (indiceActual < paginasTutorial.Count - 1)
        {
            indiceActual++;
            ActualizarTutorial();
        }
    }

    public void PaginaAnterior()
    {
        if (indiceActual > 0)
        {
            indiceActual--;
            ActualizarTutorial();
        }
    }

    private void ActualizarTutorial()
    {
        if (paginasTutorial == null || paginasTutorial.Count == 0) return;
        for (int i = 0; i < paginasTutorial.Count; i++)
        {
            if(paginasTutorial[i] != null) paginasTutorial[i].SetActive(i == indiceActual);
        }
        if (botonIzquierdo != null) botonIzquierdo.interactable = indiceActual > 0;
        if (botonDerecho != null) botonDerecho.interactable = indiceActual < paginasTutorial.Count - 1; 
    }
}
