using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class StatisticsUI : MonoBehaviour
    {
        [SerializeField] private StatsSo playerGeneralStats;
        [FormerlySerializedAs("playerStatsSO")][SerializeField] private PlayerStatsSo playerStatsSo;
        [SerializeField] private TimeManagerSo timeManager;
        [SerializeField] private GunSo gun;
        [SerializeField] private BoneSo bones;
        [FormerlySerializedAs("HealthImg")][SerializeField] private Image healthImg;

        [FormerlySerializedAs("BulletTimeImg")][SerializeField] private Image bulletTimeImg;
        // private Image
        [FormerlySerializedAs("CurrentAmmoTMP")][SerializeField] private TMP_Text currentAmmoTMP;
        [FormerlySerializedAs("BonesPickedTMP")][SerializeField] private TMP_Text bonesPickedTMP;
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

            playerGeneralStats.EHealthYourselfEvent.AddListener(HealthUpdate);
            playerGeneralStats.EGetDamageEvent.AddListener(HealthUpdate);

            playerGeneralStats.EIncreaseDamageEvent.AddListener(DamageTMPUpdate);
            playerGeneralStats.EIncreaseSpeedEvent.AddListener(SpeedTMPUpdate);
            playerGeneralStats.EIncreaseDefenseEvent.AddListener(DefenseTMPUpdate);

            timeManager.EUpdateBulletTime.AddListener(BulletTimeUpdate);
            gun.EShoot.AddListener(AmmoTMPUpdate);
            gun.EReload.AddListener(AmmoTMPUpdate);

            bones.EPickup.AddListener(BonesPickedTMPUpdate);
            bones.EReset.AddListener((BonesPickedTMPUpdate));
        }


        private void HealthUpdate(int health = -1)
        {
            healthImg.fillAmount = (float)(playerGeneralStats.health) / playerGeneralStats.maxHealth;
        }

        private void DamageTMPUpdate(int amount = -1)
        {
        }

        private void SpeedTMPUpdate(int amount = -1)
        {
        }

        private void DefenseTMPUpdate(int amount = -1)
        {
        }

        private void BulletTimeUpdate()
        {
            bulletTimeImg.fillAmount = timeManager.bulletTimeAmount / timeManager.maxBulletTimeAmount;
        }

        private void AmmoTMPUpdate()
        {
            currentAmmoTMP.color = gun.ammoCurrent < 5 ? Color.red : Color.white;
            currentAmmoTMP.text = $"{gun.ammoCurrent}";
        }

        private void BonesPickedTMPUpdate()
        {
            bonesPickedTMP.text = $"{bones.boneCurrent}/{bones.boneMax}";
        }
    }
}
