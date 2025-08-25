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

    void Awake()
    {
        currScene = SceneManager.GetActiveScene();
    }

    void Start()
    {
        if (currScene.name == "Stage1")
        {
            AudioManager.PlayStageTheme(true);
        }
        else if (currScene.name == "MainMenu")
        {
            AudioManager.PlayTitleTheme();
        }
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
