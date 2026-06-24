using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    ╭━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╮
      CheckGroundSpawner ᝰ
        Se usa para detectar cuando el item toca el suelo y asi evitar que siga cayendo, tambien 
        se usa para detectar cuando el jugador toca el item y asi destruirlo para simular que lo
        recolecto
    ╰━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╯
*/

public class CheckGroundSpawner : CheckGround
{

    //  •• <<────────────────≪•◦ Collision ◦•≫────────────────>> ••

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Esto mantiene el ítem quieto al tocar el suelo
        if (collision.gameObject.CompareTag("Pisable"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        // Chquea si el jugador toca el item asi se destruye para simular que lo recolecto
        if (other.CompareTag("Cazador") || other.CompareTag("Soporte"))
        {
            Destroy(gameObject); 
        }
    }
}