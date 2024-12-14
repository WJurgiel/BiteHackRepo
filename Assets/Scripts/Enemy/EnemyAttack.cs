using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    public UnityEvent knockbackEvent;
    [SerializeField] private float hitRange = 0.1f;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckCollision();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            knockbackEvent.Invoke();
           Debug.Log(collision.gameObject.name);
        }
        
        
    }
    private void CheckCollision()
    {
        bool hitCheck = false;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, hitRange);
        foreach (var hit in hits)
        {
            if (hit.gameObject.tag == "Player")
                hitCheck = true;
        }

        if (hitCheck)
        {
            knockbackEvent.Invoke();
        }
    }

}
