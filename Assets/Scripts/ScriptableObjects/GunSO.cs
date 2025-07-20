using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GunSO", menuName = "ScriptableObjects/GunSO")]
    public class GunSo : ScriptableObject
    {
        public float timeBetweenShots = 0.2f;
        public int ammoCurrent;
        public int maxAmmo = 20;
        public float reloadTime = 0.5f;
        [System.NonSerialized] public readonly UnityEvent EShoot = new UnityEvent();
        [System.NonSerialized] public readonly UnityEvent EReload = new UnityEvent();
        private void OnEnable()
        {
            ammoCurrent = maxAmmo;
        }
        public void Shoot()
        {
            ammoCurrent--;
            EShoot.Invoke();
        }
        public void Reload()
        {
            ammoCurrent = maxAmmo;
            EReload.Invoke();
        }
    }
}
