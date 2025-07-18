using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private StatsSO stats;
    [SerializeField] private PlayerStatsSO playerStats;
    
    private int _health;
    private int m_exp;
    void Start()
    {
            _health = stats.health;
            stats.e_SetDead.AddListener(Die);
    }

    void Die()
    {
       SceneManager.LoadScene("DeathScene"); 
    }

    public int addExp(int m_exp, int amount)
    {
         return m_exp + amount;
    }
  
}
