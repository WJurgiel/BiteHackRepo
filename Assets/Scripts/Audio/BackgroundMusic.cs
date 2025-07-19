using System;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;
    [SerializeField] private AudioClip[] backgroundMusics; // i mean there should be 3, for each level
    [SerializeField] private TimeManagerSO timeManagerSO;
    private AudioSource audioSource;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timeManagerSO.e_EnterBulletTime.AddListener(ChangePitch);
        timeManagerSO.e_ExitBulletTime.AddListener(ChangePitch);


        audioSource.clip = backgroundMusics[0]; // change to the first clip
        audioSource.Play();
    }
    private void ChangePitch()
    {
        audioSource.pitch = (timeManagerSO.isbulletTimeOn) ? timeManagerSO.bulletTimePitch : 1f;
    }
}
