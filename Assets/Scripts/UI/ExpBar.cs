using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private Image _currentImage;
    [SerializeField] private TextMeshProUGUI _textExp;
    
    private float _maxExp = 1000;
    private ExpBank _expBank;

    public void Construct(ExpBank expBank) => 
        _expBank = expBank;

    private void Start() => 
        _expBank.OnExpChange += SetExpValue;

    private void OnDisable() => 
        _expBank.OnExpChange -= SetExpValue;

    public void SetExpValue(float currentExp)
    {
        _textExp.text = $"{(int)_maxExp}/{currentExp}";
        _currentImage.fillAmount = currentExp / _maxExp;
    }
}
