using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    // variables de salud del personaje
    public int health = 100; // vida del personaje
    public int maxHealth = 100; // vida máxima del personaje
    public bool esInmune { get; private set; } = false;

    public virtual void RecibirDanio(int damage) // metodo para recibir daño
    {
        if (esInmune) return; // si el personaje es inmune, no recibe daño

        health -= damage; // restamos el daño a la vida

        if (health <= 0) // si la vida es menor o igual a 0
        {
            Morir(); // llamamos al metodo de morir
        }
    }

    public void Morir() // metodo para morir
    {
        Destroy(gameObject); // destruimos el objeto del personaje
        if (GameManager.Instance != null)
        {
            GameManager.Instance.EstablecerDerrota();
        }
    }

    // funcionalides especiales para el soporte
        public void ActivarInmunidadTemporal(float duracion) // metodo para activar la inmunidad temporal del soporte
    {
        StartCoroutine(RutinaInmunidad(duracion)); // iniciamos la rutina de inmunidad temporal
    }

    private IEnumerator RutinaInmunidad(float duracion) // rutina que maneja el estado de inmunidad temporal del soporte, usa un ienumerator para esperar el tiempo de duración del efecto
    {
        esInmune = true; // arranca a ser inmune
        print($"{gameObject.name} es ahora inmune por {duracion} segundos."); 

        yield return new WaitForSeconds(duracion); // duracion del efecto de inmunidad

        esInmune = false; // vuelve a la normalidad
        print($"{gameObject.name} ya no es inmune.");
    }

    public void Curar(int cantidad)
    {
        health += cantidad; // sumamos la curación

        if (health > maxHealth) //limita la cantidad de curación para que no supere la vida máxima
        {
            health = maxHealth;
        }
    }
}