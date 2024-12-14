using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/StatsSO", fileName = "StatsSO")]
public class StatsSO : ScriptableObject
{
    public int health;
    [SerializeField] private int maxHealth;
    public int speed;
    public int defense;
    public int damage;
    [System.NonSerialized] public UnityEvent<int> getDamageEvent = new UnityEvent<int>();
    private void OnEnable()
    {
        health = maxHealth;
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        getDamageEvent.Invoke(health);
    }
}
