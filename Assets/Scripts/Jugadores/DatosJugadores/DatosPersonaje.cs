using UnityEngine;

/*
    Este script es responsable de almacenar los datos del personaje, como su velocidad, su fuerza de salto, su escala de gravedad, etc. 
    Es una clase base que sera heredada por los datos especificos de cada rol (soporte y cazador), asi cada rol puede tener sus propios valores y habilidades sin afectar al otro.
*/

public class DatosPersonaje : MonoBehaviour
{
    // variables de movimiento y fisicas del personaje //
    public float Velocidad { get; protected set; }
    public float FuerzaSalto { get; protected set; }
    public float EscalaGravedad { get; protected set; }

    [Header("Efectos de sonido")]
    [SerializeField] private AudioClip sonidoMordida;
    private AudioSource audioSource;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public virtual int ObtenerCantidadQueso(string tipoDeQueso)
    {
        return 0; // por default devuelve 0
    }

    public virtual void AgregarQueso(string tipoDeQueso)
    {
        ReproducirSonidoMordida(); // ñam
    }

    public virtual void RestarQuesos(string tipoDeQueso, int cantidad)
    {
        // metodo abstracto
    }

    public void ReproducirSonidoMordida()
    {
        if (audioSource != null && sonidoMordida != null)
        {
            audioSource.PlayOneShot(sonidoMordida);
        }
    }
}