using UnityEngine;

public abstract class HabilidadBase : MonoBehaviour
{
    [Header("Configuración de Requesitos")] // badum tss 
    [SerializeField] private string tipoDeQuesoPorHabilidad; // el tag que identifica la habilidad
    [SerializeField] private int cantidadRequerida = 4;   // costo fijo, puede cambiarse en el inspector

    public bool PuedeEjecutar(DatosPersonaje personaje) // metodo para chequear si tiene los quesos necesarios para ejecutar la habilidad
    {
        if (personaje == null) return false;
        
        print($"Intentando usar habilidad: '{this.GetType().Name}'. Requiere: '{tipoDeQuesoPorHabilidad}' ({cantidadRequerida})");

        return personaje.ObtenerCantidadQueso(tipoDeQuesoPorHabilidad) >= cantidadRequerida; // llama al metodo segun los datos especificos
    }

    protected void GastarQuesos(DatosPersonaje personaje) // metodo para restar las unidades de queso gastadas por la habilidad
    {
        if (personaje != null)
        {
            personaje.RestarQuesos(tipoDeQuesoPorHabilidad, cantidadRequerida); //polimorfismo al palo
        }
    }
    public abstract void Ejecutar(GameObject usuario, JugadorController aliadoCazador); // metodo abstracto que debe ser implementado por cada habilidad concreta
}