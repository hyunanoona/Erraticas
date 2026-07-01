using UnityEngine;
// IMPORTANTE: Esta línea nos permite usar las herramientas de Cinemachine
using Cinemachine;

public class RegistroMovCamara : MonoBehaviour
{
    void Start()
    {
        // Buscamos el componente en la escena
        CinemachineTargetGroup grupoCamara = FindFirstObjectByType<CinemachineTargetGroup>();

        if (grupoCamara != null)
        {
            // SOLUCIÓN PARA VERSIÓN NUEVA: Usamos AddMember en lugar de AddTarget
            grupoCamara.AddMember(this.transform, 1f, 0f);

            Debug.Log($"¡{gameObject.name} se registró correctamente usando AddMember!");
        }
        else
        {
            Debug.LogWarning("No se encontró ningún CinemachineTargetGroup en la escena.");
        }
    }
}