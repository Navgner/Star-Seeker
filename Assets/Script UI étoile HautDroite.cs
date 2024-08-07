using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text collectibleText;

    public void UpdateCollectibleCount(int collected, int total)
    {
        collectibleText.text = collected.ToString() + "/" + total.ToString();
    }
}
