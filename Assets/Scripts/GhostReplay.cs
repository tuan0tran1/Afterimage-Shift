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

    void Update()
    {
        if (replayPositions == null) return;

        if (index < replayPositions.Count)
        {
            transform.position = replayPositions[index];
            index++;
        }
    }
}