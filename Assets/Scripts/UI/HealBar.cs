using UnityEngine;
using TMPro;

public class HealBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText;

    private HealBank _healBank;

    public void Construct(HealBank healBank)
    {
        _healBank = healBank;

        _healBank.OnChangeCount += ChangeCount;
    }

    private void OnDisable()
    {
        _healBank.OnChangeCount -= ChangeCount;
    }

    public void ChangeCount(int count)
    {
        _countText.text = $"{count}";
    }
}
