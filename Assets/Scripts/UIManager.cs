using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject healthBar;
    private int healthPoints;

    void Awake()
    {
        healthBar = GameObject.Find("HealthBar");
        healthPoints = healthBar.transform.childCount;
    }

    void OnEnable()
    {
        PlayerHealth.OnHealthChanged += DrawHealth;
    }

    void OnDisable()
    {
        PlayerHealth.OnHealthChanged -= DrawHealth;
    }

    void DrawHealth(int health)
    {
        // Starting at the rightmost healthpoint, removes images to equal health
        for (int i = healthPoints - 1; i >= health; i--)
        {
            healthBar.transform.GetChild(i).GetComponent<Image>().enabled = false;
        }
    }
}
