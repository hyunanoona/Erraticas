using UnityEngine;

public abstract class HabilidadBase : MonoBehaviour
{
    [Header("Configuración de la Habilidad")]
    public string nombreHabilidad;
    public float cooldown = 5f;

    public abstract void Ejecutar(GameObject usuario, JugadorController aliadoCazador); // pasamos la info del usuario/soporte y del cazador para poder aplicarle los buffs
}