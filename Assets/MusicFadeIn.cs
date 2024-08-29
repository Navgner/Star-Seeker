using UnityEngine;
using System.Collections; // Ajoutez cette ligne pour utiliser IEnumerator

public class MusicFadeIn : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeInDuration = 5f; // Durée du fade-in en secondes

    private void Start()
    {
        if (audioSource != null)
        {
            audioSource.volume = 0; // Commencer avec le volume à 0
            StartCoroutine(FadeInMusic());
        }
    }

    private IEnumerator FadeInMusic()
    {
        float startVolume = 0f;
        float targetVolume = 1f; // Volume final
        float elapsedTime = 0f;

        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / fadeInDuration);
            yield return null;
        }

        audioSource.volume = targetVolume; // Assurez-vous que le volume est à la valeur finale
    }
}
