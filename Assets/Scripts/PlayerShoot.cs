using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public AudioClip shootSFX;
    [SerializeField] SoundManagerSO soundManagerSO;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            soundManagerSO.PlaySoundFXClip(shootSFX, transform.position, 1f);
        }
    }
}
