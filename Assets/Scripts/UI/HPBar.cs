using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image _currentImage;
    [SerializeField] private TextMeshProUGUI _textHp;
    [SerializeField] private GameObject _gameObjectForHealth;

    private IHealth _health;

    private void OnEnable()
    {
        _health = _gameObjectForHealth.GetComponent<IHealth>();

        _health.OnHpChange += SetHpValue;
    }

    private void OnDisable() => 
        _health.OnHpChange -= SetHpValue;

    public void SetHpValue(float currentHP, float maxHP)
    {
        int hp = currentHP >= 0 ? (int)currentHP : 0; 

        _textHp.text = $"{(int)maxHP}/{hp}";
        _currentImage.fillAmount = currentHP / maxHP;
    }
}
