using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1f);
    }
}
