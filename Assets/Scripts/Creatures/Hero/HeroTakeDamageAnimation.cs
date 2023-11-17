using System.Collections;
using UnityEngine;

public class HeroTakeDamageAnimation : MonoBehaviour
{
    private const float Duration = 0.2f;

    [SerializeField] private GameObject _damageHead;
    [SerializeField] private GameObject _damageBody;
    [SerializeField] private GameObject _damageRightHand;
    [SerializeField] private GameObject _damageLeftHand;
    [SerializeField] private GameObject _damageRightLeg;
    [SerializeField] private GameObject _damageLeftLeg;

    public void PlayTakeDamage()
    {
        StartCoroutine(TakeDamage());
    }

    public void PlayDeath()
    {
        ActiveDamageGlow();
    }

    private void ActiveDamageGlow()
    {
        _damageHead.SetActive(true);
        _damageBody.SetActive(true);
        _damageRightHand.SetActive(true);
        _damageLeftHand.SetActive(true);
        _damageRightLeg.SetActive(true);
        _damageLeftLeg.SetActive(true);
    }

    private void DisactiveDamageGlow()
    {
        _damageHead.SetActive(false);
        _damageBody.SetActive(false);
        _damageRightHand.SetActive(false);
        _damageLeftHand.SetActive(false);
        _damageRightLeg.SetActive(false);
        _damageLeftLeg.SetActive(false);
    }

    private IEnumerator TakeDamage()
    {
        ActiveDamageGlow();

        yield return new WaitForSeconds(Duration);
        DisactiveDamageGlow();
    }
}
