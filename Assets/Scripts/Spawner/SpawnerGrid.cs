using UnityEngine;

public class SpawnerGrid : MonoBehaviour
{
    private CameraBorder _cameraBorder;

    private void Awake()
    {
        _cameraBorder = Camera.main.GetComponent<CameraBorder>();
    }

    public Vector3 GetStartPoint()
    {
        Vector3 startPoint = _cameraBorder.LeftTop;

        startPoint.x += Constants.BlocksDistanceX;
        startPoint.y = transform.position.y;
        startPoint.z = transform.position.z;

        return startPoint;
    }

    public Vector3 GetEndPoint() =>
        _cameraBorder.RightTop;

    public Vector3 GetStartPointOnlyEditor(CameraBorder cameraBorder)
    {
        _cameraBorder = cameraBorder;

        return GetStartPoint();
    }

    public Vector3 GetEndPointOnlyEditor(CameraBorder cameraBorder)
    {
        _cameraBorder = cameraBorder;

        return GetEndPoint();
    }
}
