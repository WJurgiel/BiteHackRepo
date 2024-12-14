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
    //Call this to get know if the player or enemy is damaged
    [System.NonSerialized] public UnityEvent<int> getDamageEvent = new UnityEvent<int>();
    [System.NonSerialized] public UnityEvent<int> healhYourselfEvent = new UnityEvent<int>();
    [System.NonSerialized] public UnityEvent<int> healYourselfFullEvent = new UnityEvent<int>();
    [System.NonSerialized] public UnityEvent<int> increaseSpeedEvent = new UnityEvent<int>();
    [System.NonSerialized] public UnityEvent<int> increaseDefenseEvent = new UnityEvent<int>();
    [System.NonSerialized] public UnityEvent<int> increaseDamageEvent = new UnityEvent<int>();
    private void OnEnable()
    {
        health = maxHealth;
    }
    
    public void GetDamage(int damage)
    {
        Debug.Log("event eventuje");
        health -= damage;
        getDamageEvent.Invoke(health);
    }

    public void HealYourself(int amount)
    {
        health += amount;
        healhYourselfEvent.Invoke(health);
    }

    public void HealYourselfFull()
    {
        health = maxHealth;
        healYourselfFullEvent.Invoke(0); // hardcoded
    }
    public void IncreaseSpeed(int toAdd)
    {
        speed += toAdd;
        increaseSpeedEvent.Invoke(speed);
    }

    public void IncreaseDefense(int toAdd)
    {
        defense += toAdd;
        increaseDefenseEvent.Invoke(defense);
    }

    public void IncreaseDamage(int toAdd)
    {
        damage += toAdd;
        increaseDamageEvent.Invoke(damage);
    }
}
