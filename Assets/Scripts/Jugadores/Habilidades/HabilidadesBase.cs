using UnityEngine;

public abstract class HabilidadBase : MonoBehaviour
{
    [Header("Configuración de Requesitos")] // badum tss 
    [SerializeField] private string tipoDeQuesoPorHabilidad; // el tag que identifica la habilidad
    [SerializeField] private int cantidadRequerida = 4;   // costo fijo, puede cambiarse en el inspector

    public bool PuedeEjecutar(DatosSoporte soporte) // metodo para chequear si tiene los quesos necesarios para ejecutar la habilidad
    {
        if (soporte == null) return false;
        
        print($"El soporte esta intentando usar la habilidad del componente: '{this.GetType().Name}'. Requiere: '{tipoDeQuesoPorHabilidad}' (Necesita: {cantidadRequerida})");

        if (tipoDeQuesoPorHabilidad == "Roquefort") return soporte.QuesosRoquefort >= cantidadRequerida;
        if (tipoDeQuesoPorHabilidad == "Mozzarella") return soporte.QuesosMozzarella >= cantidadRequerida;
        if (tipoDeQuesoPorHabilidad == "Provoleta") return soporte.QuesosProvoleta >= cantidadRequerida;
        if (tipoDeQuesoPorHabilidad == "Cremoso") return soporte.QuesosCremoso >= cantidadRequerida;

        return false;
    }

    protected void GastarQuesos(DatosSoporte soporte) // metodo para restar las unidades de queso gastadas por la habilidad
    {
        if (soporte != null)
        {
            soporte.RestarQuesos(tipoDeQuesoPorHabilidad, cantidadRequerida);
        }
    }

    public abstract void Ejecutar(GameObject usuario, JugadorController aliadoCazador); // metodo abstracto que debe ser implementado por cada habilidad concreta
}