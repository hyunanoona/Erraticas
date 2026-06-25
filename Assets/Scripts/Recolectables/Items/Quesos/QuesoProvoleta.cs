using UnityEngine;

public class QuesoProvoleta : ClaseItem
{
    protected override string TagPermitido => "Soporte"; // solo el soporte puede recolectar este queso
    public QuesoProvoleta() { nombreItem = "Queso Provoleta"; }

    protected override void AplicarEfectoSoporte(DatosSoporte soporte)
    {
        soporte.AgregarQueso("Provoleta"); // manda el string directo para que el soporte agregue el queso correspondiente
    }
}