using System.Collections.Generic;
using UnityEngine;

public class GridMaker
{
   /* public List<List<Vector3>> MakeGrid(Vector3 pointStart, Vector3 pointEnd, int countLevels = 1)
    {
        List<List<Vector3>> levels = new List<List<Vector3>>(countLevels);

        for (int i = 0; i < countLevels; i++)
        {
            levels.Add(MakeLine(pointStart, pointEnd));
        }

        return levels;
    }
*/
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
