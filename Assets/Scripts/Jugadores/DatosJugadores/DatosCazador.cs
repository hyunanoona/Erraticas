using UnityEngine;
using System.Collections;

/*
    Este script es responsable de almacenar los datos del personaje, como su velocidad, su fuerza de salto, su escala de gravedad, etc. 
    Es una clase base que sera heredada por los datos especificos de cada rol (soporte y cazador), asi cada rol puede tener sus propios valores y habilidades sin afectar al otro.
*/


public class DatosCazador : DatosPersonaje
{
    // contadores de unidades fisicas de queso para el cazador
    public int QuesosPategras { get; private set; } = 0;
    public int PuntajeDelQueso { get; private set; } = 0;

   
    private int quesosParaHabilidad = 4;     // cantidad de quesos necesarios para activar la habilidad del cazador
    private UI_Cazador uiAsociada;           // referencia a la UI del cazador para actualizar la barra de habilidad
    private bool esperandoHabilidad = false; // var para bloquear la habilidad mientras se espera el titileo de 2 seg

    protected override void Awake()
    {
        base.Awake(); // llama al Awake de la clase base para inicializar el audioSource

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
        if (esperandoHabilidad) return;

        if (tipoDeQueso == "Pategras")
        {
            ReproducirSonidoMordida(); // reproduce el sonido de mordida

            QuesosPategras++;
            print($"Cazador sumó {tipoDeQueso}! Total: {QuesosPategras}");

            ActualizarVisualizacionBarra();

            HabilidadBase habilidad = GetComponent<HabilidadBase>(); // se obtiene la habilidad del cazador 

            // chequea si se ejecuta la corrutina para esperar 2 segy luego ejecutar la habilidad
            if (QuesosPategras >= quesosParaHabilidad && habilidad != null) 
            {
                StartCoroutine(EsperaYEjecutaHabilidad(habilidad));
            }
        }
    }

    // metodo para esperar 2 segundos y luego ejecuta la habilidad del cazador
    private IEnumerator EsperaYEjecutaHabilidad(HabilidadBase habilidad)
    {
        esperandoHabilidad = true;

        // Si hay UI asociada se setea la barra de habilidad y se activa el titileo
        if (uiAsociada != null) uiAsociada.SetearLlenadoHabilidad(1f, true);

        yield return new WaitForSeconds(2f);

        // Se reproduce el sonido de la habilidad usada y se ejecuta la habilidad del cazador
        ReproducirSonidoHabilidad1Usada();
        JugadorController controller = GetComponent<JugadorController>();
        habilidad.Ejecutar(gameObject, controller);

        RestarQuesos("Pategras", quesosParaHabilidad);

        esperandoHabilidad = false;
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
            float porcentaje = (float)QuesosPategras / quesosParaHabilidad; // calcula el porcentaje de quesos acumulados para activar la habilidad
            uiAsociada.SetearLlenadoHabilidad(porcentaje, false);           // actualiza la barra de habilidad sin titileo
        }
    }
}