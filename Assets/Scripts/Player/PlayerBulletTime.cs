using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerBulletTime : MonoBehaviour
    {
        [SerializeField] private TimeManagerSo timeManager;

        private void Awake()
        {
            timeManager.Initialize(this);
        }
        void Update()
        {
            SwitchBulletTime();
        }
        void SwitchBulletTime()
        {
            if (Input.GetMouseButtonDown(1) && !timeManager.isBulletTimeOn)
            {
                timeManager.EnterBulletTime();
            }
            else if (Input.GetMouseButtonDown(1) && timeManager.isBulletTimeOn)
            {
                timeManager.ExitBulletTime();
            }
        }
    }
}
