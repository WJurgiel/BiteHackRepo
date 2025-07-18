using System;
using UnityEngine;

public class PlayerBulletTime : MonoBehaviour
{
    [SerializeField] TimeManagerSO timeManager;

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
        if (Input.GetMouseButtonDown(1) && !timeManager.isbulletTimeOn)
        {
            timeManager.EnterBulletTime();
        }
        else if (Input.GetMouseButtonDown(1) && timeManager.isbulletTimeOn)
        {
            timeManager.ExitBulletTime();
        }
    }
}
