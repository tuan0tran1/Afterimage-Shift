using System.Collections.Generic;
using UnityEngine;

public class PlayerRecorder : MonoBehaviour
{
    public List<Vector3> positions = new List<Vector3>();

    // Thay đổi Update thành FixedUpdate để khắc phục lỗi chạy chậm/nhanh khi thay đổi FPS máy tính
    void FixedUpdate()
    {
        positions.Add(transform.position);
    }

    public List<Vector3> GetPositions()
    {
        // Return a copy so the Ghost only replays Run 1 recordings,
        // even if PlayerRecorder continues recording in Run 2.
        return new List<Vector3>(positions);
    }

    public void ClearPositions()
    {
        positions.Clear();
    }
}