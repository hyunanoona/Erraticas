using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Este script es responsable de controlar al jugador, es decir, manejar su movimiento, sus animaciones, sus interacciones con el entorno, etc. 
    Basicamente es el cerebro, habra otros scripts que se encargaran de otras funcionalidades del jugador, como su salud, sus habilidades segun rol, etc.
*/

public class JugadorController : MonoBehaviour
{
    // datos del PJ //
    private Rigidbody2D rb; // el componente de fisica del pj
    private InputJugador input; // el script que detecta las entradas del jugador
    private DatosPersonaje datos; // aca estan los datos del pj

    // ⊹₊˚‧︵‿₊୨ visuales୧₊‿︵‧˚₊⊹
    private Animator miAnimator;
    private SpriteRenderer miSpriteRenderer;

    //detector de suelo
    private CheckGround checkGround; // el script que detecta si el pj esta tocando el suelo o no

    //para el inmovilizado del pj
    private float duracionInmovilizado = 0f; // cuenta regresiva del tiempo que el pj esta inmovilizado
    public bool EstaInmovilizado => duracionInmovilizado > 0f; // booleano que indica si el pj esta inmovilizado o no

    //info para buff del hunter
    //doble salto//
    private float tiempoBuffDobleSalto = 0f; // cuenta regresiva del poder
    private bool yaHizoDobleSalto = false; // indicador de si el jugador ya ha realizado un doble salto, para que no salte salte infinito y mas alla
    //velocidad//
    private float tiempoBuffVelocidad = 0f; // cuenta regresiva del poder
    [SerializeField] private float multiplicadorVelocidad = 1.5f; // 1.5 significa que va 50% mas rapido, 2 significa que va el doble de rapido, etc.

    //info para el soporte
    private float duracionSilenciado = 0f;
    public float TiempoSilenciadoRestante => duracionSilenciado; // agregado de Nicolsita <3

    
    void Start()
    {
        // inicializamos las referencias a los componentes del jugador
        rb = GetComponent<Rigidbody2D>(); // el rigidbody o sea la parte fisica
        input = GetComponent<InputJugador>(); //accede al input 
        datos = GetComponent<DatosPersonaje>(); // accede a los datos del personaje
        checkGround = GetComponentInChildren<CheckGround>(); // accede al script que detecta si el pj esta tocando el suelo o no

        //⊹₊˚‧︵‿₊୨ Visuales୧₊‿︵‧˚₊⊹
        miAnimator = GetComponent<Animator>();
        miSpriteRenderer = GetComponent<SpriteRenderer>();

        rb.gravityScale = datos.EscalaGravedad; // ajusta la gravedad segun el pj
    }

    void Update()
    {
        // para el inmovilizado del pj
        if (duracionInmovilizado > 0f)
        {
            duracionInmovilizado -= Time.deltaTime; // si el pj esta inmovilizado, se va descontando el tiempo de inmovilizado
        }

        //habilidades de soporte
        if (duracionSilenciado > 0f)
        {
            duracionSilenciado -= Time.deltaTime; // si el pj esta silenciado, se va descontando el tiempo de silenciado
        }

        if (input.Habilidad1 && duracionSilenciado <= 0f && !EstaInmovilizado) // si el pj presiona la habilidad 1 y no esta silenciado ni inmovilizado
        {
            input.ConsumirHabilidad1(); // avisa al controlador que ya se uso la habilidad

            DatosSoporte datosSupp = datos as DatosSoporte; // intenta convertir los datos del pj a datos de soporte

            if (datosSupp != null)
            {
                datosSupp.PresionoHabilidad1(); // si el pj es soporte, ejecuta su habilidad 1
            }
        }

        if (input.Habilidad2 && duracionSilenciado <= 0f && !EstaInmovilizado) // si el pj presiona la habilidad 2 y no esta silenciado ni inmovilizado
        {
            input.ConsumirHabilidad2(); // avisa al controlador que ya se uso la habilidad

            DatosSoporte datosSupp = datos as DatosSoporte; // intenta convertir los datos del pj a datos de soporte

            if (datosSupp != null)
            {
                datosSupp.PresionoHabilidad2(); // si el pj es soporte, ejecuta su habilidad 2
            }
        }

        {
            input.ConsumirHabilidad2();

            DatosSoporte datosSoporte = datos as DatosSoporte; // intenta convertir los datos del pj a datos de soporte
            
            if (datosSoporte != null)
            {
                datosSoporte.PresionoHabilidad2(); // si el pj es soporte, ejecuta su habilidad 2
            }
        }

        //para el doble salto del hunter
        if (tiempoBuffDobleSalto > 0f)
        {
            tiempoBuffDobleSalto -= Time.deltaTime;
        }
        //para la velocidad del hunter
        if (tiempoBuffVelocidad > 0f)
        {
            tiempoBuffVelocidad -= Time.deltaTime;
        }

        // ⊹₊˚‧︵‿₊୨ Visuales ୧₊‿︵‧˚₊⊹
        ManejarAnimacionesYRotacion();
    }

    void FixedUpdate()
    {
        // si el pj esta inmovilizado
        if (EstaInmovilizado)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y); // no puede moverse horizontalmente, pero si puede caer por gravedad
            
            if (input.Salto) input.ConsumirSalto();  // avisa al controlador que ya se uso el salto, para que no pueda saltar infinitamente en un solo fotograma
            return; 
        }

        float velocidadActual = datos.Velocidad; // velocidad base del pj

        // si el pj tiene el buff de velocidad, se multiplica la velocidad base por el multiplicador
        if (tiempoBuffVelocidad > 0f)
        {
            velocidadActual *= multiplicadorVelocidad;
        
        }

        rb.velocity = new Vector2(input.MovimientoX * velocidadActual, rb.velocity.y); // mueve al jugador horizontalmente segun el input y la velocidad del pj

        // bandera para resetear el doble salto si el pj esta tocando el suelo
        if (checkGround != null && checkGround.EstaSobreAlgoPisable)
        {
            yaHizoDobleSalto = false;
        }
        
        if (input.Salto)
        {
            //esta es la logica normal del salto
            if (checkGround != null && checkGround.EstaSobreAlgoPisable) // si el pj esta tocando el suelo, puede saltar
            {
                rb.velocity = new Vector2(rb.velocity.x, datos.FuerzaSalto); // hace que el jugador salte segun la fuerza de salto del pj
            }
            // esta para el doble salto del hunter
            else if (tiempoBuffDobleSalto > 0f && !yaHizoDobleSalto)
            {
                rb.velocity = new Vector2(rb.velocity.x, datos.FuerzaSalto); // hace que el jugador salte segun la fuerza de salto del pj
                yaHizoDobleSalto = true; // para que no siga salta y salta
            }

            input.ConsumirSalto(); // avisa al controlador que ya se uso el salto, para que no pueda saltar infinitamente en un solo fotograma
        }
    }

    // ⊹₊˚‧︵‿₊୨ Método para las Visuales୧ ₊‿︵‧˚₊⊹

    private void ManejarAnimacionesYRotacion()
    {        
        if (miAnimator == null) return;

        //  . ݁₊ ⊹ . ݁ Chequear si está corriendo  ݁ . ⊹ ₊ ݁.
        bool estaMoviendose = (input.MovimientoX != 0f) && !EstaInmovilizado;
        miAnimator.SetBool("estaCorriendo", estaMoviendose);

        // . ݁₊ ⊹ . ݁ Espejar Sprite  . ⊹ ₊ ݁.
        if (input.MovimientoX > 0f)
        {
            miSpriteRenderer.flipX = true;
        }
        else if (input.MovimientoX < 0f)
        {
            miSpriteRenderer.flipX = false;
        }
    }

    public void ActivarBuffDobleSalto(float duracion)
    {
        tiempoBuffDobleSalto = duracion; // arranca o resetea el reloj del buff de doble salto
    }

    public void ActivarBuffVelocidad(float duracion)
    {
        tiempoBuffVelocidad = duracion; // arranca o resetea el reloj del buff de velocidad
    }

    public void SilenciarHabilidades(float duracion)
    {
        duracionSilenciado = duracion; // arranca o resetea el reloj del silenciado de habilidades
    }

    public void ActivarInmovilizado(float duracion)
    {
        duracionInmovilizado = duracion;
        print($"El jugador {gameObject.name} ha sido inmovilizado por {duracion} segundos.");
    }
}
