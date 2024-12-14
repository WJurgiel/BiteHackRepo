using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           Debug.Log(collision.gameObject.name);
        }
    }

}
