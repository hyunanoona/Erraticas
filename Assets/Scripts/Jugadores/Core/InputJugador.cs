using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Este script es responsable de manejar las entradas del jugador, es decir, detectar cuando el jugador presiona una tecla o un botón y ejecutar la acción correspondiente.
*/

public class InputJugador : MonoBehaviour
{
    // Config de red local //
    public enum NumeroJugador { Jugador1, Jugador2 }
    public NumeroJugador jugadorAsignado = NumeroJugador.Jugador1;

    // Variables para almacenar el estado de las entradas //
    public float MovimientoX { get; private set; } // movimiento horizontal
    public bool Salto { get; private set; } // si el jugador ha intentado saltar en este fotograma

    void Update()
    {
        MovimientoX = 0f;

        if (jugadorAsignado == NumeroJugador.Jugador1)
        {
            // jugador 1: wasd + espacio
            if (Input.GetKey(KeyCode.D)) MovimientoX = 1f;
            if (Input.GetKey(KeyCode.A)) MovimientoX = -1f;

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                Salto = true;
            }
        }
        else if (jugadorAsignado == NumeroJugador.Jugador2)
        {
            // jugador 2: flechas + enter
            if (Input.GetKey(KeyCode.RightArrow)) MovimientoX = 1f;
            if (Input.GetKey(KeyCode.LeftArrow)) MovimientoX = -1f;

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Return))
            {
                Salto = true;
            }
        }
    }

    public void ConsumirSalto() // avisa al controlador que ya se salto
    {
        Salto = false;
    }
}