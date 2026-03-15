using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Core References")]
    public PlayerRecorder player;    // Script ghi lại hành trình của Player
    public GameObject ghostPrefab;   // Prefab của bóng ma
    public TextMeshProUGUI timerText;

    [Header("Level Settings")]
    public float timeLimit = 25f;
    private float timer;
    private Vector3 initialSpawnPosition;

    [Header("UI Panels")]
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Effects & Sound")]
    public AudioSource winSound;
    public AudioSource loseSound;
    public GameObject winEffectPrefab;
    public GameObject resetEffectPrefab; // Hiệu ứng khi reset vòng lặp

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        timer = timeLimit;
        Time.timeScale = 1f;

        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);

        if (player != null)
        {
            initialSpawnPosition = player.transform.position;
        }
    }

    void Update()
    {
        // Hệ thống đếm ngược
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timerText != null)
            {
                timerText.text = "Time: " + Mathf.Max(0, Mathf.Ceil(timer)).ToString();
            }
        }
        else
        {
            // Khi hết giờ -> Bắt đầu vòng mới với bóng mới
            StartNewRound();
        }
    }

    void StartNewRound()
    {
        // 1. DỌN DẸP: Xóa tất cả bóng cũ (chỉ giữ 1 bóng duy nhất cho lượt mới)
        GameObject[] oldGhosts = GameObject.FindGameObjectsWithTag("Ghost");
        foreach (GameObject g in oldGhosts) Destroy(g);

        // 2. TẠO BÓNG: Lấy dữ liệu từ lượt vừa rồi của Player
        List<Vector3> history = player.GetPositions();

        if (history != null && history.Count > 0)
        {
            // Sinh bóng tại vị trí bắt đầu của lịch sử lượt trước
            GameObject newGhost = Instantiate(ghostPrefab, history[0], Quaternion.identity);

            // Đồng bộ kích thước (tránh lỗi nhỏ người ở màn 3)
            newGhost.transform.localScale = player.transform.localScale;
            newGhost.tag = "Ghost";

            // Truyền dữ liệu cho script Replay của Ghost
            // Giả sử script của bạn tên là GhostReplay hoặc GhostController
            var ghostScript = newGhost.GetComponent<GhostReplay>();
            if (ghostScript != null)
            {
                ghostScript.StartReplay(history);
            }
        }

        // 3. RESET PLAYER: Đưa về vạch xuất phát
        ResetPlayerToStart();

        // 4. HIỆU ỨNG: Chớp màn hình hoặc Particle khi reset thời gian
        if (resetEffectPrefab != null)
            Instantiate(resetEffectPrefab, initialSpawnPosition, Quaternion.identity);

        // 5. KHỞI ĐỘNG LẠI TIMER
        timer = timeLimit;
    }

    private void ResetPlayerToStart()
    {
        if (player == null) return;

        player.transform.SetParent(null); // Thoát khỏi các Platform di động
        player.transform.position = initialSpawnPosition;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // Triệt tiêu lực quán tính
        }

        player.ClearPositions(); // Quan trọng: Xóa lịch sử cũ để ghi lại lượt mới
    }

    public void Win()
    {
        if (winSound != null) winSound.Play();
        if (winEffectPrefab != null) Instantiate(winEffectPrefab, player.transform.position, Quaternion.identity);

        Time.timeScale = 0f; // Dừng game
        if (winPanel != null) winPanel.SetActive(true);
    }

    public void Lose()
    {
        if (loseSound != null) loseSound.Play();
        // Hiện bảng Lose hoặc Reset toàn bộ Scene (Tùy bạn chọn)
        if (losePanel != null) losePanel.SetActive(true);

        // Chờ 1 giây rồi load lại toàn bộ Scene (xóa sạch Ghost khi chết thực sự)
        Invoke("RestartLevel", 1f);
    }

    private void RestartLevel()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void AddTime(float seconds)
    {
        timer += seconds;

        // Bạn có thể thêm code cập nhật UI ở đây để người chơi thấy số giây nhảy lên
        // uiManager.ShowTimeBonusEffect();
    }
}