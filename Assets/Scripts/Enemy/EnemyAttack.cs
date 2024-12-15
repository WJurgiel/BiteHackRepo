using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private StatsSO playerStats;
    private EnemyMovement enemyMovement;
    private Rigidbody2D rb;
    public UnityEvent knockbackEvent;
    public UnityEvent knockbackFromWallEvent;
    [SerializeField] private float hitRange = 0.1f;
    public LayerMask mapLayer; 
    //3:56 AM- hardcoding, ready, steady, go
    private float lastDamageTime = -Mathf.Infinity; // Czas ostatniego zadania obrażeń
    [SerializeField] private float damageCooldown = 1f;
    void Start()
    {
        mapLayer = UnityEngine.LayerMask.GetMask("Map");
        rb=GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
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
            if (hit.gameObject.tag == "Player")
                hitCheck = true;

        }

        if (hitCheck)
        {
            if (Time.time >= lastDamageTime + damageCooldown)
            {
                playerStats.GetDamage(Random.Range(enemyMovement.stats.damage - 5, enemyMovement.stats.damage + 5));
                knockbackEvent.Invoke();
                lastDamageTime = Time.time; // Zaktualizuj czas ostatniego zadania obrażeń
            }
            
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
