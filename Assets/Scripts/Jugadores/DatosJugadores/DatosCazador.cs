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

    // UI de los pjs
    private int quesosParaHabilidad = 4; // De prueba por el momento 
    private UI_Cazador uiAsociada;       // Se guarda la UI asignada a este clon del cazador

    void Awake()
    {
        // datos especificos del cazador
        Velocidad = 10f;
        FuerzaSalto = 18f;
        EscalaGravedad = 3.5f;
    }

    // metodo para asignar la UI del cazador a este script
    public void AsignarUI(UI_Cazador uiCazador)
    {
        uiAsociada = uiCazador;
        ActualizarVisualizacionBarra(); 
    }

    public void ActualizarPuntosBono(int puntos)
    {
        PuntajeDelQueso = puntos;
    }

    // metodo para obtener la cantidad de quesos especifica del cazador
    public override int ObtenerCantidadQueso(string tipoDeQueso)
    {
        if (tipoDeQueso == "Pategras") return QuesosPategras; //si es pategras, retorna la cantidad de quesos del cazador
        return 0; // si no, retorna 0
    }

    // metodo que carga los quesos de a 1 unidad del
    public override void AgregarQueso(string tipoDeQueso)
    {
        if (tipoDeQueso == "Pategras")
        {
            QuesosPategras++;
            print($"Cazador sumó {tipoDeQueso}! Total: {QuesosPategras}");

            ActualizarVisualizacionBarra();

            HabilidadBase habilidad = GetComponent<HabilidadBase>(); // obtenemos la habilidad del cazador 

            if (QuesosPategras >= quesosParaHabilidad && habilidad != null) // si tiene la cantidad de quesos necesarios y la habilidad no es nula
            {
                JugadorController controller = GetComponent<JugadorController>();  // obtenemos el controller del cazador para pasarlo a la habilidad
                
                habilidad.Ejecutar(gameObject, controller); // ejecutamos la habilidad del cazador, pasandole el gameObject del cazador y su controller

                RestarQuesos("Pategras", quesosParaHabilidad); // resta los cheeses
            }
        }
    }

    // metodo para restar las 4 unidades de queso gastadas por la pasiva
    public override void RestarQuesos(string tipoDeQueso, int cantidad)
    {
        if (tipoDeQueso == "Pategras") // si es del tipo de queso que el cazador puede usar
        {
            QuesosPategras -= cantidad; // resta la cantidad de quesos gastados

            ActualizarVisualizacionBarra();
        }
    }

    // metodo para actualizar la barra de habilidad en la UI del cazador
    private void ActualizarVisualizacionBarra()
    {
        if (uiAsociada != null && quesosParaHabilidad > 0)
        {
            float porcentaje = (float)QuesosPategras / quesosParaHabilidad;
            uiAsociada.SetearLlenadoHabilidad(porcentaje);
        }
    }
}