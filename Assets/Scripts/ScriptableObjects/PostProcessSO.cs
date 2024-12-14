// using System;
// using System.Collections;
// using UnityEngine;
// using UnityEngine.Rendering;
// using UnityEngine.Rendering.Universal;
//
// [CreateAssetMenu(fileName = "PostProcessSO", menuName = "ScriptableObjects/PostProcessSO")]
// public class PostProcessSO : ScriptableObject
// {
//     public Volume postProcessVolume;
//     private Vignette vignette;
//
//     private void OnEnable()
//     {
//         if (postProcessVolume.profile.TryGet(out Vignette v))
//         {
//             vignette = v;
//         }
//         else
//         {
//             Debug.LogError("Vignette effect not found in the Volume Profile!");
//         }
//     }
//     public void ActivateVignette(float targetIntensity, float duration)
//     {
//         if (vignette != null)
//         {
//             // Uruchom Coroutine do zmiany intensywności
//             CoroutineManager.Instance.StartCoroutine(ChangeVignetteIntensity(targetIntensity, duration));
//         }
//     }
//
//     // Metoda do płynnego wyłączania efektu Vignette
//     public void DeactivateVignette(float targetIntensity, float duration)
//     {
//         if (vignette != null)
//         {
//             CoroutineManager.Instance.StartCoroutine(ChangeVignetteIntensity(targetIntensity, duration));
//         }
//     }
//
//     // Coroutine do płynnej zmiany intensywności Vignette
//     private IEnumerator ChangeVignetteIntensity(float targetIntensity, float duration)
//     {
//         float startIntensity = vignette.intensity.value;
//         float elapsedTime = 0f;
//
//         while (elapsedTime < duration)
//         {
//             elapsedTime += Time.deltaTime;
//             float t = elapsedTime / duration;
//
//             vignette.intensity.value = Mathf.Lerp(startIntensity, targetIntensity, t);
//             yield return null;
//         }
//
//         vignette.intensity.value = targetIntensity;
//     }
// }
// }
