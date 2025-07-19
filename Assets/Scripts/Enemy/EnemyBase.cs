using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        private EnemyMovement _enemyMovement;
        [FormerlySerializedAs("bulletSO")][SerializeField] private BulletSo bulletSo;
        [SerializeField] private GameObject bloodParticles;
        [SerializeField] private float health;

        private void Start()
        {
            _enemyMovement = GetComponent<EnemyMovement>();
            health = _enemyMovement.stats.maxHealth;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Bullet")) return;
            health -= bulletSo.damage;
            Debug.Log($"Diddy health: {health}");
            Destroy(collision.gameObject);
            CheckDie();
        }

        private void CheckDie()
        {
            if (!(health <= 0)) return;
            Instantiate(bloodParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }
}
