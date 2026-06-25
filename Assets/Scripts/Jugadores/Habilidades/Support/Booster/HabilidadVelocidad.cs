using UnityEngine;

public class HabilidadVelocidad : HabilidadBase
{
    [Header("Ajustes de Velocidad")]
    [SerializeField] private float duracionBuff = 5f; // tiempo que dura el buff, se puede modificar igual en el inspector

    public override void Ejecutar(GameObject usuario, JugadorController aliadoCazador)
    {
        DatosSoporte datos = usuario.GetComponent<DatosSoporte>();

        if (aliadoCazador != null && datos != null)
        {
            GastarQuesos(datos); // cobra los quesos correspondientes al tipo de habilidad

            aliadoCazador.ActivarBuffVelocidad(duracionBuff); // activa el buff de velocidad en el cazador aliado por la duracion especificada
            print($"{usuario.name} le dio velocidad a {aliadoCazador.gameObject.name} por {duracionBuff}s!");
        }
    }
}