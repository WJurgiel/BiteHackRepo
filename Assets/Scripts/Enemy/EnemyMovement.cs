using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    GameObject player;
    [SerializeField] private float speed = 5f;
    private float distance;
    private float startChaseDistance = 3f;
    private bool chaseFlag = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);
        if (chaseFlag)
            Move();
        else
            CheckIfCanChase();  

    }

    private void Move()
    {

        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    private void CheckIfCanChase()
    {
        if (distance <= startChaseDistance)
        {
            chaseFlag = true;
        }
    }
}
