using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    [SerializeField] private BulletSO bulletSO;
    [SerializeField] private GameObject bloodParticles;
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyMovement.stats.e_SetDead.AddListener(Die);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            enemyMovement.stats.GetDamage(Random.Range(bulletSO.damage - 5, bulletSO.damage+5));
            Destroy(collision.gameObject);
        }
    }

    private void Die()
    {
        Instantiate(bloodParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
