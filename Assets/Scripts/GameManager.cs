using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    /*
    GameManager ᝰ
    ಄ Se encarga de las funcionalidades básicas del juego (Menú, Pausa) y sus niveles (pasar de un nivel a otro, guardar las partidas).
	಄ Conecta las animaciones y los audios con los eventos del juego. 
    */

    [Header(". ݁₊ ⊹ . ݁ Referencias y Variables  ݁ . ⊹ ₊ ݁.")]
        public static GameManager Instance;

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

}
}
