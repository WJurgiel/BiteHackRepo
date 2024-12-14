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

    void Start()
    {
        mapLayer = UnityEngine.LayerMask.GetMask("Map");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        // CheckCollision();
        CheckMapCollision();
    }

    void FixedUpdate()
    {
        if (moveFlag)
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
    
}