using UnityEngine;

public abstract class HabilidadBase : MonoBehaviour
{
    // [Header("Configuración de la Habilidad")]
    // aca irian las cosas relacionadas a los quesos que tiene que juntar para usar la habilidad.

    public abstract void Ejecutar(GameObject usuario, JugadorController aliadoCazador); // pasamos la info del usuario/soporte y del cazador para poder aplicarle los buffs
}