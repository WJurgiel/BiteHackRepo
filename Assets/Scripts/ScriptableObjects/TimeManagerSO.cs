using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "TimeManager", menuName = "ScriptableObjects/TimeManagerSO")]
public class TimeManagerSO : ScriptableObject
{
    public float bulletTimeScale = 0.2f;

    public float normalTimeScale = 1.0f;
    public float regainBulletTimeScale = 0.5f;

    public float bulletTimeAmount;
    public float maxBulletTimeAmount;
    public bool isbulletTimeOn = false;

    public float bulletTimePitch = 0.8f;
    [NonSerialized] public UnityEvent e_EnterBulletTime = new UnityEvent();
    [NonSerialized] public UnityEvent e_ExitBulletTime = new UnityEvent();
    //Be careful with this one, it's invoked everytime in Coroutine 
    [NonSerialized] public UnityEvent e_UpdateBulletTime = new UnityEvent();

    private MonoBehaviour coroutineRunner;

    public void Initialize(MonoBehaviour runner)
    {
        coroutineRunner = runner;
    }
    private void OnEnable()
    {
        if (maxBulletTimeAmount == 0) Debug.Log("[TimeManagerSO] Max bullet time amount is 0");
        isbulletTimeOn = false;

        bulletTimeAmount = maxBulletTimeAmount;
        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; ;
    }

    public void EnterBulletTime()
    {
        if (isbulletTimeOn || bulletTimeAmount <= 0) return; // Sprawdź, czy można aktywować bullet-time

        isbulletTimeOn = true;
        Time.timeScale = bulletTimeScale; // Włącz slow-motion
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        e_EnterBulletTime.Invoke(); // Wywołaj event

        // Rozpocznij asynchroniczne odliczanie
        if (coroutineRunner != null)
        {
            coroutineRunner.StartCoroutine(DrainBulletTime());
        }
        else
        {
            Debug.LogError("[TimeManagerSO] No coroutine runner has been added");
        }
    }

    private IEnumerator DrainBulletTime()
    {
        while (isbulletTimeOn && bulletTimeAmount > 0)
        {
            bulletTimeAmount -= Time.unscaledDeltaTime * regainBulletTimeScale; // Zmniejsz czas niezależnie od Time.timeScale
            e_UpdateBulletTime.Invoke();
            yield return null;
        }
        if (bulletTimeAmount <= 0) bulletTimeAmount = 0;
        e_UpdateBulletTime.Invoke();
        ExitBulletTime();
    }

    private IEnumerator RegainBulletTime()
    {
        while (!isbulletTimeOn && bulletTimeAmount < maxBulletTimeAmount)
        {
            bulletTimeAmount += Time.unscaledDeltaTime * regainBulletTimeScale;
            e_UpdateBulletTime.Invoke(); // I think it's bad, but maybe it isn't? Who knows
            yield return null;
        }
        // bulletTimeAmount = maxBulletTimeAmount;
        e_UpdateBulletTime.Invoke();
    }
    public void ExitBulletTime()
    {
        if (!isbulletTimeOn) return;

        isbulletTimeOn = false;

        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        if (coroutineRunner != null)
        {
            coroutineRunner.StartCoroutine(RegainBulletTime());
        }
        e_ExitBulletTime.Invoke();
    }

}
