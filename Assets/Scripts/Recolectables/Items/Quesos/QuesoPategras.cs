using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    ╭━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╮
      Quesos  ᝰ
        Script que contiene las clases hijas de todos los quesos del juego. Cada clase hereda de
        ClaseItem, se define su tag permitido y la carga de la barra de habilidades
    ╰━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╯
*/

//  •• <<─────────────────────≪•◦ Queso - Cazador ◦•≫─────────────────────>> ••

public class QuesoPategras : ClaseItem 
{
    public int puntosQueOtorga = 20; // cantidad de puntos que se suman al puntaje del jugador al recolectar el queso, unico queso con puntaje
    protected override string TagPermitido => "Cazador"; // solo el cazador puede recolectar este queso
    public QuesoPategras() { nombreItem = "Queso Pategras"; }

    protected override void AplicarEfectoCazador(DatosCazador cazador)
    {
        // logica para que cuando el cazador recolecta el queso se suma al puntaje del jugador
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SumarPuntos(puntosQueOtorga);
        }

        cazador.ActualizarPuntosBono(puntosQueOtorga); // le pasa el puntaje al cazador

        cazador.AgregarQueso("Pategras"); // suma el queso para la pasiva
    }
}

/* 
//  •• <<────────────────≪•◦ Quesos - Soporte Healer ◦•≫────────────────>> ••

public class QuesoRoquefort : ClaseItem
{
    protected override string TagPermitido => "Soporte";
    public QuesoRoquefort() { nombreItem = "Queso Roquefort"; }

    protected override void AplicarEfectoSoporte(DatosSoporte soporte)
    {
        soporte.AgregarQueso("Roquefort"); // Mandamos el texto directo
    }
}

public class QuesoMozzarella : ClaseItem
{
    protected override string TagPermitido => "Soporte";
    public QuesoMozzarella() { nombreItem = "Queso Mozzarella"; }

    protected override void AplicarEfectoSoporte(DatosSoporte soporte)
    {
        soporte.AgregarQueso("Mozzarella");
    }
}


//  •• <<────────────────≪•◦ Quesos - Soporte Booster ◦•≫────────────────>> ••

public class QuesoProvoleta : ClaseItem
{
    protected override string TagPermitido => "Soporte";
    public QuesoProvoleta() { nombreItem = "Queso Provoleta"; }

    protected override void AplicarEfectoSoporte(DatosSoporte soporte)
    {
        soporte.AgregarQueso("Provoleta");
    }
}

public class QuesoCremoso : ClaseItem
{
    protected override string TagPermitido => "Soporte";
    public QuesoCremoso() { nombreItem = "Queso Cremoso"; }

    protected override void AplicarEfectoSoporte(DatosSoporte soporte)
    {
        soporte.AgregarQueso("Cremoso");
    }
}
*/