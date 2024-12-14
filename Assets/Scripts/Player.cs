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
        _testing_EventTests();
        
    }
    
    /// <summary>
    /// R - Heal, T: - +speed, Y: +def U: + dmg  Z: + exp
    /// </summary>
    void _testing_EventTests()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            stats.HealYourself(20);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            stats.IncreaseSpeed(2);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            stats.IncreaseDefense(2);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            stats.IncreaseDamage(2);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerStats.GetXP(20);
        }
    }

  
}