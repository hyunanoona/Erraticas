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
    [SerializeField] private AudioClip sonidoHabilidadCargada;
    [SerializeField] private AudioClip sonidoHabilidad1Usada;
    [SerializeField] private AudioClip sonidoHabilidad2Usada;

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
        // metodo abstracto
    }

    public virtual void RestarQuesos(string tipoDeQueso, int cantidad)
    {
        // metodo abstracto
    }

    // para sonidos y chucherias asi
    public void ReproducirSonidoMordida()
    {
        if (audioSource != null && sonidoMordida != null) // si el audioSource y el clip de sonido no son nulos
        {
            audioSource.PlayOneShot(sonidoMordida); // reproduce el sonido de mordida
        }
    }

    public void ReproducirSonidoHabilidadCargada()
    {
        if (audioSource != null && sonidoHabilidadCargada != null) // si el audioSource y el clip de sonido no son nulos
            audioSource.PlayOneShot(sonidoHabilidadCargada); // reproduce el sonido de habilidad cargada
    }

    public void ReproducirSonidoHabilidad1Usada()
    {
        if (audioSource != null && sonidoHabilidad1Usada != null) // si el audioSource y el clip de sonido no son nulos
            audioSource.PlayOneShot(sonidoHabilidad1Usada, 1f); // reproduce el sonido de habilidad usada
    }

    public void ReproducirSonidoHabilidad2Usada()
    {
        if (audioSource != null && sonidoHabilidad2Usada != null) // si el audioSource y el clip de sonido no son nulos
            audioSource.PlayOneShot(sonidoHabilidad2Usada); // reproduce el sonido de habilidad usada
    }
}