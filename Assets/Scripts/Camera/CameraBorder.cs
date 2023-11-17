using UnityEngine;

public class CameraBorder : MonoBehaviour
{
    private float _distanceToHero = 10;

    public Vector3 LeftTop => Camera.main.ViewportToWorldPoint(new Vector3(0, 1, _distanceToHero));
    public Vector3 LeftBot => Camera.main.ViewportToWorldPoint(new Vector3(0, 0, _distanceToHero));
    public Vector3 RightTop => Camera.main.ViewportToWorldPoint(new Vector3(1, 1, _distanceToHero));
    public Vector3 RightBot => Camera.main.ViewportToWorldPoint(new Vector3(1, 0, _distanceToHero));
}
