using UnityEngine;

public class SpawnPointPositionFollow : MonoBehaviour
{
    private Transform _cameraTransform;
    private float _offsetY;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _offsetY = transform.position.y - _cameraTransform.position.y;
    }

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 newPosition = _cameraTransform.position;

        newPosition.y += _offsetY;
        newPosition.z = transform.position.z;

        transform.position = newPosition;
    }
}
