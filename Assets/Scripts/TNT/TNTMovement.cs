using System.Collections;
using UnityEngine;

public class TNTMovement : MonoBehaviour
{
    private const float TimeToMove = 1f;

    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;
    [SerializeField] private int _speed = 2;

    private Transform _currentPoint;
    private Timer _cooldown;

    public void Start()
    {
        _cooldown = new Timer(TimeToMove);
        _currentPoint = _point2;
    }

    public void Update()
    {
        _cooldown.Tick(Time.deltaTime);

        if (_cooldown.CurrentDuretion <= 0)
        {
            Move();

            if (IsReachedPoint())
            {
                if (_currentPoint == _point1)
                    _currentPoint = _point2;
                else
                    _currentPoint = _point1;

                _cooldown.Restart();
            }
        }
    }

    private void Move()
    {
        float distance = Vector3.Distance(transform.position, _currentPoint.position);
        transform.position = Vector3.Lerp(transform.position, _currentPoint.position, Time.deltaTime * _speed / distance);
    }

    private bool IsReachedPoint() =>
        Vector3.Distance(transform.position, _currentPoint.position) <= 0;
}
