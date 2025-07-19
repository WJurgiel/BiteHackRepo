using System.Collections;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        [FormerlySerializedAs("shootSFX")] public AudioClip shootSfx;
        [FormerlySerializedAs("reloadSFX")] public AudioClip reloadSfx;
        [FormerlySerializedAs("soundManagerSO")]
        [SerializeField]
        private SoundManagerSo soundManagerSo;
        [FormerlySerializedAs("gunSO")][SerializeField] private GunSo gunSo;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawn;

        private bool _isReloading;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            if (Input.GetButtonDown("Fire1") && !_isReloading && gunSo.ammoCurrent > 0)
            {
                gunSo.Shoot();

                soundManagerSo.PlaySoundFXClip(shootSfx, transform.position, 1f);
                var mousePosition = Input.mousePosition;
                if (_camera) mousePosition = _camera.ScreenToWorldPoint(mousePosition);
                mousePosition.z = 0;

                var direction = (mousePosition - transform.position).normalized;
                var bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(WaitForReload());
            }
        }

        private IEnumerator WaitForReload()
        {
            _isReloading = true;
            soundManagerSo.PlaySoundFXClip(reloadSfx, transform.position, 1f);
            yield return new WaitForSeconds(gunSo.reloadTime);
            gunSo.Reload();
            soundManagerSo.PlaySoundFXClip(reloadSfx, transform.position, 1f);
            _isReloading = false;
        }

    }
}
