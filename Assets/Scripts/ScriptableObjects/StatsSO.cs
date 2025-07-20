using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/StatsSO", fileName = "StatsSO")]
    public class StatsSo : ScriptableObject
    {
        public int health;
        public int maxHealth;
        public int speed;
        public int defense;
        public int damage;
        [System.NonSerialized] public readonly UnityEvent<int> EGetDamageEvent = new UnityEvent<int>();
        [System.NonSerialized] public readonly UnityEvent<int> EHealthYourselfEvent = new UnityEvent<int>();
        [System.NonSerialized] private readonly UnityEvent<int> _eHealYourselfFullEvent = new UnityEvent<int>();
        [System.NonSerialized] public readonly UnityEvent<int> EIncreaseSpeedEvent = new UnityEvent<int>();
        [System.NonSerialized] public readonly UnityEvent<int> EIncreaseDefenseEvent = new UnityEvent<int>();
        [System.NonSerialized] public readonly UnityEvent<int> EIncreaseDamageEvent = new UnityEvent<int>();
        [System.NonSerialized] public readonly UnityEvent ESetDead = new UnityEvent();

        private void OnEnable()
        {
            HealYourselfFull();
        }

        public void GetDamage(int damageAmount)
        {
            health -= damageAmount;
            CheckIfDead();
            Debug.Log(health);
            EGetDamageEvent.Invoke(health);
        }

        private void CheckIfDead()
        {
            if (health <= 0)
            {
                ESetDead.Invoke();
            }
        }
        public void HealYourself(int amount)
        {
            health += amount;
            EHealthYourselfEvent.Invoke(health);
        }

        private void HealYourselfFull()
        {
            health = maxHealth;
            _eHealYourselfFullEvent.Invoke(0); // hardcoded
        }
        public void IncreaseSpeed(int toAdd)
        {
            speed += toAdd;
            EIncreaseSpeedEvent.Invoke(speed);
        }

        public void IncreaseDefense(int toAdd)
        {
            defense += toAdd;
            EIncreaseDefenseEvent.Invoke(defense);
        }

        public void IncreaseDamage(int toAdd)
        {
            damage += toAdd;
            EIncreaseDamageEvent.Invoke(damage);
        }


    }
}
