using UnityEngine;

public class ExpCollision : MonoBehaviour
{
    private float _exp = 350;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ExpBank expBank))
        {
            expBank.AddExp(_exp);
            Destroy(gameObject);
        }
    }
}
