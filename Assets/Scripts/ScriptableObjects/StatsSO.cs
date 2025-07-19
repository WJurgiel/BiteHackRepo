using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/StatsSO", fileName = "StatsSO")]
public class StatsSO : ScriptableObject
{
    public int health;
    public int maxHealth;
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
    [System.NonSerialized] public UnityEvent e_SetDead = new UnityEvent();

    private void OnEnable()
    {
        HealYourselfFull();
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        CheckIfDead();
        Debug.Log(health);
        e_getDamageEvent.Invoke(health);
    }

    private void CheckIfDead()
    {
        if (health <= 0)
        {
            e_SetDead.Invoke();
        }
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
