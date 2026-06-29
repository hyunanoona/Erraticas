using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ConfigNivelEscena
{
    public string nombreNivel;
    public int puntajeParaGanar;  
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



        private void Awake()
        {
            if (Instance != null) 
            { 
                Destroy(gameObject); 
                return;
            }
            else { Instance = this; DontDestroyOnLoad(gameObject); }
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
            if (escena.name != "MenuInicial" && escena.name != "MenuSeleccion" && escena.name != "SeleccionRol")
            {
                SpawnearJugadores();
            }
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

            GameObject prefabP1 = BuscarPrefabPorNombre(personajeFinalP1);
            if (prefabP1 != null) Instantiate(prefabP1, posP1, Quaternion.identity);

            GameObject prefabP2 = BuscarPrefabPorNombre(personajeFinalP2);
            if (prefabP2 != null) Instantiate(prefabP2, posP2, Quaternion.identity);

            UIManager uiManagement = Object.FindFirstObjectByType<UIManager>();

            if (uiManagement != null)
            {
                uiManagement.ConfigurarInterfazNvl(tipoSeleccionadoP1, tipoSeleccionadoP2);
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
            puntajeJugador += puntos;
            ChequearMetaNivel();
            //  ⤷ ゛Actualizar UI de puntaje, sonidos, animación. ˎˊ˗
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
                puntajeJugador = 0;
                string proximaEscena = niveles[nivelActualIndice].nombreNivel;
                SceneManager.LoadScene(proximaEscena);
            }
            else
            {
                SceneManager.LoadScene("MenuInicial");  //  ⤷ ゛CAMBIAR POR ESCENA DE VICTORIA!!!!! ˎˊ˗
                Destroy(gameObject);
            }
        }

        public void RolJugadores(string rolP1, string rolP2)
        {
            rolBaseP1 = rolP1;
            rolBaseP2 = rolP2;
            Debug.Log($"Roles base registrados - P1: {rolP1} | P2: {rolP2}");
        }

}

