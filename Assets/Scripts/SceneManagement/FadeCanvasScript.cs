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
        Debug.Log("FadeFromBlack started.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleSpaceKeyPress();
        }
    }

    public void StartFadeOut(string sceneName = null)
    {
        sceneToLoad = sceneName;
        Debug.Log("StartFadeOut called. Scene to load: " + sceneToLoad);
        StartCoroutine(FadeToBlack());
    }

    private void HandleSpaceKeyPress()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log("Space key pressed. Current scene: " + currentScene);

        if (currentScene == "EndGameScene")
        {
            StartFadeOut("Scientific information");
        }
        else if (currentScene == "Scientific information")
        {
            StartFadeOut("SceneCredits");
        }
        else if (currentScene == "SceneCredits")
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

        Debug.Log("FadeToBlack completed. Scene to load: " + sceneToLoad);

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
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
        Debug.Log("FadeFromBlack completed.");
    }
}
