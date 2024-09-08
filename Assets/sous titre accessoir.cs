using UnityEngine;
using TMPro;

public class AdjustTextSize : MonoBehaviour
{
    public TextMeshProUGUI textElement;
    public RectTransform container; // Référence au RectTransform du conteneur
    public float padding = 10f; // Marge pour éviter que le texte touche les bords

    void Start()
    {
        if (textElement != null && container != null)
        {
            AdjustFontSize();
        }
    }

    void AdjustFontSize()
    {
        // Obtenir les dimensions du conteneur
        float containerWidth = container.rect.width - padding * 2;
        float containerHeight = container.rect.height - padding * 2;

        // Obtenir le texte
        string text = textElement.text;

        // Ajuster la taille de la police
        textElement.enableAutoSizing = false; // Désactiver le redimensionnement automatique
        textElement.fontSize = CalculateOptimalFontSize(text, containerWidth, containerHeight);
    }

    float CalculateOptimalFontSize(string text, float containerWidth, float containerHeight)
    {
        float fontSize = 1f; // Taille initiale de la police
        textElement.fontSize = fontSize;

        // Estimer la largeur et la hauteur du texte à la taille de police actuelle
        Vector2 textSize = textElement.GetPreferredValues(text);

        while (textSize.x < containerWidth && textSize.y < containerHeight)
        {
            fontSize += 1f;
            textElement.fontSize = fontSize;
            textSize = textElement.GetPreferredValues(text);

            // Arrêter lorsque la taille du texte dépasse les dimensions du conteneur
            if (textSize.x >= containerWidth || textSize.y >= containerHeight)
            {
                break;
            }
        }

        // Réduire légèrement la taille pour s'assurer que le texte reste dans les limites
        return fontSize - 1f;
    }
}
