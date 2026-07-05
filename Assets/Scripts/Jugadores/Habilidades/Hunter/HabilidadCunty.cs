using UnityEngine;


public class HabilidadCunty : HabilidadBase
{
    [Header("Ajustes de la pasiva")]
    [SerializeField] private float tiempoExtra = 15f; // segs que suma la cunty al cronometro del juego


    public override void Ejecutar(GameObject usuario, JugadorController aliadoCazador)
    {
        if (GameManager.Instance != null) // si no es nulo la instancia del GameManager
        {
            GameManager.Instance.SumarTiempoExtra(tiempoExtra); // llama al metodo del GameManager para sumar el tiempo extra al cronometro del juego 
            print($"Habilidad Cunty ejecutada por {usuario.name}. Tiempo extra sumado: {tiempoExtra} segundos."); // imprime en consola que la habilidad fue ejecutada y el tiempo extra sumado
        }
    }
}
