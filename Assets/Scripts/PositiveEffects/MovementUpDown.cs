using UnityEngine;

public class MovementUpDown : MonoBehaviour
{
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private float _distanceMove = 0.2f;
    
    private Vector3 _topPoint;
    private Vector3 _downPoint;
    private Vector3 _currentPoint;

    private void Start()
    {
        _topPoint = transform.position;
        _topPoint.y += _distanceMove; 
        
        _downPoint = transform.position;
        _topPoint.y += _distanceMove;

        _currentPoint = _topPoint;
    }

    private void Update()
    {
        Move();

        if (IsReachedPoint())
        {
            if (_currentPoint == _topPoint)
                _currentPoint = _downPoint;
            else
                _currentPoint = _topPoint;
        }
    }

    private void Move()
    {
        float distance = Vector3.Distance(transform.position, _currentPoint);
        transform.position = Vector3.Lerp(transform.position, _currentPoint, Time.deltaTime * _speed / distance);
    }

    private bool IsReachedPoint() =>
        Vector3.Distance(transform.position, _currentPoint) <= 0;
}
