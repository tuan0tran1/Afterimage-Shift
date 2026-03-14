using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerRecorder player;
    public GameObject ghostPrefab;
    public Transform respawnPoint;

    public float timeLimit = 25f; // Thời gian tối đa của mỗi Turn
    private float timer;
    private bool secondRun = false;
    private Vector3 initialSpawnPosition;

    public TextMeshProUGUI timerText; // Added for Timer UI
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Hình ảnh & Âm Thanh")]
    public AudioSource winSound;
    public AudioSource loseSound;
    public GameObject winEffectPrefab; // Kéo thả Particle System (Prefab) hiệu ứng Win vào đây
    public GameObject loseEffectPrefab; // Kéo thả Particle System (Prefab) hiệu ứng Lose/Die vào đây

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        timer = timeLimit; // Khởi tạo timer
        Time.timeScale = 1f;
        winPanel.SetActive(false);
        losePanel.SetActive(false);

        if (player != null)
        {
            initialSpawnPosition = player.transform.position;
        }
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
            else
            {
                // Run 2 cũng hết giờ -> Trở về điểm xuất phát (Reload)
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            }
        }
    }
    void StartSecondRun()
    {
        secondRun = true;

        List<Vector3> record = player.GetPositions();

        // 1. Ép buộc lấy toạ độ Player lúc đầu game, không dùng RespawnPoint để tránh lỗi RespawnPoint di chuyển theo Player
        Vector3 spawnPos = initialSpawnPosition;

        // 2. Spawn Ghost
        GameObject ghost = Instantiate(
            ghostPrefab,
            spawnPos,
            Quaternion.identity
        );

        ghost.GetComponent<GhostReplay>().StartReplay(record);

        // 3. Đưa Player về đích an toàn tuyệt đối
        player.transform.SetParent(null); // Thoát khỏi tấm ván di chuyển nếu đang đứng trên đó
        player.transform.position = spawnPos;
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; // Xóa gia tốc rơi/di chuyển để đứng yên
        player.ClearPositions(); // Quan trọng: Phải xóa lịch sử để chạy lại

        timer = timeLimit;
    }

    public void Win()
    {
        if (winSound != null) winSound.Play();
        if (winEffectPrefab != null && player != null) Instantiate(winEffectPrefab, player.transform.position, Quaternion.identity);

        Time.timeScale = 0f;
        winPanel.SetActive(true);
    }

    public void Lose()
    {
        if (loseSound != null) loseSound.Play();
        if (loseEffectPrefab != null && player != null) Instantiate(loseEffectPrefab, player.transform.position, Quaternion.identity);
        
        if (losePanel != null) losePanel.SetActive(true); // Tạm hiện bảng Lose 

        // Trì hoãn 1 giây để nghe hết âm thanh và xem hình ảnh máu/khói văng ra rồi mới Reset màn chơi
        Invoke("RestartLevel", 1f);
    }

    private void RestartLevel()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
