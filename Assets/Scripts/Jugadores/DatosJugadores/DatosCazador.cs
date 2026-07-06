using UnityEngine;

/*
    Este script es responsable de almacenar los datos del personaje, como su velocidad, su fuerza de salto, su escala de gravedad, etc. 
    Es una clase base que sera heredada por los datos especificos de cada rol (soporte y cazador), asi cada rol puede tener sus propios valores y habilidades sin afectar al otro.
*/
using System.Collections; // 🌟 Necesario para usar IEnumerator

public class DatosCazador : DatosPersonaje
{
    // contadores de unidades fisicas de queso para el cazador
    public int QuesosPategras { get; private set; } = 0;
    public int PuntajeDelQueso { get; private set; } = 0;

    // UI de los pjs
    private int quesosParaHabilidad = 4; // De prueba por el momento 
    private UI_Cazador uiAsociada;       // Se guarda la UI asignada a este clon del cazador
    private bool esperandoHabilidad = false; // 🌟 Evita agarrar quesos de más mientras titila

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
        if (esperandoHabilidad) return; // 🌟 Bloqueo temporal durante los 2 segundos de parpadeo

        if (tipoDeQueso == "Pategras")
        {
            ReproducirSonidoMordida(); // reproduce el sonido de mordida

            QuesosPategras++;
            print($"Cazador sumó {tipoDeQueso}! Total: {QuesosPategras}");

            ActualizarVisualizacionBarra();

            HabilidadBase habilidad = GetComponent<HabilidadBase>(); // obtenemos la habilidad del cazador 

            if (QuesosPategras >= quesosParaHabilidad && habilidad != null) // si tiene la cantidad de quesos necesarios y la habilidad no es nula
            {
                // 🌟 En vez de ejecutar todo instantáneo, llamamos a la espera de 2 segundos
                StartCoroutine(EsperaYEjecutaHabilidad(habilidad));
            }
        }
    }

    // 🌟 NUEVA CORRUTINA: Mantiene la barra al 100% y titilando, luego gasta y ejecuta
    private IEnumerator EsperaYEjecutaHabilidad(HabilidadBase habilidad)
    {
        esperandoHabilidad = true;

        // Le avisamos a la UI que fuerce el 100% y empiece a parpadear
        if (uiAsociada != null) uiAsociada.SetearLlenadoHabilidad(1f, true);

        // Esperamos exactamente los 2 segundos que me pediste
        yield return new WaitForSeconds(2f);

        // Pasados los 2 segundos de feedback, suena y se ejecuta la habilidad original
        ReproducirSonidoHabilidad1Usada();
        JugadorController controller = GetComponent<JugadorController>();
        habilidad.Ejecutar(gameObject, controller);

        // Restamos los quesos (lo que bajará la barra a 0 y apagará el titileo)
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
            float porcentaje = (float)QuesosPategras / quesosParaHabilidad;
            // Mandamos el porcentaje normal, y "false" porque no debe titilar en la carga común
            uiAsociada.SetearLlenadoHabilidad(porcentaje, false);
        }
    }
}