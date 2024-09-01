using UnityEngine;
using TMPro;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    public Question[] questions;
    private int currentQuestionIndex = 0;

    public TextMeshProUGUI[] answerTexts;
    public GameObject[] spheres;

    public Rigidbody2D playerRigidbody; // R�f�rence au Rigidbody2D du joueur
    public float repelForce = 10f; // Force de repoussement
    public float blockDuration = 0.5f; // Dur�e de blocage apr�s repoussement

    private bool isBlocked = false; // Indique si le joueur est temporairement bloqu�

    private float fadeDuration = 0.5f; // Dur�e du fade

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
            Debug.Log("R�ponse " + i + ": " + questions[currentQuestionIndex].answers[i]);
        }
    }

    public void OnPlayerAnswer(int sphereIndex)
    {
        if (isBlocked) return; // Emp�che l'action si le joueur est bloqu�

        Debug.Log("Sph�re touch�e avec l'index : " + sphereIndex);
        if (sphereIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            Debug.Log("Bonne r�ponse!");
            currentQuestionIndex++;
            StartCoroutine(DisplayQuestionWithFade());
        }
        else
        {
            Debug.Log("Mauvaise r�ponse.");
            StartCoroutine(BlockAndRepelPlayer(spheres[sphereIndex].transform));
        }
    }

    private IEnumerator BlockAndRepelPlayer(Transform sphereTransform)
    {
        isBlocked = true;

        // Calculer la direction oppos�e � la sph�re
        Vector2 repelDirection = (playerRigidbody.transform.position - sphereTransform.position).normalized;

        // Appliquer la force de repoussement
        playerRigidbody.AddForce(repelDirection * repelForce, ForceMode2D.Impulse);

        // D�sactiver le mouvement du joueur pendant la dur�e du blocage
        var playerMovement = playerRigidbody.GetComponent<PlayerController>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Attendre la fin de la p�riode de blocage
        yield return new WaitForSeconds(blockDuration);

        // R�activer le mouvement du joueur
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
        Color startColorText = new Color(1, 1, 1, 0); // Texte compl�tement transparent
        Color endColorText = new Color(1, 1, 1, 1); // Texte compl�tement opaque

        Color startColorSphere = new Color(1, 1, 1, 0); // Sph�res compl�tement transparentes
        Color endColorSphere = new Color(1, 1, 1, 1); // Sph�res compl�tement opaques

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

        // Optionnel: Afficher un �cran de fin ou un message final
        Debug.Log("Quiz Termin�! Affichage de l'�cran final.");

        // Optionnel: Fade in end screen or final message
        // Vous pouvez ajouter ici un �cran final avec une animation de fade-in.
    }
}
