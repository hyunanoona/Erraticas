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

public class Veneno : ClaseItem
    {
        public int danioVida = 10;
        protected override string TagPermitido => "Ambos";
        public Veneno() { nombreItem = "Veneno"; }
        protected override void AplicarEfectoCazador(DatosCazador cazador)
        {
            ReproducirSonidoInteractuar(); // reproduce el sonido de interaccion del item

            Health vidaCazador = cazador.GetComponent<Health>();
            if (vidaCazador != null)
            {
                vidaCazador.RecibirDanio(danioVida);
            }
        }

        protected override void AplicarEfectoSoporte(DatosSoporte soporte)
        {
            ReproducirSonidoInteractuar(); // reproduce el sonido de interaccion del item
            Health vidaSoporte = soporte.GetComponent<Health>();
            if (vidaSoporte != null)
            {
                vidaSoporte.RecibirDanio(danioVida);
            }

        }
    }

