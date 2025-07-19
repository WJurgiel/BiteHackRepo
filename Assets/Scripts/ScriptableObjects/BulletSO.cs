using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "BulletSO", menuName = "ScriptableObjects/BulletSO")]
    public class BulletSo : ScriptableObject
    {
        public int speed;
        public int damage;
        public float range;
    }
}
