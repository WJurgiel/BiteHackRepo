using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private StatsSO stats;
    [SerializeField] private BoneSO bones;
    private SpriteRenderer spriteRenderer;
    private float horizontal;
    private float vertical;
    private Rigidbody2D rb;
    private float hitRange = 0.3f;
    public LayerMask mapLayer;
    private bool moveFlag = true;
    private bool isMoving = true;
    private bool isShooting = false;
    private Animator animator;

    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing = false;
    private float dashCooldownTimer = 0f;
    void Start()
    {
        mapLayer = UnityEngine.LayerMask.GetMask("Map");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        isMoving = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;
        if (horizontal < 0 && spriteRenderer.flipX == false)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontal > 0 && spriteRenderer.flipX == true)
        {
            spriteRenderer.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0 && !isDashing)
        {
            StartCoroutine(Dash());
        }
        // Cooldown timer
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
        UpdateMovementAnimation();

        if (Input.GetButtonDown("Fire1"))
        {
            isShooting = true;
            animator.SetBool("isShooting", true);
            Shoot();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            isShooting = false;
            animator.SetBool("isShooting", false);
        }

        CheckCollision();
        CheckMapCollision();
    }


    void FixedUpdate()
    {
        if (moveFlag && !isDashing)
        {
            Vector3 m_Input = new Vector3(horizontal, vertical, 0);
            rb.MovePosition(transform.position + m_Input * stats.speed * Time.fixedDeltaTime);
        }
    }

    private void CheckCollision()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, hitRange);
        foreach (var hit in hits)
        {
            if (hit.gameObject.tag == "Guard")
                moveFlag = CanMoveOutOfCollision(hits);
        }
    }

    private void CheckMapCollision()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, hitRange, mapLayer);
        if (hits.Length > 0)
        {
            moveFlag = CanMoveOutOfCollision(hits);
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
        animator.SetBool("isRunning", isMoving);
    }

    private void Shoot()
    {
        animator.SetTrigger("Shoot");
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
        isDashing = true; // Start dash
        dashCooldownTimer = dashCooldown; // Set cooldown timer

        // Determine dash direction based on current movement input
        Vector2 dashDirection = new Vector2(horizontal, vertical).normalized;

        // Perform dash movement by overriding Rigidbody velocity
        rb.linearVelocity = dashDirection * dashSpeed;

        // Wait for the dash duration
        yield return new WaitForSeconds(dashDuration);

        // Stop dash movement and reset velocity
        rb.linearVelocity = Vector2.zero;
        isDashing = false;
    }

}