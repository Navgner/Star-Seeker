using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // R�f�rence au TextMeshPro dans le Canvas
    public float baseDisplayTime = 3f; // Dur�e de base pendant laquelle le texte est affich�
    public float timePerCharacter = 0.05f; // Temps d'affichage par caract�re

    private Coroutine currentDialogueCoroutine;

    void Start()
    {
        dialogueText.text = ""; // Assure-toi que le texte est vide au d�part
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

        // Calculer la dur�e d'affichage en fonction de la longueur du texte
        float displayTime = baseDisplayTime + message.Length * timePerCharacter;

        yield return new WaitForSeconds(displayTime);
        dialogueText.text = "";
    }
}
