using UnityEngine;
using TMPro;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    public Question[] questions;
    private int currentQuestionIndex = 0;

    public TextMeshProUGUI[] answerTexts;
    public GameObject[] spheres;

    private float fadeDuration = 0.5f; // Durée du fade

    private void Start()
    {
        StartCoroutine(DisplayQuestionWithFade());
    }

    private IEnumerator DisplayQuestionWithFade()
    {
        // Fade out existing text and spheres
        yield return FadeOutAll();

        // Display new question
        DisplayQuestion();

        // Fade in new text and spheres
        yield return FadeInAll();
    }

    private void DisplayQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            Debug.Log("Affichage de la question : " + questions[currentQuestionIndex].questionText);
            for (int i = 0; i < answerTexts.Length; i++)
            {
                answerTexts[i].text = questions[currentQuestionIndex].answers[i];
                Debug.Log("Réponse " + i + ": " + questions[currentQuestionIndex].answers[i]);
            }
        }
        else
        {
            Debug.Log("Quiz Terminé!");
        }
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

    public void OnPlayerAnswer(int sphereIndex)
    {
        Debug.Log("Sphère touchée avec l'index : " + sphereIndex);
        if (sphereIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            Debug.Log("Bonne réponse!");
        }
        else
        {
            Debug.Log("Mauvaise réponse.");
        }

        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Length)
        {
            StartCoroutine(DisplayQuestionWithFade());
        }
        else
        {
            Debug.Log("Quiz Terminé!");
        }
    }
}
