using UnityEngine;

public class HabilidadInmunidad : HabilidadBase
{
    [Header("Ajustes de Inmunidad")]
    [SerializeField] private float duracion = 3f;

    public override void Ejecutar(GameObject usuario, JugadorController aliadoCazador)
    {
        if (aliadoCazador == null) return;

        Health healthAliado = aliadoCazador.GetComponent<Health>();

        if (healthAliado != null)
        {
            healthAliado.ActivarInmunidadTemporal(duracion); // activamos la inmunidad temporal del cazador aliado
            print($"{usuario.name} ha activado inmunidad temporal en {aliadoCazador.name} por {duracion} segundos.");
        }
    }
}