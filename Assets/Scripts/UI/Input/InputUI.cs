using UnityEngine;
using UnityEngine.UI;

public class InputUI : MonoBehaviour
{
    [SerializeField] private ButtonDirection _leftButton;
    [SerializeField] private ButtonDirection _rightButton;
    [SerializeField] private Button _levelUpButton;

    public ButtonDirection LeftButton => _leftButton;
    public ButtonDirection RightButton => _rightButton;
    public Button LevelUpButton => _levelUpButton;
}
