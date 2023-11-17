using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private Vector3 _direction;
    private Transform _lookAt;
    private float _speed = 3f;

    public void Construct(Vector3 direction, Transform lookAt)
    {
        _direction = direction;
        _lookAt = lookAt;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.LookAt(_lookAt);
        transform.position += _direction * Time.deltaTime * _speed;
    }
}
