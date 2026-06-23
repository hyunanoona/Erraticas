using UnityEngine;

public class HabilidadVelocidad : HabilidadBase
{
    [Header("Ajustes de Velocidad")]
    [SerializeField] private float duracionBuff = 5f; // tiempo que dura el buff, se puede modificar igual en el inspector

    public override void Ejecutar(GameObject usuario, JugadorController aliadoCazador)
    {
        if (aliadoCazador != null)
        {
            aliadoCazador.ActivarBuffVelocidad(duracionBuff); // activamos el buff de velocidad en el aliado cazador
            print($"{usuario.name} le dio velocidad a {aliadoCazador.gameObject.name} por {duracionBuff}s!");
        }
    }
}