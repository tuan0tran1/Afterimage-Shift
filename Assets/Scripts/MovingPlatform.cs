using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public bool isActivated = false;

    private Transform currentTarget;

    void Start()
    {
        currentTarget = pointB;
    }

    void Update()
    {
        if (isActivated && pointA != null && pointB != null)
        {
            // Di chuyển về phía mục tiêu
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

            // Nếu đến gần điểm hiện tại thì quay đầu
            if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
            {
                if (currentTarget == pointA)
                {
                    currentTarget = pointB;
                }
                else
                {
                    currentTarget = pointA;
                }
            }
        }
    }

    // Làm cho Player gắn vào Platform để không bị trôi
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ghost"))
        {
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ghost"))
        {
            collision.transform.SetParent(null);
        }
    }
}
