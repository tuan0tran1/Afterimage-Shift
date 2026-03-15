using UnityEngine;

public class Lever : MonoBehaviour
{
    public ControlledPlatform platform; // Kéo platform mới vào đây
    private int occupantsCount = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ghost"))
        {
            occupantsCount++;
            if (platform != null) platform.isActivated = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ghost"))
        {
            occupantsCount--;
            // Chỉ khi không còn ai đứng trên Lever mới tắt kích hoạt
            if (occupantsCount <= 0)
            {
                occupantsCount = 0; // Đảm bảo không bị âm
                if (platform != null) platform.isActivated = false;
            }
        }
    }
}