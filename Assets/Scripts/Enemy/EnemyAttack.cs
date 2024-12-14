using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           Debug.Log(collision.gameObject.name);
        }
    }

}
