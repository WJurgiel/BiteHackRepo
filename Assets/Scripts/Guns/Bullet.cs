using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Guns
{
    public class Bullet : MonoBehaviour
    {
        [FormerlySerializedAs("bulletSO")][SerializeField] private BulletSo bulletSo;

        private Vector3 _startPoint;
        void Start()
        {
            _startPoint = transform.position;
        }

        private void Update()
        {
            transform.Translate(Vector3.right * (bulletSo.speed * Time.deltaTime));
            Destroy(gameObject, 2f);
        }
    }
}
