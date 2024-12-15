using UnityEngine;
[CreateAssetMenu(fileName = "BoneSO", menuName = "ScriptableObjects/BoneSO", order = 1)]
public class BoneSO : ScriptableObject
{
    public int boneCurrent;
    public int boneMax;
    
    public void OnEnable()
    {
        boneCurrent = 0; 
    }
}
