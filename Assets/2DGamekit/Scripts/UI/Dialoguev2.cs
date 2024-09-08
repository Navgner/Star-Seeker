using UnityEngine;
using System.Collections; // Add this line to include the IEnumerator type

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public float displayDuration = 5f; // Duration in seconds the text should be visible

    private void OnEnable()
    {
        StartCoroutine(HideAfterDelay(displayDuration));
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Update the position of the Text to follow the player with an offset
            transform.position = playerTransform.position + offset;
        }
    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
