using UnityEngine;

public class QuesoMozzarella : ClaseItem
{
    protected override string TagPermitido => "Soporte"; // solo el soporte puede recolectar este queso
    public QuesoMozzarella() { nombreItem = "Queso Mozzarella"; }

    protected override void AplicarEfectoSoporte(DatosSoporte soporte)
    {
        soporte.AgregarQueso("Mozzarella"); // manda el string directo para que el soporte agregue el queso correspondiente
    }
}