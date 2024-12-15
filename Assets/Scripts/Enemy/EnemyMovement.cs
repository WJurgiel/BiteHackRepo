using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    public StatsSO stats;
    GameObject player;
    private EnemyAttack knockbackEmitter;
    private Rigidbody2D rb;
    
    [SerializeField] private float speed = 5f;
    private float distance;
    private float startChaseDistance = 3f;
    private bool chaseFlag = false;
    private Vector2 direction;
    
    [SerializeField]
    private float KBForce = 0.3f;
    private float KBCounter = 0;
    [SerializeField]
    private float KBTotalTime = 0.5f;

    

    void Awake()
    {
        knockbackEmitter = GetComponent<EnemyAttack>();
        knockbackEmitter.knockbackEvent.AddListener(EnemyHit);
        knockbackEmitter.knockbackFromWallEvent.AddListener(EnemyHitWall);
        
        
    }

    void Start()
    {

        rb=GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);
        if (chaseFlag)
        {
            
            if (KBCounter <= 0)
            {
                direction = returnDirection();
                Rotate(direction);
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, stats.speed * Time.deltaTime);
            }
            else
            {
                Rotate(returnDirection());
                transform.position = Vector2.MoveTowards(transform.position,  transform.position + (transform.position - player.transform.position).normalized, KBForce * Time.deltaTime);
                KBCounter -= Time.deltaTime;
            }
        }
        else
        {
            CheckIfCanChase();
        }
    }

    private void Rotate(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    private Vector2 returnDirection()
    {
        return player.transform.position - transform.position;
    }

    private void CheckIfCanChase()
    {
        if (distance <= startChaseDistance)
        {
            chaseFlag = true;
        }
    }

    public void EnemyHit()
    {
        KBCounter = KBTotalTime;
    }
    public void EnemyHitWall()
    {
        KBCounter = 0.1f;
    }
    

}
