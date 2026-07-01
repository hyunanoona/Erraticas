using UnityEngine;

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

        JugadorController controller = cazador.GetComponent<JugadorController>();
        if (controller != null)
        {
            controller.ActivarInmovilizado(tiempoInmovilizado);
        }
    }
    protected override void AplicarEfectoSoporte(DatosSoporte soporte)
    {
        //Resta puntaje
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SumarPuntos(-puntosQueResta);
        }

        JugadorController controller = soporte.GetComponent<JugadorController>();
        if (controller != null)
        {
            controller.ActivarInmovilizado(tiempoInmovilizado);
        }
    }
}