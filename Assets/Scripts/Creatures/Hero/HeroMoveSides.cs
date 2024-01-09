using System.Collections;
using UnityEngine;

public class HeroMoveSides : MonoBehaviour
{
    private const float DurationForMoveSides = 0.15f;
    private const float AngleRotationY = 140f;

    [SerializeField] private HeroAnimator _heroAnimator;

    public bool IsMoving { get; private set; } = false;

    public void Move(Directions direction, Vector3 blockPosition)
    {
        Vector3 newPosition = new Vector3(blockPosition.x, 0, 0);
        HeroExtraDown(blockPosition);

        if (direction == Directions.Left)
        {
            MoveLeft(newPosition);
        }
        else if (direction == Directions.Right)
        {
            MoveRight(newPosition);
        }
    }

    private void HeroExtraDown(Vector3 blockPosition)
    {
        float offsetFromBlock = 0.9f;
        transform.position = new Vector3(transform.position.x, blockPosition.y + offsetFromBlock, transform.position.z);
    }

    private void MoveLeft(Vector3 position)
    {
        transform.rotation = Quaternion.Euler(0, -AngleRotationY, 0);
        StartCoroutine(Move(position, DurationForMoveSides));
    }

    private void MoveRight(Vector3 position)
    {
        transform.rotation = Quaternion.Euler(0, AngleRotationY, 0);
        StartCoroutine(Move(position, DurationForMoveSides));
    }

    private IEnumerator Move(Vector3 newPosition, float duration)
    {
        _heroAnimator.StartMove();
        IsMoving = true;

        Vector3 startingPos = transform.position;
        Vector3 finalPos = newPosition;
        float elapsedTime = 0;

        finalPos.y = transform.position.y;
        finalPos.z = transform.position.z;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        IsMoving = false;
        _heroAnimator.StopMove();
    }
}
