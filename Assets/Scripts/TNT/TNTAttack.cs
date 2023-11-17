using UnityEngine;

public class TNTAttack : MonoBehaviour
{
    private const string HeroTag = "Player";

    [SerializeField] private TNTDeath _death;
    [SerializeField] private int _damage = 1;

    private BoxCollider _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == HeroTag)
        {
            other.GetComponent<HeroHealth>().TakeDamage(_damage);
            _collider.enabled = false;
            _death.Dead();
        }
    }
}
