using System.Collections;
using UnityEngine;
using TMPro; // Importez le namespace pour TextMeshPro

namespace YourGameNamespace.UI
{
    public class DialogueManager : MonoBehaviour
    {
        public TextMeshProUGUI dialogueText; // Référence au composant TextMeshProUGUI pour afficher le dialogue
        public float typingSpeed = 0.05f; // Vitesse de défilement du texte

        private void Start()
        {
            if (dialogueText != null)
            {
                dialogueText.text = ""; // Initialise le texte comme vide
            }
        }

        public void ShowDialogue(string text)
        {
            if (dialogueText != null)
            {
                StopAllCoroutines(); // Arrête les coroutines en cours
                StartCoroutine(TypeSentence(text)); // Démarre la coroutine pour afficher le texte
            }
        }

        private IEnumerator TypeSentence(string sentence)
        {
            dialogueText.text = ""; // Réinitialise le texte affiché
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed); // Attends un petit moment entre chaque lettre
            }
        }
    }
}
