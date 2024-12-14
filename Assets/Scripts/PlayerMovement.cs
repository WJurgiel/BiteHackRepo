using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 8f;
    private float horizontal;
    private float vertical;
    private Rigidbody2D rb;
    private float hitRange;
    public LayerMask mapLayer; 
    private bool moveFlag = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        CheckCollision();
        CheckMapCollision();
    }

    void FixedUpdate()
    {
        if (moveFlag)
        {
            Vector3 m_Input = new Vector3(horizontal, vertical, 0);
            rb.MovePosition(transform.position + m_Input * speed * Time.fixedDeltaTime);
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
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, hitRange);
        foreach (var hit in hits)
        {
            if (hit.gameObject.tag == "Enemy")
                moveFlag = false;
        }
    }
    
}