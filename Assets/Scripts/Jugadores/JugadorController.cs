using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Este script es responsable de controlar al jugador, es decir, manejar su movimiento, sus animaciones, sus interacciones con el entorno, etc. 
    Basicamente es el cerebro, habra otros scripts que se encargaran de otras funcionalidades del jugador, como su salud, sus habilidades segun rol, etc.
*/

public class JugadorController : MonoBehaviour
{
    // Datos del PJ //
    private Rigidbody2D rb; // el componente de fisica del pj
    private InputJugador input; // el script que detecta las entradas del jugador
    private DatosPersonaje datos; // aca estan los datos del pj

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // el rigidbody o sea la parte fisica
        input = GetComponent<InputJugador>(); //accede al input 
        datos = GetComponent<DatosPersonaje>(); // accede a los datos del personaje

        rb.gravityScale = datos.EscalaGravedad; // ajusta la gravedad segun el pj
    }

    void Update()
    {
        // if (input.InputHabilidad) 
        // {
        //     datos.ActivarHabilidad(); // llamara a la futura habilidad (crear)
        // }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(input.MovimientoX * datos.Velocidad, rb.velocity.y); // mueve al jugador horizontalmente segun el input y la velocidad del pj

        if (input.Salto)
        {
            rb.velocity = new Vector2(rb.velocity.x, datos.FuerzaSalto); // hace que el jugador salte segun la fuerza de salto del pj
            input.ConsumirSalto();
        }
    }
}
