using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MagicStone : MonoBehaviour
{
    private SpriteRenderer sr;
    private new Light2D light;
    [SerializeField] private Sprite[] stoneSprites;
    [SerializeField] private BoneSO bone;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bone.e_Pickup.AddListener(CheckBonesCount);
        light = GetComponentInChildren<Light2D>();
    }
    void CheckBonesCount()
    {
        if (bone.boneCurrent < 10 && sr.sprite != stoneSprites[0])
        {
            sr.sprite = stoneSprites[0];
            light.intensity = 0.0f;
        }
        else if (bone.boneCurrent >= 10 && sr.sprite != stoneSprites[1])
        {
            sr.sprite = stoneSprites[1];
            light.intensity = 5f;
        }
        else if (bone.boneCurrent >= 20 && sr.sprite != stoneSprites[2])
        {
            sr.sprite = stoneSprites[2];
            light.intensity = 10f;
        }
    }
}
