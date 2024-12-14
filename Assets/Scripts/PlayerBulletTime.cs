using System;
using UnityEditor.Embree;
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
        _testing_bulletTime();
    }
    void _testing_bulletTime()
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
