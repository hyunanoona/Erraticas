using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ConfigNivelEscena
{
    public string nombreNivel;
    public int puntajeParaGanar;  
    public float tiempoParaGanar = 90f;
    public AudioClip musicaNivel; // para que cada nivel tenga su rola
}

[System.Serializable]
public class PersonajePrefabConfig
{
    public string nombreID;
    public GameObject prefabObjeto; 
}

public class GameManager : MonoBehaviour
{

    /*
    GameManager ᝰ
    ಄ Se encarga de la gestión de los niveles (pasar de un escenario a otro, cargar el siguiente nivel), actualiza el estado del juego, controla el puntaje de los jugadores, 
	಄ Conecta las animaciones y los audios con los eventos del juego, y maneja la condición de victoria o derrota. También instancia los jugadores. 
    */

    [Header(". ݁₊ ⊹ . ݁ Referencias y Variables  ݁ . ⊹ ₊ ݁.")]
        public static GameManager Instance;
        [Header(". ݁₊ ⊹ . ݁ Lista de Niveles  ݁ . ⊹ ₊ ݁.")]
        [SerializeField] private List<ConfigNivelEscena> niveles; 
        [Header(". ݁₊ ⊹ . ݁ Base de Datos de Personajes  ݁ . ⊹ ₊ ݁.")]
        [SerializeField] private List<PersonajePrefabConfig> catalogoPersonajes;
        public string tipoSeleccionadoP1;
        public string tipoSeleccionadoP2;
        public string rolBaseP1; 
        public string rolBaseP2;
        public int nivelActualIndice = 0;
        public int puntajeJugador = 0;      
        private TextMeshProUGUI textoPuntajeUI;

        // . ⊹ ₊ ݁. Sistema de cronómetro . ⊹ ₊ ݁.
        private float tiempoRestante;
        private bool nivelTerminado = false;
        private TMPro.TextMeshProUGUI textoCronometro;

        // para la musica de cada nivel
        [SerializeField] private AudioSource audioSourceMúsica; // referencia al componente AudioSource para reproducir la música de cada nivel


        private void Awake()
        {
            if (Instance != null) 
            { 
                Destroy(gameObject); 
                return;
            }
            else { Instance = this; DontDestroyOnLoad(gameObject); }

            // para la musica de cada nivel
            audioSourceMúsica = GetComponent<AudioSource>(); // accede al componente AudioSource del GameObject al que está adjunto este script

            SceneManager.sceneLoaded += AlCargarEscena;
        }

        public void OnDestroy()
        {
            SceneManager.sceneLoaded -= AlCargarEscena;
        }

        public void GuardarSeleccionMenu(string tipoP1, string tipoP2)
        {
            tipoSeleccionadoP1 = tipoP1;
            tipoSeleccionadoP2 = tipoP2;
            Debug.Log($"GameManager guardó: P1 = {tipoP1}, P2 = {tipoP2}");
        }

        private void AlCargarEscena(Scene escena, LoadSceneMode modo)
        {
            if (escena.name == "MenuInicial" || escena.name == "SeleccionRol" ||
                escena.name == "Derrota" || escena.name == "Victoria")
            {
                nivelTerminado = true;
                if (audioSourceMúsica != null) audioSourceMúsica.Stop(); // detiene la música si se está reproduciendo y noe sta en el nivel
                return;
            }

            // Inicializacion de cada Nivel
            nivelTerminado = false;
            puntajeJugador = 0;

            // ⤷ ゛Texto de puntaje ˎˊ˗
            GameObject puntajeObj = GameObject.FindWithTag("TextoPuntaje");
            if (puntajeObj != null) textoPuntajeUI = puntajeObj.GetComponent<TMPro.TextMeshProUGUI>();
            ActualizarTextoPuntaje();

            // ⤷ ゛Texto del timer ˎˊ˗
            GameObject textObj = GameObject.FindWithTag("TextoCronometro");
            if (textObj != null) textoCronometro = textObj.GetComponent<TMPro.TextMeshProUGUI>();
            if(nivelActualIndice < niveles.Count)
            {
                tiempoRestante = niveles[nivelActualIndice].tiempoParaGanar;

                //para la musica de cada nivel
                AudioClip cancionNivel = niveles[nivelActualIndice].musicaNivel; // obtiene la canción del nivel actual
                ControlarReproduccionMusica(cancionNivel); // llama al método para controlar la reproducción de la música del nivel
            }
            else
            {
                tiempoRestante = 90f;
            }

            SpawnearJugadores();
        }

        void Update()
        {
            if (nivelTerminado) return;
            if (tiempoRestante > 0)
            {
                tiempoRestante -= Time.deltaTime;
                ActualizarTextoCronometro();
            }
            else
            {
                tiempoRestante = 0;
                ActualizarTextoCronometro();
                EstablecerDerrota();
            }
        }

        private void ActualizarTextoCronometro()
        {
            if (textoCronometro == null) return;
            int minutos = Mathf.FloorToInt(tiempoRestante / 60);
            int segundos = Mathf.FloorToInt(tiempoRestante % 60);
            textoCronometro.text = string.Format("{0:00}: {1:00}", minutos, segundos);
        }

        private void SpawnearJugadores()
        {

            string personajeFinalP1 = tipoSeleccionadoP1;
            string personajeFinalP2 = tipoSeleccionadoP2;

            //  ⤷ ゛En caso de querer probar el nivel sin pasar por el menú de selección asigno por defecto ˎˊ˗
            if (string.IsNullOrEmpty(personajeFinalP1)) 
            {
                personajeFinalP1 = "Cunty"; 
            }
            if (string.IsNullOrEmpty(personajeFinalP2)) 
            {
                personajeFinalP2 = "Lava Rigota"; 
            }
            // ꒷꒦︶꒷꒦︶ ๋ ࣭ ⭑꒷꒦꒷꒦︶꒷꒦︶ ๋ ࣭ ⭑꒷꒦꒷꒦︶꒷꒦︶ ๋ ࣭ ⭑꒷꒦꒷꒦︶꒷꒦︶ ๋ ࣭ ⭑꒷꒦꒷꒦︶꒷꒦︶ ๋ ࣭ ⭑꒷꒦꒷꒦︶꒷꒦︶ ๋ ࣭ ⭑꒷꒦꒷꒦︶꒷꒦
            
            GameObject spawnP1 = GameObject.FindWithTag("SpawnPointP1");
            GameObject spawnP2 = GameObject.FindWithTag("SpawnPointP2");

            Vector3 posP1 = spawnP1 != null ? spawnP1.transform.position : new Vector3(-2, 0, 0);
            Vector3 posP2 = spawnP2 != null ? spawnP2.transform.position : new Vector3(2, 0, 0);

            //  . ݁₊ ⊹ . ݁ SPAWN JUGADOR 1 (LADO IZQUIERDO, WASD) ݁ . ⊹ ₊ ݁.

            GameObject prefabP1 = BuscarPrefabPorNombre(personajeFinalP1);
            if (prefabP1 != null) {
                GameObject clonP1 = Instantiate(prefabP1, posP1, Quaternion.identity);
                InputJugador inputP1 = clonP1.GetComponent<InputJugador>();
                if (inputP1 != null)
                {
                    inputP1.jugadorAsignado = InputJugador.NumeroJugador.Jugador1;
                }
            }

            //  . ݁₊ ⊹ . ݁ SPAWN JUGADOR 2 (LADO DERECHO, Flechitas) ݁ . ⊹ ₊ ݁.

            GameObject prefabP2 = BuscarPrefabPorNombre(personajeFinalP2);
            if (prefabP2 != null) {
                GameObject clonP2 = Instantiate(prefabP2, posP2, Quaternion.identity);
                InputJugador inputP2 = clonP2.GetComponent<InputJugador>();
                if (clonP2 != null)
                {
                    inputP2.jugadorAsignado = InputJugador.NumeroJugador.Jugador2;
                }
            }

            UIManager uiManagement = Object.FindFirstObjectByType<UIManager>();
            if (uiManagement != null)
            {
                uiManagement.ConfigurarInterfazNvl(personajeFinalP1, personajeFinalP2);
            }
        }

        private GameObject BuscarPrefabPorNombre(string nombre)
        {
            foreach (var config in catalogoPersonajes)
        {
            if(config.nombreID == nombre)
            {
                return config.prefabObjeto;
            }
        }
            Debug.LogWarning($"No se encontró ningún Prefab para el nombre: {nombre}");
            return null;
        }

        public void SumarPuntos(int puntos)
        {
            if (nivelTerminado) return;
            puntajeJugador += puntos;
            ActualizarTextoPuntaje();
            ChequearMetaNivel();
        }

        public void SumarTiempoExtra(float segundosExtra) // para la pasiva de la cunty, agregado por ame :p
        {
            if (nivelTerminado) return; // si el nivel termino, returnea

            tiempoRestante += segundosExtra; // suma el tiempo extra al cronometro
            
            ActualizarTextoCronometro(); //llama al metodo para actualizar el texto del cronometro en la UI

            print($"Tiempo extra sumado: {segundosExtra} segundos. Tiempo restante ahora: {tiempoRestante} segundos."); // imprime en consola el tiempo extra sumado y el tiempo restante
        }

        private void ActualizarTextoPuntaje()
        {
                if (textoPuntajeUI != null)
                {
                    textoPuntajeUI.text = puntajeJugador.ToString();
                }
        }

        private void ChequearMetaNivel()
        {
            if (nivelActualIndice >= niveles.Count) return;

            ConfigNivelEscena nivelActual = niveles[nivelActualIndice];
    
            if(puntajeJugador >= nivelActual.puntajeParaGanar)
            {
                AvanzarDeEscena();
            }
        }

        private void AvanzarDeEscena()
        {
            nivelActualIndice++;
            if (nivelActualIndice < niveles.Count)
            {
                string proximaEscena = niveles[nivelActualIndice].nombreNivel;
                SceneManager.LoadScene(proximaEscena);
            }
            else
            {
                nivelTerminado = true;
                SceneManager.LoadScene("Victoria");  
                Destroy(gameObject);
            }
        }

        //MAS DE COSAS DE LA MUSICA AAAAAAAAAAAAAA
        private void ControlarReproduccionMusica(AudioClip cancionNivel)
        {
            if (audioSourceMúsica == null || cancionNivel == null) return; // si no hay audioSource o cancionNivel, returnea

            if (audioSourceMúsica != cancionNivel) // si la cancion actual no es la misma que la del nivel
            {
                audioSourceMúsica.clip = cancionNivel; // asigna la cancion del nivel al audioSource
                audioSourceMúsica.Play(); // reproduce la cancion del nivel
            }
            else if (!audioSourceMúsica.isPlaying) // si la cancion es la misma pero no se está reproduciendo
            {
                audioSourceMúsica.Play(); // reproduce la cancion del nivel
            }
        }

        public void EstablecerDerrota()
        {
            if (nivelTerminado) return;
            nivelTerminado = true;
            if (audioSourceMúsica != null) audioSourceMúsica.Stop(); // detiene la música si se está reproduciendo y se perdio el nivel
            SceneManager.LoadScene("Derrota");
        }

        public void ReiniciarNivelActual()
        {
                nivelTerminado = false;
                puntajeJugador = 0;
                // (usando ?. por seguridad por si es null)
                audioSourceMúsica?.Stop();
                string escenaActual = niveles[nivelActualIndice].nombreNivel;
                SceneManager.LoadScene(escenaActual);
        }

        public void RolJugadores(string rolP1, string rolP2)
        {
            rolBaseP1 = rolP1;
            rolBaseP2 = rolP2;
            Debug.Log($"Roles base registrados - P1: {rolP1} | P2: {rolP2}");
        }
}

