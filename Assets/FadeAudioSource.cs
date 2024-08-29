using System.Collections;
using UnityEngine;

public static class FadeAudioSource
{
    public static IEnumerator StartFade(AudioSource audioSource, float fadeDuration, float targetVolume)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume != targetVolume)
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume, startVolume * Time.deltaTime / fadeDuration);
            yield return null;
        }

        if (targetVolume == 0)
        {
            audioSource.Stop();
        }
    }
}
