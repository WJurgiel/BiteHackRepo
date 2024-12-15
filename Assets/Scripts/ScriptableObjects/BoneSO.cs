using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BoneSO", menuName = "ScriptableObjects/BoneSO", order = 1)]
public class BoneSO : ScriptableObject
{
    public int boneCurrent;
    public int boneMax;
    
    public void OnEnable()
    {
        boneCurrent = 0; 
    }
    [NonSerialized] public UnityEvent e_Pickup = new UnityEvent();

    public void AddBone()
    {
        boneCurrent += 1;
        e_Pickup.Invoke();
    }
}
