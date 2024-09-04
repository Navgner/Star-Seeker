using System.Collections;
using UnityEngine;
using TMPro; // Importez le namespace pour TextMeshPro

namespace YourGameNamespace.UI
{
    public class DialogueManager : MonoBehaviour
    {
        public TextMeshProUGUI dialogueText; // R�f�rence au composant TextMeshProUGUI pour afficher le dialogue
        public float typingSpeed = 0.05f; // Vitesse de d�filement du texte

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
                StopAllCoroutines(); // Arr�te les coroutines en cours
                StartCoroutine(TypeSentence(text)); // D�marre la coroutine pour afficher le texte
            }
        }

        private IEnumerator TypeSentence(string sentence)
        {
            dialogueText.text = ""; // R�initialise le texte affich�
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed); // Attends un petit moment entre chaque lettre
            }
        }
    }
}
