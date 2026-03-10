using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Pause UI")]
    public GameObject pausePanel;

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;

        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentLevel + 1;

        PlayerPrefs.SetInt("UnlockedLevel", nextLevel);
        PlayerPrefs.Save();

        SceneManager.LoadScene(nextLevel);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
        PlayerPrefs.DeleteAll();
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    public void LoadLevel(int levelIndex)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelIndex);
    }

    public void OpenIntroduction()
    {
        SceneManager.LoadScene("Introduction");
    }
}
