using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject door;
    public MovingPlatform platform;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ghost"))
        {
            if (door != null) door.SetActive(false);
            if (platform != null) platform.isActivated = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ghost"))
        {
            if (door != null) door.SetActive(true);
            if (platform != null) platform.isActivated = false;
        }
    }
}