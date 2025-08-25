using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // private static GameManager _instance;

    // public static GameManager Instance()
    // {
    //     return _instance;
    // }

    // void Awake()
    // {
    //     if (_instance != null && _instance != this)
    //     {
    //         Destroy(gameObject);
    //     }
    //     else
    //     {
    //         weapons = Resources.LoadAll<WeaponBehavior>("ScriptableObjects/Weapons").ToList();
    //         _instance = this;
    //     }
    // }
    Scene currScene;
    public GameObject audioManager;
    public GameObject particleManager;

    public static event Action<bool> OnAudioToggled;
    public static event Action<bool> OnParticlesToggled;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleAudio();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleParticles();
        }
    }

    void ToggleAudio()
    {
        audioManager.SetActive(!audioManager.activeSelf);
        OnAudioToggled?.Invoke(audioManager.activeSelf);
    }

    void ToggleParticles()
    {
        particleManager.SetActive(!particleManager.activeSelf);
        OnParticlesToggled?.Invoke(particleManager.activeSelf);
    }

    void Awake()
    {
        currScene = SceneManager.GetActiveScene();
    }

    void Start()
    {
    }

    void OnEnable()
    {
        PlayerHealth.OnDied += RestartStage;
    }

    void OnDisable()
    {
        PlayerHealth.OnDied -= RestartStage;
    }

    private void RestartStage()
    {
        Debug.Log("Restarted");
        SceneManager.LoadScene("Scenes/Stage1");
    }

    public void LoadStage()
    {
        SceneManager.LoadScene("Scenes/Stage1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
