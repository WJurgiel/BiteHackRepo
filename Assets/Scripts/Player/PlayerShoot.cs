using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public AudioClip shootSFX;
    public AudioClip reloadSFX;
    [SerializeField] SoundManagerSO soundManagerSO;
    [SerializeField] private GunSO gunSO;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;

    private bool isReloading = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isReloading && gunSO.ammoCurrent > 0)
        {
            gunSO.Shoot();

            soundManagerSO.PlaySoundFXClip(shootSFX, transform.position, 1f);
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0;

            Vector3 direction = (mousePosition - transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(WaitForReload());
        }
    }

    private IEnumerator WaitForReload()
    {
        isReloading = true;
        soundManagerSO.PlaySoundFXClip(reloadSFX, transform.position, 1f);
        yield return new WaitForSeconds(gunSO.reloadTime);
        gunSO.Reload();
        soundManagerSO.PlaySoundFXClip(reloadSFX, transform.position, 1f);
        isReloading = false;
    }

}
