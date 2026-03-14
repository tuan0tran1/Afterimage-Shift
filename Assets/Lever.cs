using UnityEngine;

public class Lever : MonoBehaviour
{
    public MovingPlatform platform;
    private bool isActivated = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActivated && (other.CompareTag("Player") || other.CompareTag("Ghost")))
        {
            isActivated = true;
            if (platform != null)
            {
                platform.isActivated = true;
            }
            // Đổi màu để nhận biết cần gạt đã được kéo
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = Color.green; 
            }
        }
    }
}
