using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessController : MonoBehaviour
{
    [SerializeField] TimeManagerSO timeManagerSO;
    private Vignette vignette;
    private Volume volume;
    [SerializeField] private float targetVignette = 0.25f;
    [SerializeField] private float duration = 0.3f;
    private void Awake()
    {
        volume = GetComponent<Volume>();
    }

    private void Start()
    {
        vignette = volume.profile.TryGet<Vignette>(out var vignetteProfile) ? vignetteProfile : null;
        Debug.Log(vignette);
        timeManagerSO.e_EnterBulletTime.AddListener(ChangeVignette);
        timeManagerSO.e_ExitBulletTime.AddListener(ChangeVignette);
    }

    private IEnumerator ChangeVignetteIntensity(float target, float duration)
    {
        float startValue = vignette.intensity.value;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float t = elapsedTime / duration;
            
            vignette.intensity.value = Mathf.Lerp(startValue, target, t);
            yield return null;
        }
        vignette.intensity.value = target;
    }

    private void ChangeVignette()
    {
        float target = timeManagerSO.isbulletTimeOn ? targetVignette : 0.0f;
        StartCoroutine(ChangeVignetteIntensity(target, duration));
    }
}
