using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsSO", menuName = "ScriptableObjects/PlayerStatsSO")]
public class PlayerStatsSO : ScriptableObject
{
    public int dashSpeed;
    public int experience;
    public int expToNextLevel;
    public int level;
    public int equippedGunID;
    
 
}
