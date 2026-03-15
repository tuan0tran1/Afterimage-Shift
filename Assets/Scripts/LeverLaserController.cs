using UnityEngine;

public class LeverLaserController : MonoBehaviour
{
    [Header("Laser Settings")]
    public GameObject laserObject; // Kéo đối tượng chứa tia Laser (và Collider của nó) vào đây

    private SpriteRenderer spriteRenderer;
    private int occupantsCount = 0; // Đếm số lượng người (Player/Ghost) đang đứng trên cần gạt

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu là Player hoặc Ghost bước vào vùng kích hoạt
        if (other.CompareTag("Player") || other.CompareTag("Ghost"))
        {
            occupantsCount++;
            UpdateStatus();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ghost"))
        {
            occupantsCount--;
            // Đảm bảo đếm không bị âm nếu có lỗi vật lý
            if (occupantsCount < 0) occupantsCount = 0;
            UpdateStatus();
        }
    }

    private void UpdateStatus()
    {
        // Nếu có ít nhất 1 người đang đứng trên cần gạt
        bool isPressed = occupantsCount > 0;

        if (laserObject != null)
        {
            // Laser TẮT khi cần gạt bị ĐÈ (isPressed = true)
            laserObject.SetActive(!isPressed);
        }
    }
}