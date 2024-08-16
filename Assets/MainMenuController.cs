using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public FadeController fadeController;
    public Button playButton;

    private void Start()
    {
        playButton.onClick.AddListener(() =>
            fadeController.StartFadeOut("Testing 1", fadeController.gameMusic)); // Musique du jeu
    }
}
