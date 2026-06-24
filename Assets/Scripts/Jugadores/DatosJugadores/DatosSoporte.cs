using UnityEngine;

/*
    Este script es responsable de almacenar los datos del personaje, como su velocidad, su fuerza de salto, su escala de gravedad, etc. 
    Es una clase base que sera heredada por los datos especificos de cada rol (soporte y cazador), asi cada rol puede tener sus propios valores y habilidades sin afectar al otro.
*/
public class DatosSoporte : DatosPersonaje
{
    private JugadorController cazadorAliado;
    
    //habilidades del soporte
    private HabilidadBase hab1;
    private HabilidadBase hab2;

    void Awake()
    {
        // datos especificos del soporte
        Velocidad = 6f;
        FuerzaSalto = 12f;
        EscalaGravedad = 2.7f;
    }

    void Start()
    {
        GameObject cazador = GameObject.FindWithTag("Cazador"); // buscamos el objeto del cazador aliado en la escena

        if (cazador != null)
        {
            cazadorAliado = cazador.GetComponent<JugadorController>(); // obtenemos el componente del cazador aliado
        }

        HabilidadBase[] habilidades = GetComponents<HabilidadBase>(); // obtenemos todas las habilidades del soporte

        if (habilidades.Length > 0)
        {
            hab1 = habilidades[0]; // asignamos la primera habilidad a hab1
            hab2 = habilidades[1]; // asignamos la segunda habilidad a hab2
        }
    }

    public void PresionoHabilidad1()
    {
        if (hab1 != null)
        {
            hab1.Ejecutar(gameObject, cazadorAliado); // ejecutamos la habilidad pasando la info del soporte y del cazador aliado
        }
    }

    public void PresionoHabilidad2()
    {
        if (hab2 != null)
        {
            hab2.Ejecutar(gameObject, cazadorAliado); // ejecutamos la habilidad pasando la info del soporte y del cazador aliado
        }
    }
}