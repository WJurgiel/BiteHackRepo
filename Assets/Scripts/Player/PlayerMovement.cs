using System.Collections;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private StatsSo stats;
        [SerializeField] private BoneSo bones;
        private SpriteRenderer _spriteRenderer;
        private float _horizontal;
        private float _vertical;
        private Rigidbody2D _rb;
        private const float HitRange = 0.3f;
        public LayerMask mapLayer;
        private bool _moveFlag = true;
        private bool _isMoving = true;
        private bool _isShooting = false;
        private Animator _animator;

        public float dashSpeed = 20f;
        public float dashDuration = 0.2f;
        public float dashCooldown = 1f;
        private bool _isDashing;
        private float _dashCooldownTimer;

        private void Start()
        {
            mapLayer = UnityEngine.LayerMask.GetMask("Map");
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");

            _isMoving = Mathf.Abs(_horizontal) > 0.1f || Mathf.Abs(_vertical) > 0.1f;
            _spriteRenderer.flipX = _horizontal switch
            {
                < 0 when _spriteRenderer.flipX == false => true,
                > 0 when _spriteRenderer.flipX == true => false,
                _ => _spriteRenderer.flipX
            };
            if (Input.GetKeyDown(KeyCode.LeftShift) && _dashCooldownTimer <= 0 && !_isDashing)
            {
                StartCoroutine(Dash());
            }
            if (_dashCooldownTimer > 0)
            {
                _dashCooldownTimer -= Time.deltaTime;
            }
            UpdateMovementAnimation();

            if (Input.GetButtonDown("Fire1"))
            {
                _isShooting = true;
                _animator.SetBool("isShooting", _isShooting);
                Shoot();
            }

            if (Input.GetButtonUp("Fire1"))
            {
                _isShooting = false;
                _animator.SetBool("isShooting", _isShooting);
            }

            CheckCollision();
            CheckMapCollision();
        }


        void FixedUpdate()
        {
            if (!_moveFlag || _isDashing) return;
            var mInput = new Vector3(_horizontal, _vertical, 0);
            _rb.MovePosition(transform.position + mInput * (stats.speed * Time.fixedDeltaTime));
        }

        private void CheckCollision()
        {
            var hits = Physics2D.OverlapCircleAll(transform.position, HitRange);
            foreach (var hit in hits)
            {
                if (hit.gameObject.CompareTag("Guard"))
                    _moveFlag = CanMoveOutOfCollision(hits);
            }
        }

        private void CheckMapCollision()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, HitRange, mapLayer);
            if (hits.Length > 0)
            {
                _moveFlag = CanMoveOutOfCollision(hits);
            }


        }

        private bool CanMoveOutOfCollision(Collider2D[] hits)
        {
            foreach (var hit in hits)
            {
                Vector2 directionToExit = (transform.position - hit.transform.position).normalized;

                if (Input.GetAxisRaw("Horizontal") * directionToExit.x > 0 ||
                    Input.GetAxisRaw("Vertical") * directionToExit.y > 0)
                {
                    return true;
                }
            }
            return false;
        }

        // animation functions
        private void UpdateMovementAnimation()
        {
            _animator.SetBool("isRunning", _isMoving);
        }

        private void Shoot()
        {
            _animator.SetTrigger("Shoot");
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Bone")
            {
                bones.AddBone();
                Destroy(other.gameObject);
            }
            else if (other.gameObject.tag == "GuardTrigger")
            {
                CanvasGroup guardCanvas = other.gameObject.GetComponentInChildren<CanvasGroup>();

                if (bones.boneCurrent >= bones.boneMax)
                {
                    GameObject panel = guardCanvas.gameObject.GetComponentInChildren<Image>().gameObject;
                    TextMeshProUGUI text = panel.GetComponentInChildren<TextMeshProUGUI>();
                    text.text = "You have enough bones to pass!";
                }
                StartCoroutine(FadeCanvasOpacity(1f, 0.6f, guardCanvas));
            }
        }
        public IEnumerator FadeCanvasOpacity(float targetOpacity, float duration, CanvasGroup canvasGroup)
        {
            float startOpacity = canvasGroup.alpha;
            float timeElapsed = 0f;

            while (timeElapsed < duration)
            {
                canvasGroup.alpha = Mathf.Lerp(startOpacity, targetOpacity, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            // Ensure final opacity is set to target
            canvasGroup.alpha = targetOpacity;
            if (targetOpacity == 1.0f)
            {
                yield return new WaitForSeconds(4f);
                StartCoroutine(FadeCanvasOpacity(0f, 1.0f, canvasGroup));

            }
        }
        private IEnumerator Dash()
        {
            _isDashing = true; // Start dash
            _dashCooldownTimer = dashCooldown; // Set cooldown timer

            // Determine dash direction based on current movement input
            Vector2 dashDirection = new Vector2(_horizontal, _vertical).normalized;

            // Perform dash movement by overriding Rigidbody velocity
            _rb.velocity = dashDirection * dashSpeed;
            // Wait for the dash duration
            yield return new WaitForSeconds(dashDuration);

            // Stop dash movement and reset velocity
            _rb.velocity = Vector2.zero;
            _isDashing = false;
        }

    }
}