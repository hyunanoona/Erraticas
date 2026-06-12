using UnityEngine;

/*
    Este script es responsable de almacenar los datos del personaje, como su velocidad, su fuerza de salto, su escala de gravedad, etc. 
    Es una clase base que sera heredada por los datos especificos de cada rol (soporte y cazador), asi cada rol puede tener sus propios valores y habilidades sin afectar al otro.
*/
public class DatosCazador : DatosPersonaje
{
    void Awake()
    {
        // datos especificos del cazador
        Velocidad = 10f;
        FuerzaSalto = 10f;
        EscalaGravedad = 3.5f;
    }

    public override void ActivarHabilidad()
    {
        // override para hacer la habilidad del cazador
    }
}