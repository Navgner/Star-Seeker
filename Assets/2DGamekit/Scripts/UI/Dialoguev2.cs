// 8/7/2024 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;

    void Update()
    {
        if (playerTransform != null)
        {
            // Update the position of the Text to follow the player with an offset
            transform.position = playerTransform.position + offset;
        }
    }
}