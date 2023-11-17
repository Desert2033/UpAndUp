using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _target;
    private Vector3 _prevTargetPosition;
    private Vector3 _offset = new Vector3(0f, 4.5f, 0f);

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target != null)
        {
            if(transform.position.y - 4.5f < _target.position.y)
            {
                Vector3 targetPosition = _target.position;
                targetPosition.z = 0;
                Vector3 cameraPosition = transform.position;
                cameraPosition.z = 0;

                Vector3 newPosition = Vector3.Lerp(cameraPosition, targetPosition + _offset, Time.deltaTime);
                newPosition.z = transform.position.z;

                transform.position = newPosition;
            }

            /*if (transform.position.y - 3.5f < _target.position.y )
            {
                Vector3 newPosition = _target.position + _offset;
                newPosition.y = newPosition.y * Time.deltaTime;
                newPosition.z = 0;

                _prevTargetPosition = _target.position;
                transform.position = transform.position + new Vector3(
                    0, 
                    _target.position.y  * Time.deltaTime, 
                    0);
            }
            else if (transform.position.x != _target.position.x)
            {
                Vector3 newPosition = _target.position;
                newPosition.z = transform.position.z;
                newPosition.y = transform.position.y;

                _prevTargetPosition = _target.position;
                transform.position = newPosition;
            }*/
        }
    }
}
