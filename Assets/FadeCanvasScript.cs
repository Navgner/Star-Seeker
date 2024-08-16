using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2f;
    public string sceneToLoad;

    private void Start()
    {
        if (fadeImage == null)
        {
            fadeImage = GetComponent<Image>();
        }
        fadeImage.color = new Color(0, 0, 0, 1);
        StartCoroutine(FadeFromBlack());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // Chargement de la scène
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);

            // Assure-toi que la musique de la nouvelle scène est jouée
            if (sceneToLoad == "MainMenu")
            {
                MusicManager.Instance.PlayMusic(MusicManager.Instance.menuMusic, fadeDuration);
            }
            else if (sceneToLoad == "GameScene")
            {
                MusicManager.Instance.PlayMusic(MusicManager.Instance.gameMusic, fadeDuration);
            }
            else if (sceneToLoad == "EndScene")
            {
                MusicManager.Instance.PlayMusic(MusicManager.Instance.endMusic, fadeDuration);
            }
        }
        else
        {
            Debug.LogWarning("Scene name is not specified in FadeController.");
        }
    }

    private IEnumerator FadeFromBlack()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = 1 - Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 0);
    }
}
