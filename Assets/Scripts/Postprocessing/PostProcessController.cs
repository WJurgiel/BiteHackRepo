using System.Collections;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace Postprocessing
{
    public class PostProcessController : MonoBehaviour
    {
        [FormerlySerializedAs("timeManagerSO")]
        [SerializeField]
        private TimeManagerSo timeManagerSo;
        private Vignette _vignette;
        private Volume _volume;
        [SerializeField] private float targetVignette = 0.25f;
        [SerializeField] private float duration = 0.3f;
        private void Awake()
        {
            _volume = GetComponent<Volume>();
        }

        private void Start()
        {
            _vignette = _volume.profile.TryGet<Vignette>(out var vignetteProfile) ? vignetteProfile : null;
            Debug.Log(_vignette);
            timeManagerSo.EEnterBulletTime.AddListener(ChangeVignette);
            timeManagerSo.EExitBulletTime.AddListener(ChangeVignette);
        }

        private IEnumerator ChangeVignetteIntensity(float target, float durationTime)
        {
            var startValue = _vignette.intensity.value;
            var elapsedTime = 0f;

            while (elapsedTime < durationTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                var t = elapsedTime / durationTime;

                _vignette.intensity.value = Mathf.Lerp(startValue, target, t);
                yield return null;
            }
            _vignette.intensity.value = target;
        }

        private void ChangeVignette()
        {
            var target = timeManagerSo.isBulletTimeOn ? targetVignette : 0.0f;
            StartCoroutine(ChangeVignetteIntensity(target, duration));
        }
    }
}
