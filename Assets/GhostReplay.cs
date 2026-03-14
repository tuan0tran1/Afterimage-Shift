using System.Collections.Generic;
using UnityEngine;

public class GhostReplay : MonoBehaviour
{
    private List<Vector3> replayPositions;
    private int index = 0;

    public void StartReplay(List<Vector3> positions)
    {
        replayPositions = positions;
        index = 0;
    }

    // Thay vì đổi theo thời gian Update, bóng ma sẽ bám chính xác bằng FixedUpdate (chuẩn thời gian vật lý)
    void FixedUpdate()
    {
        if (replayPositions == null) return;

        if (index < replayPositions.Count)
        {
            Vector3 pos = replayPositions[index];
            
            // Xử lý Quay mặt trái phải cho con ma sống động hơn
            if (index > 0)
            {
                float diff = pos.x - replayPositions[index - 1].x;
                if (Mathf.Abs(diff) > 0.001f)
                {
                    SpriteRenderer sr = GetComponent<SpriteRenderer>();
                    if (sr != null) sr.flipX = diff < 0;
                }
            }
            
            transform.position = pos;
            index++;
        }
    }
}