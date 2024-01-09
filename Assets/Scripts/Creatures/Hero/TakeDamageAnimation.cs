using System.Collections;
using UnityEngine;

public class TakeDamageAnimation : MonoBehaviour
{
    private const float Duration = 0.2f;

    [SerializeField] private GameObject[] _damageObjects;

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
        foreach (GameObject damage in _damageObjects)
        {
            damage.SetActive(true);
        }
    }

    private void DisactiveDamageGlow()
    {
        foreach (GameObject damage in _damageObjects)
        {
            damage.SetActive(false);
        }
    }

    private IEnumerator TakeDamage()
    {
        ActiveDamageGlow();

        yield return new WaitForSeconds(Duration);
        DisactiveDamageGlow();
    }
}
