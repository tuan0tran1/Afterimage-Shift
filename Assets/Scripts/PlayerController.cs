using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Transform respawnPoint;
    public AudioSource jumpSound;
    public ParticleSystem jumpEffect; // Thêm biến lưu hiệu ứng hình ảnh (bụi) khi nhảy

    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        if (animator != null)
        {
            animator.SetBool("isWalking", Mathf.Abs(move) > 0.1f);
            animator.SetBool("isJumping", !isGrounded);
        }

        if (spriteRenderer != null && Mathf.Abs(move) > 0.01f)
        {
            spriteRenderer.flipX = move < 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            if (jumpSound != null) jumpSound.Play();
            if (jumpEffect != null) jumpEffect.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (collision.contacts[0].normal.y > 0.5f)
                isGrounded = true;
        }
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trap"))
        {
            Die();
        }
    }

    void Die()
    {
        rb.linearVelocity = Vector2.zero;
        GameManager.Instance.Lose();
    }
    void Jump()
    {
        if (jumpSound != null) jumpSound.Play();
        if (jumpEffect != null) jumpEffect.Play();
    }

}
