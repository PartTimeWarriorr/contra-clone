using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject healthBar;
    private int healthPoints;

    private GameObject levelEndPanel;

    void Awake()
    {
        healthBar = GameObject.Find("HealthBar");
        levelEndPanel = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        healthPoints = healthBar.transform.childCount;
    }

    void Start()
    {
        Debug.Log(levelEndPanel);
    }

    void OnEnable()
    {
        PlayerHealth.OnHealthChanged += DrawHealth;
        LevelExit.OnLevelComplete += LevelEndPopUp;
    }

    void OnDisable()
    {
        PlayerHealth.OnHealthChanged -= DrawHealth;
        LevelExit.OnLevelComplete -= LevelEndPopUp;
    }

    void DrawHealth(int health)
    {
        // Starting at the rightmost healthpoint, removes images to equal health
        for (int i = healthPoints - 1; i >= health; i--)
        {
            healthBar.transform.GetChild(i).GetComponent<Image>().enabled = false;
        }
    }

    void LevelEndPopUp()
    {
        levelEndPanel.SetActive(true);
    }
}
