using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    ╭━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╮
      Obstaculos ᝰ
        Script que contiene las clases hijas de todos los obstacuos (veneno y ratonera)
        Todos los obstaculos usan el TagPermitido => "Ambos" ya que afectan a cualquier raton.
    ╰━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╯
*/

//  •• <<───────────────────────≪•◦ Veneno ◦•≫───────────────────────>> ••

public class Veneno : ClaseItem
    {
        public int danioVida = 10;
        protected override string TagPermitido => "Ambos";
        public Veneno() { nombreItem = "Veneno"; }
        protected override void AplicarEfectoCazador(DatosCazador cazador)
        {
            //cazador.RestarVida(danioVida);
        }
        protected override void AplicarEfectoSoporte(DatosSoporte soporte)
        {
            //soporte.RestarVida(danioVida);
        }
    }


//  •• <<──────────────────────≪•◦ Ratonera ◦•≫──────────────────────>> ••

public class Ratonera : ClaseItem 
{
    public float tiempoInmovilizado = 5f;

    public int puntosQueResta = 5;
    protected override string TagPermitido => "Ambos";
    public Ratonera() { nombreItem = "Ratonera"; }

    protected override void AplicarEfectoCazador(DatosCazador cazador)
    {
        //Resta puntaje
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SumarPuntos(-puntosQueResta);
        }

        //cazador.Inmovilizar(tiempoInmovilizado);
    }
    protected override void AplicarEfectoSoporte(DatosSoporte soporte)
    {
        //Resta puntaje
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SumarPuntos(-puntosQueResta);
        }
    }

    //soporte.Inmovilizar(tiempoInmovilizado);
}
