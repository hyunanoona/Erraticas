using UnityEngine;

/*
    Este script es responsable de almacenar los datos del personaje, como su velocidad, su fuerza de salto, su escala de gravedad, etc. 
    Es una clase base que sera heredada por los datos especificos de cada rol (soporte y cazador), asi cada rol puede tener sus propios valores y habilidades sin afectar al otro.
*/
public class DatosCazador : DatosPersonaje
{
    // contadores de unidades fisicas de queso para el cazador
    public int QuesosPategras { get; private set; } = 0; 
    public int PuntajeDelQueso { get; private set; } = 0;
    void Awake()
    {
        // datos especificos del cazador
        Velocidad = 10f;
        FuerzaSalto = 18f;
        EscalaGravedad = 3.5f;
    }

    public void ActualizarPuntosBono(int puntos)
    {
        PuntajeDelQueso = puntos;
    }

    // metodo para obtener la cantidad de quesos especifica del cazador
    public override int ObtenerCantidadQueso(string tipoDeQueso)
    {
        if (tipoDeQueso == "Pategras") return QuesosPategras;
        return 0;
    }

    // metodo que carga los quesos de a 1 unidad del
    public override void AgregarQueso(string tipoDeQueso)
    {
        if (tipoDeQueso == "Pategras")
        {
            QuesosPategras++;
            print($"Cazador sumó {tipoDeQueso}! Total: {QuesosPategras}");

            HabilidadFaso habilidad = GetComponent<HabilidadFaso>();
        
            if (habilidad != null && habilidad.PuedeEjecutar(this))
            {
                JugadorController controller = GetComponent<JugadorController>();
                habilidad.Ejecutar(gameObject, controller);
            }
        }
    }

    // metodo para restar las 4 unidades de queso gastadas por la pasiva
    public override void RestarQuesos(string tipoDeQueso, int cantidad)
    {
        if (tipoDeQueso == "Pategras") QuesosPategras -= cantidad;
    }
}