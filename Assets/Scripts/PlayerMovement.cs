using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private StatsSO stats;
    private float horizontal;
    private float vertical;
    private Rigidbody2D rb;
    private float hitRange = 0.3f;
    public LayerMask mapLayer; 
    private bool moveFlag = true;
    private bool isShooting = false;
    private Animator animator;

    void Start()
    {
        mapLayer = UnityEngine.LayerMask.GetMask("Map");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        moveFlag = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;

        UpdateMovementAnimation();

        if (Input.GetButtonDown("Fire1")) {
            isShooting = true;
            animator.SetBool("isShooting", true);
            Shoot();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            isShooting = false;
            animator.SetBool("isShooting", false);
        }

        // CheckCollision();
        CheckMapCollision();
    }

    void FixedUpdate()
    {
        if (!isShooting)
        {
            Vector3 m_Input = new Vector3(horizontal, vertical, 0);
            rb.MovePosition(transform.position + m_Input * stats.speed * Time.fixedDeltaTime);
        }
    }
    
    private void CheckCollision()
    {
        moveFlag = true;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, hitRange);
        foreach (var hit in hits)
        {
            if (hit.gameObject.tag == "Enemy")
                moveFlag = false;
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
        animator.SetBool("isRunning", moveFlag);
    }

    private void Shoot()
    {
        animator.SetTrigger("Shoot");
    }

}