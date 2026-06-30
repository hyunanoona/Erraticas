using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    ╭━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╮
      SpawnMulti
        Clase hija del script de "Spawn", pero permite definir múltiples alturas (Y) para 
        adaptarse al nivel 3 que cuenta con estantes.
    ╰━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╯
*/
public class SpawnMulti : Spawn 
{
    //  •• <<────────────────≪•◦ Variable ◦•≫────────────────>> ••

    [Header("Configuración Multinivel")]
    public float[] alturasYSpawn;


    //  •• <<────────────────≪•◦ Spawn ◦•≫────────────────>> ••
    protected override void SpawnearItem()
    {
        // Se elije un item al azar
        int indiceAleatorio = Random.Range(0, prefabsItems.Length);
        GameObject itemElegido = prefabsItems[indiceAleatorio];

        // Se calcula una posicion X aleatoria dentro del rango determinado
        float posicionXAleatoria = Random.Range(limiteXMinimo, limiteXMaximo);

        // Se elije una altura de la lista al azar
        int indiceAltura = Random.Range(0, alturasYSpawn.Length);
        float alturaElegida = alturasYSpawn[indiceAltura];

        // El item se instancia en la nueva posicion
        Vector3 posicionSpawn = new Vector3(posicionXAleatoria, alturaElegida, 0f);
        Instantiate(itemElegido, posicionSpawn, Quaternion.identity);
    }
}
