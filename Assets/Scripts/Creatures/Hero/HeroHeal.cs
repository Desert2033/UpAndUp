using UnityEngine;
using UnityEngine.UI;

public class HeroHeal : MonoBehaviour
{
    [SerializeField] private HealBank _healBank;
    [SerializeField] private HeroHealth _heroHealth;

    private Button _healButton;
    private int _procentHeal = 25;

    public void Construct(Button healButton)
    {
        _healButton = healButton;

        _healButton.onClick.AddListener(UseHeal);
    }

    private void OnDisable()
    {
        _healButton.onClick.RemoveListener(UseHeal);
    }

    public void UseHeal()
    {
        if (_healBank.HaveHeal())
        {
            float heal = _heroHealth.MaxHP * _procentHeal / 100;

            if (heal < 1)
                heal = 1;

            _heroHealth.Heal((int)heal);
            _healBank.Remove();
        }
    }
}