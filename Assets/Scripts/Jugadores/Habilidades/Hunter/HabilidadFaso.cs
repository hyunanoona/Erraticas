using UnityEngine;


public class HabilidadFaso : HabilidadBase
{
    /*
    [Header("Ajustes de la pasiva")]
    variables para ajustar la pasiva, como el tiempo de recarga, el daño, la duración del efecto, etc
    */


    public override void Ejecutar(GameObject usuario, JugadorController aliadoCazador)
    {
        //contendria la pasiva de spawnear un queso pategras cerca de ella al juntar 4 de este mismo queso
        DatosCazador datosCazador = usuario.GetComponent<DatosCazador>();
        if (datosCazador != null)
        {
            GastarQuesos(datosCazador); // gasta los quesos

            int puntosExtra = datosCazador.PuntajeDelQueso; // accede al valor de los quesos desde los datos del cazador

            if (GameManager.Instance != null)
            {
                GameManager.Instance.SumarPuntos(puntosExtra); //suma el puntaje del queso extra directamente al gamemanager
                print($"Pasiva faso activada, se consumieron 4 quesos y se sumó {puntosExtra}.");
            }
        }
    }
}
