using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Référence au TextMeshPro dans le Canvas
    public float baseDisplayTime = 3f; // Durée de base pendant laquelle le texte est affiché
    public float timePerCharacter = 0.05f; // Temps d'affichage par caractère

    private Coroutine currentDialogueCoroutine;

    void Start()
    {
        dialogueText.text = ""; // Assure-toi que le texte est vide au départ
    }

    public void ShowDialogue(string message)
    {
        if (currentDialogueCoroutine != null)
        {
            StopCoroutine(currentDialogueCoroutine);
        }
        currentDialogueCoroutine = StartCoroutine(DisplayDialogue(message));
    }

    private IEnumerator DisplayDialogue(string message)
    {
        dialogueText.text = message;

        // Calculer la durée d'affichage en fonction de la longueur du texte
        float displayTime = baseDisplayTime + message.Length * timePerCharacter;

        yield return new WaitForSeconds(displayTime);
        dialogueText.text = "";
    }
}
