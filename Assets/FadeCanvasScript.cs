using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2f;
    public string sceneToLoad;
    public AudioClip musicClip;

    private void Start()
    {
        if (fadeImage == null)
        {
            fadeImage = GetComponent<Image>();
        }
        fadeImage.color = new Color(0, 0, 0, 1);
        StartCoroutine(FadeFromBlack());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleSpaceKeyPress();
        }
    }

    public void StartFadeOut(string sceneName = null, AudioClip newMusic = null)
    {
        sceneToLoad = sceneName;
        musicClip = newMusic;
        StartCoroutine(FadeToBlack());
    }

    private void HandleSpaceKeyPress()
    {
        if (SceneManager.GetActiveScene().name == "EndGameScene")
        {
            StartFadeOut("SceneCredits", null);
        }
        else if (SceneManager.GetActiveScene().name == "SceneCredits")
        {
            StartFadeOut("MainMenu");
        }
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

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);

            if (sceneToLoad != "Testing 1" && musicClip != null)
            {
                AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
                foreach (var source in audioSources)
                {
                    if (source.gameObject.scene.name == sceneToLoad)
                    {
                        source.clip = musicClip;
                        source.Play();
                    }
                }
            }
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
