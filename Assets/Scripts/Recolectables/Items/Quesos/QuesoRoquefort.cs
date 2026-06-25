using UnityEngine;

public class QuesoRoquefort : ClaseItem
{
    protected override string TagPermitido => "Soporte"; // solo el soporte puede recolectar este queso
    public QuesoRoquefort() { nombreItem = "Queso Roquefort"; }

    protected override void AplicarEfectoSoporte(DatosSoporte soporte)
    {
        soporte.AgregarQueso("Roquefort"); // manda el string directo para que el soporte agregue el queso correspondiente
    }
}