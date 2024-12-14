using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundManager", menuName = "ScriptableObjects/SoundManager")]
public class SoundManagerSO : ScriptableObject
{
    public AudioSource SoundObject;
    [SerializeField] public TimeManagerSO timeManager;

    public float bulletTimePitch = 0.2f;
    public float normalPitch = 1f;
    
    public void PlaySoundFXClip(AudioClip clip, Vector3 soundPos, float volume)
    {
        float pitch = (timeManager.isbulletTimeOn) ? bulletTimePitch : normalPitch;
        AudioSource a = Instantiate(SoundObject, soundPos, Quaternion.identity);
        a.tag = "SFXPlayer";

        a.clip = clip;
        a.volume = volume;
        a.pitch = pitch;
        a.Play();
    }
    public void PlaySoundFXClip(AudioClip[] clips, Vector3 soundPos, float volume)
    {
        int randClip = Random.Range(0, clips.Length);
        float pitch = (timeManager.isbulletTimeOn) ? bulletTimePitch : normalPitch;
        AudioSource a = Instantiate(SoundObject, soundPos, Quaternion.identity);

        a.clip = clips[randClip];
        a.volume = volume;
        a.pitch = pitch;
        a.Play();
    }
}
