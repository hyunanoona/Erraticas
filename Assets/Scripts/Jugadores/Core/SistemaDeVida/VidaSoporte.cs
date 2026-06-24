using UnityEngine;

public class HealthSupport : Health
{
    private JugadorController controlador;

    private void Start()
    {        
        controlador = GetComponent<JugadorController>(); // obtenemos la referencia al controlador del jugador para poder aplicar el silenciado
    }

    public override void RecibirDaño(int damage) //acomodamos el recibir daño para que haya polimorfismo y el soporte pueda ser silenciado en vez de recibir daño
    {
        if (controlador != null)
        {
            controlador.SilenciarHabilidades(5f);
            print($"{gameObject.name} ha sido silenciado por 5 segundos.");
        }
    }
}