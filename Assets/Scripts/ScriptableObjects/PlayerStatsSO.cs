using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerStatsSO", menuName = "ScriptableObjects/PlayerStatsSO")]
    public class PlayerStatsSo : ScriptableObject
    {
        public int bonesPicked;
        public int bonesToBePicked;
        public int dashSpeed;
        public float experience;
        public float expToNextLevel;
        public int level;
        public int equippedGunID;

        [SerializeField]
        private float expScaleFactor;
        [System.NonSerialized] private readonly UnityEvent _eGetXp = new UnityEvent();
        [System.NonSerialized] private readonly UnityEvent _eLevelUp = new UnityEvent();

        public void OnEnable()
        {
            if (level == 0) level = 1;
            expToNextLevel = level * 100 * expScaleFactor;
        }
        public void GetXp(int amount)
        {
            experience += amount;
            CheckIfLevelUp();
            _eGetXp.Invoke();
        }
        private void CheckIfLevelUp()
        {
            if (experience >= expToNextLevel)
            {
                level++;
                float toAdd = Mathf.Floor(experience - expToNextLevel);
                experience = toAdd;
                expToNextLevel = level * 100 * expScaleFactor;
                Debug.Log($"EXP: {experience}/{expToNextLevel}, Level: {level}");
                _eLevelUp.Invoke();
            }
        }
    }
}
