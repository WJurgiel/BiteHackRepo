using ScriptableObjects;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MagicStone : MonoBehaviour
{
    private SpriteRenderer _sr;
    private Light2D _light;
    [SerializeField] private Sprite[] stoneSprites;
    [SerializeField] private BoneSo bone;
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        bone.EPickup.AddListener(CheckBonesCount);
        _light = GetComponentInChildren<Light2D>();
    }
    void CheckBonesCount()
    {
        switch (bone.boneCurrent)
        {
            case < 10 when _sr.sprite != stoneSprites[0]:
                _sr.sprite = stoneSprites[0];
                _light.intensity = 0.0f;
                break;
            case >= 10 when _sr.sprite != stoneSprites[1]:
                _sr.sprite = stoneSprites[1];
                _light.intensity = 5f;
                break;
            case >= 20 when _sr.sprite != stoneSprites[2]:
                _sr.sprite = stoneSprites[2];
                _light.intensity = 10f;
                break;
        }
    }
}
