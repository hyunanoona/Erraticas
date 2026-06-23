using UnityEngine;

public class HabilidadDobleSalto : HabilidadBase
{
    [Header("Ajustes de la Habilidad")]
    [SerializeField] private float duracionBuff = 5f; // tiempo que dura el buff, se puede modificar igual en el inspector

    public override void Ejecutar(GameObject usuario, JugadorController aliadoCazador)
    {
        if (aliadoCazador != null)
        {
            aliadoCazador.ActivarBuffDobleSalto(duracionBuff); // activamos el buff de doble salto en el aliado cazador
            print($"{usuario.name} potenció a {aliadoCazador.gameObject.name} con Doble Salto por {duracionBuff}s!");
        }
    }
}