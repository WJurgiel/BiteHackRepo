using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerBase : MonoBehaviour
    {
        [SerializeField] private StatsSo stats;
        [SerializeField] private PlayerStatsSo playerStats;

        private int _exp;

        private void Start()
        {
            stats.ESetDead.AddListener(Die);
        }

        private static void Die()
        {
            SceneManager.LoadScene($"DeathScene");
        }

        public static int AddExp(int exp, int amount)
        {
            return exp + amount;
        }

    }
}
