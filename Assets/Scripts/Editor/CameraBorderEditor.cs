using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraBorder))]
public class CameraBorderEditor : Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderGismoBorder(CameraBorder cameraBorder, GizmoType gizmo)
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(cameraBorder.LeftBot, cameraBorder.LeftTop);
        Gizmos.DrawLine(cameraBorder.RightBot, cameraBorder.RightTop);
        Gizmos.DrawLine(cameraBorder.LeftBot, cameraBorder.RightBot);
        Gizmos.DrawLine(cameraBorder.LeftTop, cameraBorder.RightTop);
    }
}
