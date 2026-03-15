using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject door;
    public MovingPlatform platform;

    // Biến đếm số lượng đối tượng (Player/Ghost) đang đứng trên Switch
    private int occupantsCount = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ghost"))
        {
            occupantsCount++; // Tăng thêm 1 người khi có ai đó bước vào
            UpdateSwitchState();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ghost"))
        {
            occupantsCount--; // Giảm đi 1 khi có ai đó rời đi
            UpdateSwitchState();
        }
    }

    void UpdateSwitchState()
    {
        // Chỉ khi không còn ai đứng trên Switch (count == 0) thì cửa mới đóng lại
        if (occupantsCount > 0)
        {
            if (door != null) door.SetActive(false); // Mở cửa
            if (platform != null) platform.isActivated = true;
        }
        else
        {
            if (door != null) door.SetActive(true); // Đóng cửa
            if (platform != null) platform.isActivated = false;
        }
    }
}