using UnityEngine;
using UnityEngine.UI;

public class TutorialEnPausa : MonoBehaviour
{
    [Header("⁺‧₊˚ ཐི⋆ Botón de Regreso ⋆ཋྀ ˚₊‧⁺")]
    [SerializeField] private Button botonVolverAlMenuPausa;

        private MenuPausa menuPausaPrincipal;

        private void Start()
        {
            if (botonVolverAlMenuPausa != null)
            {
                botonVolverAlMenuPausa.onClick.AddListener(Volver);
            }
        }

        public void ConfigurarMenuPausa(MenuPausa menu)
        {
            menuPausaPrincipal = menu;
        }

        private void Volver()
        {
            if (menuPausaPrincipal != null)
            {
                menuPausaPrincipal.ActivarMenuDesdeTutorial();
            }
        }
}
