using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2f;
    public AudioSource audioSource; // Assurez-vous d'assigner ce composant dans l'inspecteur
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip endMusic;

    private void Start()
    {
        if (fadeImage == null)
        {
            fadeImage = GetComponent<Image>();
        }
        fadeImage.color = new Color(0, 0, 0, 1);
        StartCoroutine(FadeFromBlack());
    }

    public void StartFadeOut(string sceneName, AudioClip newMusic = null)
    {
        StartCoroutine(FadeToBlack(sceneName, newMusic));
    }

    private IEnumerator FadeToBlack(string sceneName, AudioClip newMusic = null)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        // Fade out effect
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // Load the new scene
        SceneManager.LoadScene(sceneName);

        // Ensure scene is fully loaded before updating the music
        yield return new WaitForSeconds(0.1f);

        // Stop current music if needed
        if (newMusic != null)
        {
            StartCoroutine(FadeAudioSource.StartFade(audioSource, fadeDuration, 0f));
            yield return new WaitForSeconds(fadeDuration); // Wait for fade-out to complete

            // Update to the new music
            audioSource.clip = newMusic;
            audioSource.Play();
            StartCoroutine(FadeAudioSource.StartFade(audioSource, fadeDuration, 1f));
        }
        else
        {
            // If no new music, just ensure old music fades out
            yield return new WaitForSeconds(fadeDuration); // Wait for fade-out to complete
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

    private void Update()
    {
        // Gérer les transitions spécifiques par les touches de l'utilisateur
        if (SceneManager.GetActiveScene().name == "EndGameScene" && Input.GetKeyDown(KeyCode.Space))
        {
            StartFadeOut("SceneCredits", menuMusic);
        }
        else if (SceneManager.GetActiveScene().name == "SceneCredits" && Input.GetKeyDown(KeyCode.Space))
        {
            StartFadeOut("MainMenu", menuMusic);
        }
    }
}
