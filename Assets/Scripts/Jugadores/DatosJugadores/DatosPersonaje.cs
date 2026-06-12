using UnityEngine;

/*
    Este script es responsable de almacenar los datos del personaje, como su velocidad, su fuerza de salto, su escala de gravedad, etc. 
    Es una clase base que sera heredada por los datos especificos de cada rol (soporte y cazador), asi cada rol puede tener sus propios valores y habilidades sin afectar al otro.
*/

public class DatosPersonaje : MonoBehaviour
{
    public float Velocidad { get; protected set; }
    public float FuerzaSalto { get; protected set; }
    public float EscalaGravedad { get; protected set; }

    public virtual void ActivarHabilidad()
    {
        // aca estaria la futura habilidad del pj (soporte o cazador
    }
}