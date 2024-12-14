using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public AudioClip _shootSFX;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("shootin");
            SoundManagerSO.PlaySoundFXClip(_shootSFX, transform.position, 1f);
        }
    }
}
