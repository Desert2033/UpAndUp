using UnityEngine;

public class HealCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out HealBank healBank))
        {
            healBank.Add();
            Destroy(gameObject);
        }
    }
}
