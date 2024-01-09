using UnityEngine;

public class InputUI : MonoBehaviour
{
    [SerializeField] private ButtonDirection _leftButton;
    [SerializeField] private ButtonDirection _rightButton;

    public ButtonDirection LeftButton => _leftButton;
    public ButtonDirection RightButton => _rightButton;
}
