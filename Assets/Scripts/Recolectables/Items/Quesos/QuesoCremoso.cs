using UnityEngine;

public class QuesoCremoso : ClaseItem
{
    protected override string TagPermitido => "Soporte"; // solo el soporte puede recolectar este queso
    public QuesoCremoso() { nombreItem = "Queso Cremoso"; }

    protected override void AplicarEfectoSoporte(DatosSoporte soporte)
    {
        soporte.AgregarQueso("Cremoso"); // manda el string directo para que el soporte agregue el queso correspondiente
    }
}