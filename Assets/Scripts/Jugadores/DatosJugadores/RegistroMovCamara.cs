using UnityEngine;
using Cinemachine;

/*
    ╭━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╮
      RegistroMovCamara
        Este script se encarga de registrar el objeto al CinemachineTargetGroup para que la 
        camara lo siga    
    ╰━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╯
*/

public class RegistroMovCamara : MonoBehaviour
{
    void Start()
    {
        // Se busca el componente en la escena
        CinemachineTargetGroup grupoCamara = FindFirstObjectByType<CinemachineTargetGroup>();

        if (grupoCamara != null)
        {
            // Se agrega el objeto al grupo de la camara
            grupoCamara.AddMember(this.transform, 1f, 0f);
        }
    }
}