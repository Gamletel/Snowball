using UnityEngine;

public class LosePanel : MonoBehaviour
{
    public delegate void PlayerLose();
    public static event PlayerLose playerLose;

    private void Awake()
    {
        playerLose += EnablePanel;
        gameObject.SetActive(false);
    }

    private void EnablePanel()
    {
        gameObject.SetActive(true);
    }

    public static void OnPlayerLose()
    {
        playerLose?.Invoke();
    }
}
