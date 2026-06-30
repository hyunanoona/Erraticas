using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    ╭━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╮
      Spawn
        Se encarga de spawnear los items (quesos y obstaculos) en el escenario. 
        Se puede configurar los items a spawnear desde el inspector.
    ╰━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╯
*/

public class Spawn : MonoBehaviour
{

    //  •• <<────────────────≪•◦ Variables ◦•≫────────────────>> ••

    [Header("Prefabs de Items")]
    public GameObject[] prefabsItems;

    [Header("Configuración de Posición (Lluvia)")]
    public float puntoYSpawn = 6f;

    // Limites para la posición X de spawn
    public float limiteXMinimo = -10f;
    public float limiteXMaximo = 10f;

    [Header("Configuración de Tiempos")]
    public float tiempoInicial = 2f;
    public float intervaloSpawn = 1.5f;


    //  •• <<────────────────≪•◦ Tiempo ◦•≫────────────────>> ••

    private void Start()
    {
        InvokeRepeating(nameof(SpawnearItem), tiempoInicial, intervaloSpawn);
    }

    //  •• <<────────────────≪•◦ Spawn ◦•≫────────────────>> ••
    protected virtual void SpawnearItem()
    {

        // Se elije un item al azar
        int indiceAleatorio = Random.Range(0, prefabsItems.Length);
        GameObject itemElegido = prefabsItems[indiceAleatorio];

        // Se calcula una posicion X aleatoria dentro del rango determinado
        float posicionXAleatoria = Random.Range(limiteXMinimo, limiteXMaximo);

        // Se crea el vector de posicion final 
        Vector3 posicionSpawn = new Vector3(posicionXAleatoria, puntoYSpawn, 0f);

        // Se instancia el item en el escenario
        Instantiate(itemElegido, posicionSpawn, Quaternion.identity);
    }
}
