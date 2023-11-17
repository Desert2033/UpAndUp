using System;
using System.Collections;
using UnityEngine;

public class HeroJump : MonoBehaviour
{
    private const float DistanceJump = 1.3f;
    private const float DurationForJump = 0.2f;
    private const float DurationForDown = 0.2f;

    [SerializeField] private AnimationCurve _curveY;

    public bool IsMovingUp { get; private set; } = false;

    public void Jump()
    {
        StartCoroutine(JumpByTime(DurationForJump));
    }

    private IEnumerator JumpByTime(float duration)
    {
        float expireTime = 0;
        float progress = 0;

        IsMovingUp = true;

        Vector3 startPosition = transform.position;

        while (progress < 1)
        {
            expireTime += Time.deltaTime;
            progress = expireTime / duration;

            transform.position = startPosition + new Vector3(0, _curveY.Evaluate(progress) * DistanceJump, 0);
            
            yield return null;
        }

        yield return new WaitForSeconds(DurationForDown);

        IsMovingUp = false;
    }
}
