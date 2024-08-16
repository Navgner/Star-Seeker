using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    public FadeController fadeController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (fadeController != null)
            {
                fadeController.StartFadeOut();
            }
        }
    }
}
