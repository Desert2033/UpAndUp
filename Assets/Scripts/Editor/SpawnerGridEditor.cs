using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnerGrid))]
public class SpawnerGridEditor : Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.NonSelected | GizmoType.Pickable)]
    public static void RenderGizmoGrid(SpawnerGrid spawnerGrid, GizmoType gizmoType)
    {
        GridMaker gridMaker = new GridMaker();
        CameraBorder cameraBorder = Camera.main.GetComponent<CameraBorder>();
        Vector3 startPoint = spawnerGrid.GetStartPointOnlyEditor(cameraBorder);
        Vector3 endPoint = spawnerGrid.GetEndPointOnlyEditor(cameraBorder);
        List<Vector3> line = gridMaker.MakeLine(startPoint, endPoint);

        foreach (Vector3 position in line)
        {
            Gizmos.DrawWireCube(position, new Vector3(0.82f, 0.87f, 0.83f));
        }
    }
}
