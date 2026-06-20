using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100; // vida del personaje

    public void RecibirDaño(int damage) // metodo para recibir daño
    {
        health -= damage; // restamos el daño a la vida

        if (health <= 0) // si la vida es menor o igual a 0
        {
            Morir(); // llamamos al metodo de morir
        }
    }

    public void Morir() // metodo para morir
    {
        Destroy(gameObject); // destruimos el objeto del personaje
    }
}