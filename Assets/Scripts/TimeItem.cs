using UnityEngine;

public class TimeItem : MonoBehaviour
{
    public float timeToAdd = 5f; // Số giây được cộng thêm
    //public GameObject collectEffect; // Hiệu ứng hạt (tùy chọn)

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Chỉ Player thật mới ăn được thời gian (Ghost không ăn được)
        if (other.CompareTag("Player"))
        {
            // Gọi hàm cộng thời gian trong GameManager
            GameManager.Instance.AddTime(timeToAdd);

            // Tạo hiệu ứng nếu có
            //if (collectEffect != null)
            //{
            //    Instantiate(collectEffect, transform.position, Quaternion.identity);
            //}

            // Xóa vật phẩm khỏi màn hình
            Destroy(gameObject);
        }
    }
}