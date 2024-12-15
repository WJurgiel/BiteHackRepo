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
    [System.NonSerialized] public UnityEvent<int> e_getDamageEvent = new UnityEvent<int>();
    [System.NonSerialized] public UnityEvent<int> e_healhYourselfEvent = new UnityEvent<int>();
    [System.NonSerialized] public UnityEvent<int> e_healYourselfFullEvent = new UnityEvent<int>();
    [System.NonSerialized] public UnityEvent<int> e_increaseSpeedEvent = new UnityEvent<int>();
    [System.NonSerialized] public UnityEvent<int> e_increaseDefenseEvent = new UnityEvent<int>();
    [System.NonSerialized] public UnityEvent<int> e_increaseDamageEvent = new UnityEvent<int>();
    private void OnEnable()
    {
        HealYourselfFull();
    }
    
    public void GetDamage(int damage)
    {
        Debug.Log("event eventuje");
        health -= damage;
        e_getDamageEvent.Invoke(health);
    }

    public void HealYourself(int amount)
    {
        health += amount;
        e_healhYourselfEvent.Invoke(health);
    }

    public void HealYourselfFull()
    {
        health = maxHealth;
        e_healYourselfFullEvent.Invoke(0); // hardcoded
    }
    public void IncreaseSpeed(int toAdd)
    {
        speed += toAdd;
        e_increaseSpeedEvent.Invoke(speed);
    }

    public void IncreaseDefense(int toAdd)
    {
        defense += toAdd;
        e_increaseDefenseEvent.Invoke(defense);
    }

    public void IncreaseDamage(int toAdd)
    {
        damage += toAdd;
        e_increaseDamageEvent.Invoke(damage);
    }
}
