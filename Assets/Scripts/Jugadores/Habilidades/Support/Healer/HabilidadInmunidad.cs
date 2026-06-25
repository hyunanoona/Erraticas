using UnityEngine;

public class HabilidadInmunidad : HabilidadBase
{
    [Header("Ajustes de Inmunidad")]
    [SerializeField] private float duracion = 3f; // tiempo que dura el buff, se puede modificar igual en el inspector

    public override void Ejecutar(GameObject usuario, JugadorController aliadoCazador)
    {
        if (aliadoCazador == null) return;

        Health healthCazador = aliadoCazador.GetComponent<Health>();
        DatosSoporte datos = usuario.GetComponent<DatosSoporte>();

        if (healthCazador != null)
        {
            GastarQuesos(datos); // cobra los quesos correspondientes al tipo de habilidad
            
            healthCazador.ActivarInmunidadTemporal(duracion); // activamos la inmunidad temporal del cazador aliado
            print($"{usuario.name} ha activado inmunidad temporal en {aliadoCazador.name} por {duracion} segundos.");
        }
    }
}