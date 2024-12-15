using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private StatsSO stats;
    [SerializeField] private PlayerStatsSO playerStats;

    
    private int _health;
    
    
    void Start()
    {
            _health = stats.health;
            stats.e_SetDead.AddListener(Die);
    }

    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     stats.GetDamage(Random.Range(0, 10));
        //     Debug.Log("Private health: " + _health + " SO health = " + stats.health) ;
        // }
        // _testing_EventTests();
        
    }

    void Die()
    {
       SceneManager.LoadScene("DeathScene"); 
    }
    /// <summary>
    /// R - Heal, T: - +speed, Y: +def U: + dmg  Z: + exp
    /// </summary>
    void _testing_EventTests()
    {
       
    }

  
}
