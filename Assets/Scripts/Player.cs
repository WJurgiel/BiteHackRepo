using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private StatsSO stats;
    [SerializeField] private PlayerStatsSO playerStats;

    private int _health;
    void Start()
    {
            _health = stats.health;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            stats.GetDamage(Random.Range(0, stats.damage));
            Debug.Log("Private health: " + _health + " SO health = " + stats.health) ;
        }
    }
}
