using UnityEngine;

/*
    Este script va pegado a los pies del jugador, con un collider trigger, para detectar si el jugador esta tocando el suelo o no.
*/
public class CheckGround : MonoBehaviour
{
    public bool EstaSobreAlgoPisable;

    protected virtual void OnTriggerEnter2D(Collider2D collision) // al tocar el suelo
    {
        if (collision.CompareTag("Pisable"))
        {
            EstaSobreAlgoPisable = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision) // al levantar los pies del suelo
    {
        if (collision.CompareTag("Pisable"))
        {
            EstaSobreAlgoPisable = false;
        }
    }
}