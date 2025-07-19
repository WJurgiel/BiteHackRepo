using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        public StatsSo stats;
        private GameObject _player;
        private EnemyAttack _knockbackEmitter;

        [SerializeField] private float speed;
        private float _distance;
        [SerializeField] private float startChaseDistance = 6f;
        private bool _chaseFlag;

        [FormerlySerializedAs("KBForce")]
        [SerializeField]
        private float kbForce = 0.3f;
        private float _kbCounter;
        [FormerlySerializedAs("KBTotalTime")]
        [SerializeField]
        private float kbTotalTime = 0.5f;
        private SpriteRenderer _spriteRenderer;

        void Awake()
        {
            _knockbackEmitter = GetComponent<EnemyAttack>();
            _knockbackEmitter.knockbackEvent.AddListener(EnemyHit);
            _knockbackEmitter.knockbackFromWallEvent.AddListener(EnemyHitWall);
        }
        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        void Update()
        {
            _distance = Vector2.Distance(_player.transform.position, transform.position);
            if (_chaseFlag)
            {

                if (_kbCounter <= 0)
                {
                    transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, stats.speed * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, transform.position + (transform.position - _player.transform.position).normalized, kbForce * Time.deltaTime);
                    _kbCounter -= Time.deltaTime;
                }
            }
            else
            {
                CheckIfCanChase();
            }

            _spriteRenderer.flipX = !(ReturnDirection().x > 0);
        }
        private Vector2 ReturnDirection()
        {
            return _player.transform.position - transform.position;
        }
        private void CheckIfCanChase()
        {
            if (_distance <= startChaseDistance)
            {
                _chaseFlag = true;
            }
        }
        private void EnemyHit()
        {
            _kbCounter = kbTotalTime;
        }

        private void EnemyHitWall()
        {
            _kbCounter = 0.1f;
        }
    }
}
