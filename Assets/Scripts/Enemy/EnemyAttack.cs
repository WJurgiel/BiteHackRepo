using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private StatsSo playerStats;
        private EnemyMovement _enemyMovement;
        public UnityEvent knockbackEvent;
        public UnityEvent knockbackFromWallEvent;
        [SerializeField] private float hitRange = 0.1f;
        public LayerMask mapLayer;
        private float _lastDamageTime = -Mathf.Infinity;
        [SerializeField] private float damageCooldown = 1f;
        void Start()
        {
            mapLayer = LayerMask.GetMask("Map");
            _enemyMovement = GetComponent<EnemyMovement>();
        }

        void Update()
        {
            CheckCollision();
            CheckMapCollision();
        }

        private void CheckCollision()
        {
            bool hitCheck = false;
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, hitRange);
            Debug.DrawRay(transform.position, Vector3.down * hitRange, Color.red);
            foreach (var hit in hits)
            {
                if (hit.gameObject.CompareTag("Player"))
                    hitCheck = true;

            }
            if (!hitCheck) return;
            if (!(Time.time >= _lastDamageTime + damageCooldown)) return;
            playerStats.GetDamage(Random.Range(_enemyMovement.stats.damage - 5, _enemyMovement.stats.damage + 5));
            knockbackEvent.Invoke();
            _lastDamageTime = Time.time;
        }
        private void CheckMapCollision()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, hitRange, mapLayer);
            if (hits.Length > 0)
            {
                knockbackFromWallEvent.Invoke();
            }
        }
    }
}
