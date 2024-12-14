using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private StatsSO stats;
    private float horizontal;
    private float vertical;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Vector3 m_Input = new Vector3(horizontal, vertical, 0);
        rb.MovePosition(transform.position + m_Input * (stats.speed * Time.fixedDeltaTime));
        
    }
}