using System.Collections;
using UnityEngine;

public class TNTDeath : MonoBehaviour
{
    private const float Duration = 1f;
    [SerializeField] private GameObject _explosionEffect;
    [SerializeField] private GameObject _model;

    public void Dead()
    {
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        _model.SetActive(false);
        _explosionEffect.SetActive(true);

        yield return new WaitForSeconds(Duration);

        Destroy(gameObject);
    }
}
