using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StatisticsUI : MonoBehaviour
{
    [SerializeField] private StatsSO playerGeneralStats;
    [SerializeField] private PlayerStatsSO playerStatsSO;
    [SerializeField] private TimeManagerSO timeManager;
    [SerializeField] private GunSO gun;
    [SerializeField] private BoneSO bones;
    [SerializeField] private Image HealthImg;

    [SerializeField] private Image BulletTimeImg;
    // private Image
    [SerializeField] private TMP_Text CurrentAmmoTMP;
    [SerializeField] private TMP_Text BonesPickedTMP;
    void Awake()
    {
        
    }
    void Start()
    {
        bones.boneCurrent = 0;
        playerGeneralStats.health = playerGeneralStats.maxHealth;
        timeManager.bulletTimeAmount = timeManager.maxBulletTimeAmount;
        gun.ammoCurrent = gun.maxAmmo;
        HealthUpdate();
        BulletTimeUpdate();
        AmmoTMPUpdate();
        BonesPickedTMPUpdate();
        
        playerGeneralStats.e_healhYourselfEvent.AddListener(HealthUpdate);
        playerGeneralStats.e_getDamageEvent.AddListener(HealthUpdate);
        
        playerGeneralStats.e_increaseDamageEvent.AddListener(DamageTMPUpdate);
        playerGeneralStats.e_increaseSpeedEvent.AddListener(SpeedTMPUpdate);
        playerGeneralStats.e_increaseDefenseEvent.AddListener(DefenseTMPUpdate);
        
        timeManager.e_UpdateBulletTime.AddListener(BulletTimeUpdate);
        gun.e_Shoot.AddListener(AmmoTMPUpdate);
        gun.e_Reload.AddListener(AmmoTMPUpdate);
        
        bones.e_Pickup.AddListener(BonesPickedTMPUpdate);
        bones.e_Reset.AddListener((BonesPickedTMPUpdate));
        
        // Debug.Log(healthTMP.text);
    }


    private void HealthUpdate(int health=-1)
    {
        HealthImg.fillAmount = (float)(playerGeneralStats.health) / playerGeneralStats.maxHealth;
    }

    private void DamageTMPUpdate(int amount=-1)
    {
    }

    private void SpeedTMPUpdate(int amount=-1)
    {
    }

    private void DefenseTMPUpdate(int amount=-1)
    {
    }

    private void BulletTimeUpdate()
    {
        BulletTimeImg.fillAmount = timeManager.bulletTimeAmount / timeManager.maxBulletTimeAmount;
    }

    private void AmmoTMPUpdate()
    {
        if (gun.ammoCurrent < 5) CurrentAmmoTMP.color = Color.red;
        else CurrentAmmoTMP.color = Color.white;
        CurrentAmmoTMP.text = $"{gun.ammoCurrent}";
    }

    private void BonesPickedTMPUpdate()
    {
        BonesPickedTMP.text = $"{bones.boneCurrent}/{bones.boneMax}";
    }
}
