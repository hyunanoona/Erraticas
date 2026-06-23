using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ConfigNivelEscena
{
    public string nombreNivel;
    public int puntajeParaGanar;  
}

public class GameManager : MonoBehaviour
{

    /*
    GameManager ᝰ
    ಄ Se encarga de la gestión de los niveles (pasar de un escenario a otro, cargar el siguiente nivel), actualiza el estado del juego, controla el puntaje de los jugadores, 
	಄ Conecta las animaciones y los audios con los eventos del juego, y maneja la condición de victoria o derrota. 
    */

    [Header(". ݁₊ ⊹ . ݁ Referencias y Variables  ݁ . ⊹ ₊ ݁.")]
        public static GameManager Instance;

        [SerializeField] private List<ConfigNivelEscena> niveles; 
        public int nivelActualIndice = 0;
        public int puntajeJugador = 0;        

        public string ratita1Rol;
        public string ratita2Rol;


        private void Awake()
        {
            //Esto permite que sea una instancia unica y no haya mas de dos.
            if (Instance!=null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void SumarPuntos(int puntos)
        {
            puntajeJugador += puntos;
            ChequearMetaNivel();
            // Actualizar UI de puntaje, sonidos, animación.
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
                SceneManager.LoadScene("MenuInicial");  // CAMBIAR POR ESCENA DE VICTORIA!!!!!
                Destroy(gameObject);
            }
        }

    public void RolJugadores(string rol1, string rol2)
        {
            ratita1Rol = rol1;
            ratita2Rol = rol2;
        }

}

