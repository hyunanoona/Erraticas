using UnityEngine;

public class HabilidadCuracion : HabilidadBase
{
    [Header("Ajustes de Curación")]
    [SerializeField] private int cantidadCurar = 20; // cantidad de vida que se restaurará al aliado cazador

    public override void Ejecutar(GameObject usuario, JugadorController aliadoCazador)
    {
        if (aliadoCazador == null) return;

        Health healthCazador = aliadoCazador.GetComponent<Health>();

        if (healthCazador != null)
        {
            healthCazador.Curar(cantidadCurar); // curamos al cazador aliado
            print($"{usuario.name} ha curado a {aliadoCazador.name} por {cantidadCurar} puntos de vida. Su vida actual es {healthCazador.health}"); 
        }
    }
}