using UnityEngine;

public class ControlledPlatform : MonoBehaviour
{
    [Header("Movement Points")]
    public Transform pointA; // Vị trí mặc định (bờ trái)
    public Transform pointB; // Vị trí đích (bờ phải)

    [Header("Settings")]
    public float speed = 3f;
    public bool isActivated = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Ép Rigidbody về Kinematic để chạy mượt và không bị trọng lực kéo
        rb.bodyType = RigidbodyType2D.Kinematic;

        // Đặt vị trí ban đầu tại Point A
        if (pointA != null) transform.position = pointA.position;
    }

    void FixedUpdate()
    {
        if (pointA == null || pointB == null) return;

        // Quyết định mục tiêu: Nếu kích hoạt thì đi tới B, ngược lại lùi về A
        Vector3 targetPos = isActivated ? pointB.position : pointA.position;

        // Di chuyển bằng Rigidbody (giúp kéo theo Player/Ghost đứng trên nó)
        Vector2 nextPos = Vector2.MoveTowards(rb.position, targetPos, speed * Time.fixedDeltaTime);
        rb.MovePosition(nextPos);
    }
}