using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerRecorder player;
    public GameObject ghostPrefab;
    public Transform respawnPoint;

    private float timer = 20f;
    private bool secondRun = false;

    public TextMeshProUGUI timerText; // Added for Timer UI
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
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            
            // Update the Timer UI every frame
            if (timerText != null)
            {
                timerText.text = "Time: " + Mathf.Max(0, Mathf.Ceil(timer)).ToString();
            }
        }

        if (timer <= 0)
        {
            if (!secondRun)
            {
                StartSecondRun();
            }
        }
    }
    void StartSecondRun()
    {
        secondRun = true;

        List<Vector3> record = player.GetPositions();

        GameObject ghost = Instantiate(
            ghostPrefab,
            respawnPoint.position,
            Quaternion.identity
        );

        ghost.GetComponent<GhostReplay>().StartReplay(record);

        player.transform.position = respawnPoint.position;
        player.ClearPositions(); // Quan trọng: Phải xóa lịch sử để chạy lại

        timer = 20f;
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
