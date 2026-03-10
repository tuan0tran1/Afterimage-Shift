using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("LEVEL COMPLETE");
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Win();
            }
        }
    }
}
