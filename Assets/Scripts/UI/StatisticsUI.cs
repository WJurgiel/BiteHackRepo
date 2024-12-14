using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class StatisticsUI : MonoBehaviour
{
    [SerializeField] private StatsSO playerGeneralStats;
    [SerializeField] private TimeManagerSO timeManager;
    private TMP_Text healthTMP;
    private TMP_Text damageTMP;
    private TMP_Text speedTMP;
    private TMP_Text defenseTMP;
    private TMP_Text bulletTimeTMP;

    void Awake()
    {
        healthTMP = transform.GetChild(0).GetComponent<TMP_Text>();
        speedTMP = transform.GetChild(1).GetComponentInChildren<TMP_Text>();
        defenseTMP = transform.GetChild(2).GetComponentInChildren<TMP_Text>();
        damageTMP = transform.GetChild(3).GetComponentInChildren<TMP_Text>();
        bulletTimeTMP = transform.GetChild(4).GetComponentInChildren<TMP_Text>();
    }
    void Start()
    {
        HealthTMPUpdate();
        DamageTMPUpdate();
        SpeedTMPUpdate();
        DefenseTMPUpdate();
        BulletTimeTMPUpdate();
        
        playerGeneralStats.e_healhYourselfEvent.AddListener(HealthTMPUpdate);
        playerGeneralStats.e_getDamageEvent.AddListener(HealthTMPUpdate);
        
        playerGeneralStats.e_increaseDamageEvent.AddListener(DamageTMPUpdate);
        playerGeneralStats.e_increaseSpeedEvent.AddListener(SpeedTMPUpdate);
        playerGeneralStats.e_increaseDefenseEvent.AddListener(DefenseTMPUpdate);
        
        timeManager.e_UpdateBulletTime.AddListener(BulletTimeTMPUpdate);
        Debug.Log(healthTMP.text);
    }


    private void HealthTMPUpdate(int health=-1)
    {
        healthTMP.text = playerGeneralStats.health.ToString();
    }

    private void DamageTMPUpdate(int amount=-1)
    {
        damageTMP.text = playerGeneralStats.damage.ToString();
    }

    private void SpeedTMPUpdate(int amount=-1)
    {
        speedTMP.text = playerGeneralStats.speed.ToString();
    }

    private void DefenseTMPUpdate(int amount=-1)
    {
        defenseTMP.text = playerGeneralStats.defense.ToString();
    }

    private void BulletTimeTMPUpdate()
    {
        bulletTimeTMP.text = $"{timeManager.bulletTimeAmount:F2}/{timeManager.maxBulletTimeAmount:F2}";
    }
}
