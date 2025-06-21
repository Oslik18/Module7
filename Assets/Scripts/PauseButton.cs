using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject pausePanel;

    void Start()
    {
        pausePanel.SetActive(true);
    }

    void Update()
    {
        GamePause();
    }

    private void GamePause()
    {
        if (pausePanel.activeSelf)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
