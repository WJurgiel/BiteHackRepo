using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Audio
{
    public class BackgroundMusic : MonoBehaviour
    {
        private static BackgroundMusic _instance;
        [SerializeField] private AudioClip[] backgroundMusics; // i mean there should be 3, for each level
        [FormerlySerializedAs("timeManagerSO")][SerializeField] private TimeManagerSo timeManagerSo;
        private AudioSource _audioSource;
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            timeManagerSo.EEnterBulletTime.AddListener(ChangePitch);
            timeManagerSo.EExitBulletTime.AddListener(ChangePitch);


            _audioSource.clip = backgroundMusics[0]; // change to the first clip
            _audioSource.Play();

            if (PlayerPrefs.HasKey("volume"))
            {
                AudioListener.volume = PlayerPrefs.GetFloat("volume");
            }
        }
        private void ChangePitch()
        {
            _audioSource.pitch = (timeManagerSo.isBulletTimeOn) ? timeManagerSo.bulletTimePitch : 1f;
        }
    }
}
