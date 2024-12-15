using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public AudioClip shootSFX;
    [SerializeField] SoundManagerSO soundManagerSO;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            soundManagerSO.PlaySoundFXClip(shootSFX, transform.position, 1f);
            Vector3 mousePosition = Input.mousePosition;             
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); 
            mousePosition.z = 0;                                    

            Vector3 direction = (mousePosition - transform.position).normalized; 
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
