using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borrador : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

/*
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace DemoMetegolPorTurnos.Scripts
{
    public class GameManager : MonoBehaviour
    {

        //Esto permite llamar al GameManager desde cualquier clase.
        public static GameManager Instance;
        public GameObject controlJugador1;
        public GameObject controlJugador2;
        
    [Header(". ݁₊ ⊹ . ݁ Referencias y Variables  ݁ . ⊹ ₊ ݁.")]

        [SerializeField] private GameObject primeraFichaJP1;
        [SerializeField] private GameObject segundaFichaJP1;
        [SerializeField] private GameObject terceraFichaJP1;
        [SerializeField] private GameObject primeraFichaJP2;
        [SerializeField] private GameObject segundaFichaJP2;
        [SerializeField] private GameObject terceraFichaJP2;
        [SerializeField] private GameObject pelota;
        [SerializeField] private GameObject turno1;
        [SerializeField] private GameObject turno2;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _pausaButton;
        [SerializeField] private Button _reiniciarButton;
        [SerializeField] private AudioManager audioManager;

        private bool enPausa = false;
        public float timer = 0;
        public float tiempoTurnoMax;
        public TextMeshProUGUI textoTimer;
        public TextMeshProUGUI textoPuntajeJP1;
        public TextMeshProUGUI textoPuntajeJP2;
        public TextMeshProUGUI textoVictoria;
        public bool timerComenzable = false;
        public bool turnoJugador1 = true;
        private Vector3 posicionInicialFicha1JP1;
        private Vector3 posicionInicialFicha2JP1;
        private Vector3 posicionInicialFicha3JP1;
        private Vector3 posicioninicialFicha1JP2;
        private Vector3 posicionInicialFicha2JP2;
        private Vector3 posicionInicialFicha3JP2;

        public float puntajeJugador1 = 0;
        public float puntajeJugador2 = 0;
        private float velocidadOriginalFicha;
        private float fuerzaOriginalFicha;

        [Header(". ݁₊ ⊹ . ݁ Animaciones  ݁ . ⊹ ₊ ݁.")]

        [SerializeField] private GameObject prefabVFXGol;
        [SerializeField] private GameObject puntoGolJp1;
        [SerializeField] private GameObject puntoGolJp2;

        [SerializeField] private GameObject victoria;
        [SerializeField] private GameObject puntoVictoria;

        private void Awake()
        {
            //Esto permite que sea una instancia unica y no haya mas de dos.
            if (Instance!=null)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            segundaFichaJP1.SetActive(false);
            terceraFichaJP1.SetActive(false);  
            segundaFichaJP2.SetActive(false);
            terceraFichaJP2.SetActive(false);

            controlJugador1.SetActive(false);
            controlJugador2.SetActive(false);

            turno1.SetActive(false);
            turno2.SetActive(false);

            _playButton.onClick.AddListener(TocarPlay);
            _pausaButton.onClick.AddListener(TocarPausa);
            _reiniciarButton.onClick.AddListener(TocarReiniciar);

            posicionInicialFicha1JP1 = primeraFichaJP1.transform.position;
            posicionInicialFicha2JP1 = segundaFichaJP1.transform.position;
            posicionInicialFicha3JP1 = terceraFichaJP1.transform.position;
            posicioninicialFicha1JP2 = primeraFichaJP2.transform.position;
            posicionInicialFicha2JP2 = segundaFichaJP2.transform.position;
            posicionInicialFicha3JP2 = terceraFichaJP2.transform.position;

            velocidadOriginalFicha = primeraFichaJP1.GetComponent<JugadorController>().velocidadRotacion;
            fuerzaOriginalFicha = primeraFichaJP1.GetComponent<JugadorController>().fuerzaDeImpulso;

        }

        private void Update()
        {
            if (timerComenzable && !enPausa)
            {
                comenzarTimerEnTurno();
            }
        }

        public void comenzarTimerEnTurno()
        {
            timer -= Time.deltaTime;
            textoTimer.text = "" + timer.ToString("f1");
            if(turnoJugador1 && timer <= 0)
            {
                SeleccionarJugador2();
                timer = tiempoTurnoMax;
            }
            else if (!turnoJugador1 && timer <= 0)
            {
                SeleccionarJugador1();
                timer = tiempoTurnoMax;
            }
        }

        public void TocarPlay()
        {
            enPausa = false;
            _pausaButton.gameObject.SetActive(true);
            turno1.SetActive(true);
            turno2.SetActive(true);
            _playButton.gameObject.SetActive(false);
        }

        public void TocarPausa()
        {
            enPausa = true;
            turno1.SetActive(false);
            turno2.SetActive(false);
            controlJugador1.SetActive(false);
            controlJugador2.SetActive(false);
            _pausaButton.gameObject.SetActive(false);
            _playButton.gameObject.SetActive(true);
        }

        public IEnumerator ReinicioDeJuego()
        {
            yield return new WaitForSeconds(5.0f);
            SceneManager.LoadScene("Game");
            enPausa = false;
        }

        public void TocarReiniciar()
        {
            SceneManager.LoadScene("Game");
            enPausa = false;
        }

        public void ReiniciarRonda(string ganador)
        {
            if(puntajeJugador1 >= 3 || puntajeJugador2 >= 3)
            {
                return;
            }

            if(ganador == "Jugador1")
            {
                controlJugador1.SetActive(false);
                controlJugador2.SetActive(true);
                turnoJugador1 = false;
            }
            else if(ganador == "Jugador2")
            {
                controlJugador1.SetActive(true);
                controlJugador2.SetActive(false);
                turnoJugador1 = true;
            }

            timer = tiempoTurnoMax;
            devolverFichasAPosicionInicial(); 
            desactivarFichasAdicionales();
            resetearValoresFichas();
            pelota.GetComponent<Pelota>().ReiniciarPosicion();

        }

        private void resetearValoresFichas()
        {
            JugadorController[] fichas = FindObjectsOfType<JugadorController>();
            foreach (JugadorController ficha in fichas)
            {
                ficha.fuerzaDeImpulso = fuerzaOriginalFicha;
                ficha.velocidadRotacion = velocidadOriginalFicha;
            }
        }

        private void ResetearFisicasFicha(GameObject ficha)
        {
            if(ficha == null)
            {
                return;
            }
            Rigidbody2D rb = ficha.GetComponent<Rigidbody2D>();  // acá reseteé las fisicas de las fichas para que no sigan en la siguiente ronda aplicandose las fuerzas de la anterior
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }

        private void devolverFichasAPosicionInicial()
        {
            primeraFichaJP1.transform.position = posicionInicialFicha1JP1;
            ResetearFisicasFicha(primeraFichaJP1);
            segundaFichaJP1.transform.position = posicionInicialFicha2JP1;
            ResetearFisicasFicha(segundaFichaJP1);
            terceraFichaJP1.transform.position = posicionInicialFicha3JP1;
            ResetearFisicasFicha(terceraFichaJP1);
            primeraFichaJP2.transform.position = posicioninicialFicha1JP2;
            ResetearFisicasFicha(primeraFichaJP2);
            segundaFichaJP2.transform.position = posicionInicialFicha2JP2;
            ResetearFisicasFicha(segundaFichaJP2);
            terceraFichaJP2.transform.position = posicionInicialFicha3JP2;
            ResetearFisicasFicha(terceraFichaJP2);
        }

        private void desactivarFichasAdicionales()
        {
            segundaFichaJP1.SetActive(false);
            terceraFichaJP1.SetActive(false);
            segundaFichaJP2.SetActive(false);
            terceraFichaJP2.SetActive(false);
        }

        private void MostrarVFXGol(string jugador)
        {
            if(jugador == "Jugador1")
            {
                GameObject vfxGol = Instantiate(prefabVFXGol, puntoGolJp1.transform.position, Quaternion.identity);
                if (vfxGol != null)
                {
                    Destroy(vfxGol, 3.0f);
                }
            }
            else if(jugador == "Jugador2")
            {
                GameObject vfxGol = Instantiate(prefabVFXGol, puntoGolJp2.transform.position, Quaternion.identity);
                if (vfxGol != null)
                {
                    Destroy(vfxGol, 3.0f);
                }
            }
        }

        private void MostrarVFXVictoria()
        {
            GameObject vfxVictoria = Instantiate(victoria, puntoVictoria.transform.position, Quaternion.identity);
            audioManager.ReproducirVictoria();
            if (vfxVictoria != null)
            {
                Destroy(vfxVictoria, 5.0f);
            }
        }

        private void Victoria(string jugadorGanador)
        {
            timerComenzable = false;
            controlJugador1.SetActive(false);
            controlJugador2.SetActive(false);
            Debug.Log($"¡Gana el {jugadorGanador}!");
            if (textoVictoria != null)
                {
                    textoVictoria.gameObject.SetActive(true);
                    textoVictoria.text = $"¡Gana el {jugadorGanador}!";
                }
            MostrarVFXVictoria();
            StartCoroutine(ReinicioDeJuego());
        }

        public void SeleccionarJugador1()
        {
            controlJugador1.SetActive(true);
            controlJugador2.SetActive(false);
            timerComenzable = true;
            turnoJugador1 = true;
            timer = tiempoTurnoMax;
        }

        public void SeleccionarJugador2()
        {
            controlJugador1.SetActive(false);
            controlJugador2.SetActive(true);
            timerComenzable = true;
            turnoJugador1 = false;
            timer = tiempoTurnoMax;
        }

        public void ActualizarPuntajeJP1(float puntaje)
        {
            puntajeJugador1 += puntaje;
            textoPuntajeJP1.text = puntajeJugador1.ToString("f0");
            MostrarVFXGol("Jugador2");
            if (puntajeJugador1 >= 3)
            {
                Victoria("Jugador 1");
            }
            else
            {
                ReiniciarRonda("Jugador1");
            }
        }

        public void ActualizarPuntajeJP2(float puntaje)
        {
            puntajeJugador2 += puntaje;
            textoPuntajeJP2.text = puntajeJugador2.ToString("f0");
            MostrarVFXGol("Jugador1");
            if (puntajeJugador2 >= 3)
            {
                Victoria("Jugador 2");
            }
            else
            {
                ReiniciarRonda("Jugador2");
            }
        }

        //TODO: Crear las funciones necesarias segun las responsabilidades del GameManager

    }
}
*/

}
