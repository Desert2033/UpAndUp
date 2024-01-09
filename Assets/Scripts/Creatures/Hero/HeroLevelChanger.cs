using System;
using UnityEngine;

public class HeroLevelChanger : MonoBehaviour
{
    private const int LevelMin = 1;
    
    [SerializeField] private MeshRenderer _armorHead;
    [SerializeField] private MeshRenderer _armorBody;
    [SerializeField] private MeshRenderer _armorLegLeft;
    [SerializeField] private MeshRenderer _armorLegRight;
    [SerializeField] private MeshRenderer _armorHandRight;
    [SerializeField] private MeshRenderer _armorHandLeft;
    [SerializeField] private GameObject _legRight;
    [SerializeField] private GameObject _legLeft;
    [SerializeField] private HeroHealth _heroHealth;
    [SerializeField] private HeroAttack _heroAttack;
    [SerializeField] private ExpBank _expBank;

    private IStaticDataService _staticDataService;
    private int _currentLevel;

    public void Construct(IStaticDataService staticDataService, int heroLevel)
    {
        _staticDataService = staticDataService;
        _currentLevel = heroLevel;
    }

    private void OnEnable()
    {
        _expBank.OnExpMax += UpLevel;
    }

    private void OnDisable()
    {
        _expBank.OnExpMax -= UpLevel;
    }

    public void UpLevel()
    {
        int newLevel = ++_currentLevel;

        if (newLevel < LevelMin)
            throw new Exception("Level can't be less than 1");

        HeroLevelData levelData = _staticDataService.ForHeroLevel(newLevel);
        
        ChangeStats(levelData);
        ChangeArmor(levelData);
    }

    private void ChangeStats(HeroLevelData levelData)
    {
        _heroHealth.SetMaxHp(levelData.Hp);
        _heroAttack.SetDamage(levelData.Damage);
    }

    private void ChangeArmor(HeroLevelData levelData)
    {
        if (levelData.WearArmorHead)
            SetArmor(_armorHead, levelData.ColorArmor);
        if (levelData.WearArmorBody)
            SetArmor(_armorBody, levelData.ColorArmor);
        if (levelData.WearArmorHands)
            SetArmorHands(_armorHandLeft, _armorHandRight, levelData.ColorArmor);
        if (levelData.WearArmorLegs)
            SetArmorLegs(_armorLegLeft, _armorLegRight, levelData.ColorArmor);
    }

    private void SetArmorHands(MeshRenderer armorFirst, MeshRenderer armorSecond, Color color)
    {
        armorFirst.gameObject.SetActive(true);
        armorFirst.material.color = color;

        armorSecond.gameObject.SetActive(true);
        armorSecond.material.color = color;
    }

    private void SetArmorLegs(MeshRenderer armorFirst, MeshRenderer armorSecond, Color color)
    {
        armorFirst.gameObject.SetActive(true);
        armorFirst.material.color = color;

        armorSecond.gameObject.SetActive(true);
        armorSecond.material.color = color;

        _legLeft.SetActive(false);
        _legRight.SetActive(false);
    }

    private void SetArmor(MeshRenderer armor, Color color)
    {
        armor.gameObject.SetActive(true);
        armor.material.color = color;
    }
}
