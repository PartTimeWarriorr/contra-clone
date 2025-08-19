using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossHeart : MonoBehaviour
{
    public static event Action OnBossHeartDestroyed;

    public Sprite bossDestroyedSprite;

    public GameObject boundary;
    public SpriteRenderer bossLowerSpriteRenderer;

    void OnDestroy()
    {
        OnBossHeartDestroyed?.Invoke();
        bossLowerSpriteRenderer.sprite = bossDestroyedSprite;
        boundary.SetActive(false);
    }
}
