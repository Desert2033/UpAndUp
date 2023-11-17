using UnityEngine;

[CreateAssetMenu(fileName = "HeroLevel", menuName = "ScriptableObjects/Hero Levels")]
public class HeroLevelData : ScriptableObject
{
    [Min(1)]
    public int Level;
    public float Hp;
    public float Damage;
    public Color ColorArmor;
    public bool WearArmorLegs;
    public bool WearArmorHands;
    public bool WearArmorBody;
    public bool WearArmorHead;
}
