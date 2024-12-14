using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    public UnityEvent knockbackEvent;
    public UnityEvent knockbackFromWallEvent;
    [SerializeField] private float hitRange = 0.1f;
    public LayerMask mapLayer; 
    void Start()
    {
        mapLayer = UnityEngine.LayerMask.GetMask("Map");
        rb=GetComponent<Rigidbody2D>();
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
        foreach (var hit in hits)
        {
            if (hit.gameObject.tag == "Player")
                hitCheck = true;
        }

        if (hitCheck)
        {
            Debug.Log("Hit");
            knockbackEvent.Invoke();
        }
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
