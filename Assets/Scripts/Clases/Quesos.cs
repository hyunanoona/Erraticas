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
    public int puntosQueOtorga = 20;
    protected override string TagPermitido => "Cazador";
    public QuesoPategras() { nombreItem = "Queso Pategras"; }

    protected override void AplicarEfectoCazador(DatosCazador cazador)
    {
        // Puntaje 
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SumarPuntos(puntosQueOtorga);
        }
    }
}


//  •• <<────────────────≪•◦ Quesos - Soporte Healer ◦•≫────────────────>> ••

public class QuesoRoquefort : ClaseItem
{
    protected override string TagPermitido => "Soporte";
    public QuesoRoquefort() { nombreItem = "Queso Roquefort"; }

    protected override void AplicarEfectoSoporte(DatosSoporte soporte)
    {
        //soporte.CargarInmunidad(puntosDeCarga);
    }
}

public class QuesoMozzarella : ClaseItem
{
    protected override string TagPermitido => "Soporte";

    public QuesoMozzarella() { nombreItem = "Queso Mozzarella"; }

    protected override void AplicarEfectoSoporte(DatosSoporte soporte)
    {
        //soporte.CargarCuracion(puntosDeCarga);
    }
}


//  •• <<────────────────≪•◦ Quesos - Soporte Booster ◦•≫────────────────>> ••

public class QuesoProvoleta : ClaseItem
{
    protected override string TagPermitido => "Soporte";

    public QuesoProvoleta() { nombreItem = "Queso Provoleta"; }

    protected override void AplicarEfectoSoporte(DatosSoporte soporte)
    {
        //soporte.CargarVelocidad(puntosDeCarga);
    }
}

public class QuesoCremoso : ClaseItem
{
    protected override string TagPermitido => "Soporte";

    public QuesoCremoso() { nombreItem = "Queso Cremoso"; }

    protected override void AplicarEfectoSoporte(DatosSoporte soporte)
    {
        //soporte.CargarDobleSalto(puntosDeCarga);
    }
}