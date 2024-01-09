using System.Collections.Generic;
using UnityEngine;

public class GridMaker
{
    public List<Vector3> MakeLine(Vector3 pointStart, Vector3 pointEnd)
    {
        List<Vector3> pointsForEnemySpawn = new List<Vector3>();
        Vector3 newPosition = pointStart;

        while (newPosition.x < pointEnd.x)
        {
            pointsForEnemySpawn.Add(newPosition);

            newPosition += new Vector3(Constants.BlocksDistanceX, 0f, 0f);
        }

        return pointsForEnemySpawn;
    }
}
