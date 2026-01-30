using UnityEngine;

public class EnemyPatrolFixedRange : MonoBehaviour
{
    public float speed = 2f;
    public float patrolDistance = 3f;

    private float startX;
    private int direction = 1;

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        if (transform.position.x >= startX + patrolDistance)
        {
            direction = -1;
            Flip();
        }
        else if (transform.position.x <= startX - patrolDistance)
        {
            direction = 1;
            Flip();
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }
}
