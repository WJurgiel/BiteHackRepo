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
       
        
    }

    void Die()
    {
       SceneManager.LoadScene("DeathScene"); 
    }
   

  
}
