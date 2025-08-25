using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject healthBar;
    private int healthPoints;

    public GameObject canvas;

    private GameObject levelEndPanel;
    private GameObject audioIcon;
    private GameObject particlesIcon;
    private GameObject godModeIcon;

    void Awake()
    {
        healthBar = canvas.transform.Find("HealthBar").gameObject;
        levelEndPanel = canvas.transform.Find("LevelEndPanel").gameObject;
        audioIcon = canvas.transform.Find("AudioIcon").gameObject;
        particlesIcon = canvas.transform.Find("ParticlesIcon").gameObject;
        godModeIcon = canvas.transform.Find("GodModeIcon").gameObject;
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
        GameManager.OnAudioToggled += ToggleAudioIcon;
        GameManager.OnParticlesToggled += ToggleParticlesIcon;
        GameManager.OnGodModeToggled += ToggleGodModeIcon;
    }

    void OnDisable()
    {
        PlayerHealth.OnHealthChanged -= DrawHealth;
        LevelExit.OnLevelComplete -= LevelEndPopUp;
        GameManager.OnAudioToggled -= ToggleAudioIcon;
        GameManager.OnParticlesToggled -= ToggleParticlesIcon;
        GameManager.OnGodModeToggled -= ToggleGodModeIcon;
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

    void ToggleAudioIcon(bool status)
    {
        audioIcon.SetActive(!status);
    }

    void ToggleParticlesIcon(bool status)
    {
        particlesIcon.SetActive(!status);
    }

    void ToggleGodModeIcon(bool status)
    {
        godModeIcon.SetActive(!status);
    }
}