using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GunSO", menuName = "ScriptableObjects/GunSO")]
public class GunSO : ScriptableObject
{
    public float timeBetweenShots = 0.2f; //firerate
    public int ammoCurrent;
    public int maxAmmo = 20;
    public float reloadTime = 0.5f;
    [System.NonSerialized] public UnityEvent e_Shoot = new UnityEvent();
    [System.NonSerialized] public UnityEvent e_Reload = new UnityEvent();
    private void OnEnable()
    {
        ammoCurrent = maxAmmo;
    }

    public void Shoot()
    {
        ammoCurrent--;
        e_Shoot.Invoke();
    }
    public void Reload()
    {
        ammoCurrent = maxAmmo;
        e_Reload.Invoke();
    }

}
