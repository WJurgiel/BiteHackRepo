using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    [SerializeField] private BulletSO bulletSO;
    [SerializeField] private GameObject bloodParticles;
    [SerializeField] private float health;
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        health = enemyMovement.stats.maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {   
            health -= bulletSO.damage;
            Debug.Log($"Diddy health: {health}");
            Destroy(collision.gameObject);
            CheckDie();
        }
    }

    private void CheckDie()
    {
        if (health <= 0)
        {
            Instantiate(bloodParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
     
    }
}
