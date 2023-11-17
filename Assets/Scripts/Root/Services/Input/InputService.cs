using UnityEngine;

public enum Directions 
{ 
    Left,
    Right,
    Up
}

public enum Actions
{
    Move,
    Attack
}

public class InputService : IInputService
{
    private InputUI _inputUI;
    private Camera _mainCamera;

    public void SetInputUI(InputUI inputUI)
    {
        _mainCamera = Camera.main;
        _inputUI = inputUI;
    }

    public Actions GetActions(out Transform enemyTransform)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsHitWithEnemy(out enemyTransform))
                return Actions.Attack;
        }

        enemyTransform = null;
        return Actions.Move;
    }

    public Directions GetDirection()
    {
        Directions direction;

        if (_inputUI.LeftButton.IsPressed)
            direction = Directions.Left;
        else if (_inputUI.RightButton.IsPressed)
            direction = Directions.Right;
        else
            direction = Directions.Up;

        return direction;
    }

    private bool IsHitWithEnemy(out Transform enemyTransform)
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, Constants.EnemyLayerMask))
        {
            if (hit.transform.tag == Constants.EnemyTag)
            {
                enemyTransform = hit.transform;
                return true;
            }
        }

        enemyTransform = null;
        return false;
    }
}
