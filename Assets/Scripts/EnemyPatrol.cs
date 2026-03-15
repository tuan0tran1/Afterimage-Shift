using UnityEngine;

public class EnemyPatrolFixedRange : MonoBehaviour
{
    [Header("Cấu hình di chuyển")]
    public float speed = 2f;
    public float patrolDistance = 3f; // Khoảng cách tuần tra sang mỗi bên

    private float startX;
    private int direction = 1;
    private SpriteRenderer sr;

    void Start()
    {
        // Lưu lại vị trí X ban đầu làm mốc trung tâm
        startX = transform.position.x;
        // Lấy SpriteRenderer để xử lý quay mặt (nếu có)
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Di chuyển kẻ địch theo hướng hiện tại (Giống hệt code cũ)
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        // Kiểm tra nếu vượt quá giới hạn bên phải
        if (transform.position.x >= startX + patrolDistance)
        {
            direction = -1;
            Flip();
        }
        // Kiểm tra nếu vượt quá giới hạn bên trái
        else if (transform.position.x <= startX - patrolDistance)
        {
            direction = 1;
            Flip();
        }
    }

    void Flip()
    {
        // Ưu tiên lật Sprite (đẹp hơn)
        if (sr != null)
        {
            sr.flipX = (direction == -1);
        }
        else
        {
            // Nếu không có SpriteRenderer thì dùng cách cũ của bạn (localScale)
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * direction;
            transform.localScale = scale;
        }
    }

    // Hàm này giúp PlayerController nhận diện va chạm tốt hơn
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Để trống vì logic Die() nằm ở PlayerController
    }
}