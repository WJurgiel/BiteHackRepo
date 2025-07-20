using System;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "BoneSO", menuName = "ScriptableObjects/BoneSO", order = 1)]
    public class BoneSo : ScriptableObject
    {
        public int boneCurrent;
        public int boneMax;

        public void OnEnable()
        {
            boneCurrent = 0;
        }
        [NonSerialized] public readonly UnityEvent EPickup = new UnityEvent();
        [NonSerialized] public readonly UnityEvent EReset = new UnityEvent();

        public void AddBone()
        {
            boneCurrent += 1;
            EPickup.Invoke();
        }
    }
}
