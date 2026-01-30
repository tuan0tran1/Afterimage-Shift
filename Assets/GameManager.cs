using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int totalCoins;
    private int currentCoins;

    public TextMeshProUGUI coinText;
    public GameObject winPanel;
    public GameObject losePanel;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        Time.timeScale = 1f;
        UpdateUI();
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    public void AddScore(int value)
    {
        currentCoins += value;
        UpdateUI();

        if (currentCoins >= totalCoins)
        {
            Win();
        }
    }

    void UpdateUI()
    {
        coinText.text = currentCoins + " / " + totalCoins;
    }

    public void Win()
    {
        Time.timeScale = 0f;
        winPanel.SetActive(true);
    }

    public void Lose()
    {
        Time.timeScale = 0f;
        losePanel.SetActive(true);
    }
}
