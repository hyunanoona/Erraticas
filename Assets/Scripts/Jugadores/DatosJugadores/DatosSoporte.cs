using UnityEngine;

/*
    Este script es responsable de almacenar los datos del personaje, como su velocidad, su fuerza de salto, su escala de gravedad, etc. 
    Es una clase base que sera heredada por los datos especificos de cada rol (soporte y cazador), asi cada rol puede tener sus propios valores y habilidades sin afectar al otro.
*/
public class DatosSoporte : DatosPersonaje
{
    private JugadorController cazadorAliado;
    
    //habilidades del soporte
    private HabilidadBase hab1;
    private HabilidadBase hab2;

    // contadores de unidades fisicas de queso para el soporte
    public int QuesosRoquefort { get; private set; } = 0;
    public int QuesosMozzarella { get; private set; } = 0;
    public int QuesosProvoleta { get; private set; } = 0;
    public int QuesosCremoso { get; private set; } = 0;

    void Awake()
    {
        // datos especificos del soporte
        Velocidad = 6f;
        FuerzaSalto = 16f;
        EscalaGravedad = 2f;
    }

    void Start()
    {
        GameObject cazador = GameObject.FindWithTag("Cazador"); // buscamos el objeto del cazador aliado en la escena

        if (cazador != null)
        {
            cazadorAliado = cazador.GetComponent<JugadorController>(); // obtenemos el componente del cazador aliado
        }

        HabilidadBase[] habilidades = GetComponents<HabilidadBase>(); // obtenemos todas las habilidades del soporte

        if (habilidades.Length > 0)
        {
            hab1 = habilidades[0]; // asignamos la primera habilidad a hab1, doble salto en el booster y curacion en el healer
            hab2 = habilidades[1]; // asignamos la segunda habilidad a hab2, velocidad en el booster e inmunidad en el healer
        }
    }

    public void PresionoHabilidad1()
    {
        if (hab1 != null)
        {
            if (hab1.PuedeEjecutar(this)) // solo ejecuta si la habilidad 1 confirma que tiene los 4 quesos correspondientes
            {
                hab1.Ejecutar(gameObject, cazadorAliado); // ejecutamos la habilidad pasando la info del soporte y del cazador aliado
            }
        }
    }

    public void PresionoHabilidad2()
    {
        if (hab2 != null)
        {
            if (hab2.PuedeEjecutar(this)) // solo ejecuta si la habilidad 2 confirma que tiene los 4 quesos correspondientes
            {
                hab2.Ejecutar(gameObject, cazadorAliado); // ejecutamos la habilidad pasando la info del soporte y del cazador aliado
            }
        }
    }

    // metodo para obtener la cantidad de quesos especifica del soporte
    public override int ObtenerCantidadQueso(string tipoDeQueso)
    {
        if (tipoDeQueso == "Roquefort") return QuesosRoquefort;
        if (tipoDeQueso == "Mozzarella") return QuesosMozzarella;
        if (tipoDeQueso == "Provoleta") return QuesosProvoleta;
        if (tipoDeQueso == "Cremoso") return QuesosCremoso;
        return 0;
    }

    // metodo que carga los quesos de a 1 unidad del soporte
    public override void AgregarQueso(string tipoDeQueso)
    {
        if (tipoDeQueso == "Roquefort") QuesosRoquefort++;
        if (tipoDeQueso == "Mozzarella") QuesosMozzarella++;
        if (tipoDeQueso == "Provoleta") QuesosProvoleta++;
        if (tipoDeQueso == "Cremoso") QuesosCremoso++;
        
        print($" Quesito sumado tipo: {tipoDeQueso} | Total: R:{QuesosRoquefort} M:{QuesosMozzarella} P:{QuesosProvoleta} C:{QuesosCremoso}");
    }

    // metodo para restar las 4 unidades de queso gastadas por la habilidad
    public override void RestarQuesos(string tipoQueso, int cantidad)
    {
        if (tipoQueso == "Roquefort") QuesosRoquefort -= cantidad;
        if (tipoQueso == "Mozzarella") QuesosMozzarella -= cantidad;
        if (tipoQueso == "Provoleta") QuesosProvoleta -= cantidad;
        if (tipoQueso == "Cremoso") QuesosCremoso -= cantidad;
    }
}