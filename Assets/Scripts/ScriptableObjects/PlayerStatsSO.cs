using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerStatsSO", menuName = "ScriptableObjects/PlayerStatsSO")]
public class PlayerStatsSO : ScriptableObject
{
    public int bonesPicked;
    public int bonesToBePicked;
    public int dashSpeed;
    public float experience;
    public float expToNextLevel;
    public int level;
    public int equippedGunID;

    [SerializeField]
    private float expScaleFactor;
    [System.NonSerialized] public UnityEvent e_GetXP = new UnityEvent();
    [System.NonSerialized] public UnityEvent e_LevelUp = new UnityEvent();
    [System.NonSerialized] public UnityEvent e_pickBone = new UnityEvent();

    public void OnEnable()
    {
        //w sumie pamietac o wczytywaniu levela i innych statow z playerprefs
        if(level == 0 ) level = 1; 
        expToNextLevel = level * 100 * expScaleFactor; 
    }

    public void GetXP(int amount)
    {
        experience += amount;
        CheckIfLevelUp();
        e_GetXP.Invoke();
    }

    private void CheckIfLevelUp()
    {
        if (experience >= expToNextLevel)
        {
            level++;
            float toAdd = Mathf.Floor(experience - expToNextLevel);
            experience = toAdd;
            expToNextLevel = level * 100 * expScaleFactor;
            Debug.Log($"EXP: {experience}/{expToNextLevel}, Level: {level}");
            e_LevelUp.Invoke();
        }
    }

    public void PickBone()
    {
        bonesPicked++;
    }
    

}
