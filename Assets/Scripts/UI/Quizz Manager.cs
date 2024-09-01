using UnityEngine;
using TMPro;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    public Question[] questions;
    private int currentQuestionIndex = 0;

    public TextMeshProUGUI[] answerTexts;
    public GameObject[] spheres;

    public Rigidbody2D playerRigidbody; // Référence au Rigidbody2D du joueur
    public float repelForce = 10f; // Force de repoussement
    public float blockDuration = 0.5f; // Durée de blocage après repoussement

    private bool isBlocked = false; // Indique si le joueur est temporairement bloqué

    private float fadeDuration = 0.5f; // Durée du fade

    private void Start()
    {
        StartCoroutine(DisplayQuestionWithFade());
    }

    private IEnumerator DisplayQuestionWithFade()
    {
        // Fade out existing text and spheres
        yield return FadeOutAll();

        // Display new question or end quiz
        if (currentQuestionIndex < questions.Length)
        {
            DisplayQuestion();
            yield return FadeInAll();
        }
        else
        {
            StartCoroutine(HandleQuizEnd());
        }
    }

    private void DisplayQuestion()
    {
        Debug.Log("Affichage de la question : " + questions[currentQuestionIndex].questionText);
        for (int i = 0; i < answerTexts.Length; i++)
        {
            answerTexts[i].text = questions[currentQuestionIndex].answers[i];
            Debug.Log("Réponse " + i + ": " + questions[currentQuestionIndex].answers[i]);
        }
    }

    public void OnPlayerAnswer(int sphereIndex)
    {
        if (isBlocked) return; // Empêche l'action si le joueur est bloqué

        Debug.Log("Sphère touchée avec l'index : " + sphereIndex);
        if (sphereIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            Debug.Log("Bonne réponse!");
            currentQuestionIndex++;
            StartCoroutine(DisplayQuestionWithFade());
        }
        else
        {
            Debug.Log("Mauvaise réponse.");
            StartCoroutine(BlockAndRepelPlayer(spheres[sphereIndex].transform));
        }
    }

    private IEnumerator BlockAndRepelPlayer(Transform sphereTransform)
    {
        isBlocked = true;

        // Calculer la direction opposée à la sphère
        Vector2 repelDirection = (playerRigidbody.transform.position - sphereTransform.position).normalized;

        // Appliquer la force de repoussement
        playerRigidbody.AddForce(repelDirection * repelForce, ForceMode2D.Impulse);

        // Désactiver le mouvement du joueur pendant la durée du blocage
        var playerMovement = playerRigidbody.GetComponent<PlayerController>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Attendre la fin de la période de blocage
        yield return new WaitForSeconds(blockDuration);

        // Réactiver le mouvement du joueur
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        isBlocked = false;
    }

    private IEnumerator FadeOutAll()
    {
        float startAlphaText = answerTexts[0].color.a;
        float startAlphaSphere = spheres[0].GetComponent<SpriteRenderer>().color.a;

        // Fade out texts
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            Color newColorText = new Color(1, 1, 1, Mathf.Lerp(startAlphaText, 0, normalizedTime));
            foreach (var text in answerTexts)
            {
                text.color = newColorText;
            }

            // Fade out spheres
            Color newColorSphere = new Color(1, 1, 1, Mathf.Lerp(startAlphaSphere, 0, normalizedTime));
            foreach (var sphere in spheres)
            {
                var spriteRenderer = sphere.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = newColorSphere;
                }
            }
            yield return null;
        }

        // Ensure fully transparent
        foreach (var text in answerTexts)
        {
            text.color = new Color(1, 1, 1, 0);
        }
        foreach (var sphere in spheres)
        {
            var spriteRenderer = sphere.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = new Color(1, 1, 1, 0);
            }
        }
    }

    private IEnumerator FadeInAll()
    {
        Color startColorText = new Color(1, 1, 1, 0); // Texte complètement transparent
        Color endColorText = new Color(1, 1, 1, 1); // Texte complètement opaque

        Color startColorSphere = new Color(1, 1, 1, 0); // Sphères complètement transparentes
        Color endColorSphere = new Color(1, 1, 1, 1); // Sphères complètement opaques

        // Set initial color
        foreach (var text in answerTexts)
        {
            text.color = startColorText;
        }
        foreach (var sphere in spheres)
        {
            var spriteRenderer = sphere.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = startColorSphere;
            }
        }

        // Fade in texts and spheres
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            Color newColorText = Color.Lerp(startColorText, endColorText, normalizedTime);
            foreach (var text in answerTexts)
            {
                text.color = newColorText;
            }

            Color newColorSphere = Color.Lerp(startColorSphere, endColorSphere, normalizedTime);
            foreach (var sphere in spheres)
            {
                var spriteRenderer = sphere.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = newColorSphere;
                }
            }
            yield return null;
        }

        // Ensure fully opaque
        foreach (var text in answerTexts)
        {
            text.color = endColorText;
        }
        foreach (var sphere in spheres)
        {
            var spriteRenderer = sphere.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = endColorSphere;
            }
        }
    }

    private IEnumerator HandleQuizEnd()
    {
        // Fade out the last question and spheres
        yield return FadeOutAll();

        // Transition to the next scene
        SceneTransitionManager.Instance?.LoadNextScene();

        // Optionnel: Afficher un écran de fin ou un message final
        Debug.Log("Quiz Terminé! Affichage de l'écran final.");

        // Optionnel: Fade in end screen or final message
        // Vous pouvez ajouter ici un écran final avec une animation de fade-in.
    }
}
