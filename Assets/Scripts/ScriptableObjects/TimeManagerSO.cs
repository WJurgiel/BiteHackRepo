using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "TimeManager", menuName = "ScriptableObjects/TimeManagerSO")]
    public class TimeManagerSo : ScriptableObject
    {
        public float bulletTimeScale = 0.2f;

        public float normalTimeScale = 1.0f;
        public float regainBulletTimeScale = 0.5f;

        public float bulletTimeAmount;
        public float maxBulletTimeAmount;
        [FormerlySerializedAs("isbulletTimeOn")] public bool isBulletTimeOn;

        public float bulletTimePitch = 0.8f;
        [NonSerialized] public readonly UnityEvent EEnterBulletTime = new UnityEvent();
        [NonSerialized] public readonly UnityEvent EExitBulletTime = new UnityEvent();
        [NonSerialized] public readonly UnityEvent EUpdateBulletTime = new UnityEvent();

        private MonoBehaviour _coroutineRunner;

        public void Initialize(MonoBehaviour runner)
        {
            _coroutineRunner = runner;
        }
        private void OnEnable()
        {
            if (maxBulletTimeAmount == 0) Debug.Log("[TimeManagerSO] Max bullet time amount is 0");
            isBulletTimeOn = false;

            bulletTimeAmount = maxBulletTimeAmount;
            Time.timeScale = normalTimeScale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }

        public void EnterBulletTime()
        {
            if (isBulletTimeOn || bulletTimeAmount <= 0) return;

            isBulletTimeOn = true;
            Time.timeScale = bulletTimeScale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            EEnterBulletTime.Invoke();

            if (_coroutineRunner != null)
            {
                _coroutineRunner.StartCoroutine(DrainBulletTime());
            }
            else
            {
                Debug.LogError("[TimeManagerSO] No coroutine runner has been added");
            }
        }

        private IEnumerator DrainBulletTime()
        {
            while (isBulletTimeOn && bulletTimeAmount > 0)
            {
                bulletTimeAmount -= Time.unscaledDeltaTime * regainBulletTimeScale;
                EUpdateBulletTime.Invoke();
                yield return null;
            }
            if (bulletTimeAmount <= 0) bulletTimeAmount = 0;
            EUpdateBulletTime.Invoke();
            ExitBulletTime();
        }

        private IEnumerator RegainBulletTime()
        {
            while (!isBulletTimeOn && bulletTimeAmount < maxBulletTimeAmount)
            {
                bulletTimeAmount += Time.unscaledDeltaTime * regainBulletTimeScale;
                EUpdateBulletTime.Invoke(); // I think it's bad, but maybe it isn't? Who knows
                yield return null;
            }
            // bulletTimeAmount = maxBulletTimeAmount;
            EUpdateBulletTime.Invoke();
        }
        public void ExitBulletTime()
        {
            if (!isBulletTimeOn) return;

            isBulletTimeOn = false;

            Time.timeScale = normalTimeScale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            if (_coroutineRunner != null)
            {
                _coroutineRunner.StartCoroutine(RegainBulletTime());
            }
            EExitBulletTime.Invoke();
        }

    }
}
