using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource stageTheme = null;
    [SerializeField]
    private AudioSource titleTheme = null;
    [SerializeField]
    private AudioSource playerDamage = null;
    [SerializeField]
    private AudioSource machineDamage = null;
    [SerializeField]
    private AudioSource basicShoot = null;
    [SerializeField]
    private AudioSource autoShoot = null;
    [SerializeField]
    private AudioSource sprayShoot = null;
    [SerializeField]
    private AudioSource enemyDeath = null;

    private static AudioManager instance;

    void Awake()
    {
        instance = this;
    }

    string currScene;

    void Start()
    {
        currScene = SceneManager.GetActiveScene().name;
        PlayTheme();
    }

    void OnEnable()
    {
        PlayTheme(); 
    }

    void PlayTheme()
    {
        if (currScene == "Stage1")
        {
            PlayStageTheme(true);
        }
        else if (currScene == "MainMenu")
        {
            PlayTitleTheme();
        }
    }

    void PlayStageTheme(bool playing)
    {
        if (playing)
        {
            instance.stageTheme.Play();
        }
        else
        {
            instance.stageTheme.Stop();
        }
    }

    void PlayTitleTheme()
    {
        instance.titleTheme.Play();
    }

    public static void PlayPlayerDamage()
    {
        instance.playerDamage.Play();
    }

    public static void PlayMachineDamage()
    {
        instance.machineDamage.Play();
    }

    public static void PlayEnemyDeath()
    {
        instance.enemyDeath.Play();
    }

    public static void PlayShoot(string weapon)
    {
        if (weapon == "Basic Weapon")
        {
            PlayBasicShoot();
        }
        else if (weapon == "Automatic Basic Weapon")
        {
            PlayAutoShoot();
        }
        else if (weapon == "Spray Weapon")
        {
            PlaySprayShoot();
        }
    }

    static void PlayBasicShoot()
    {
        instance.basicShoot.Play();
    }

    static void PlayAutoShoot()
    {
        instance.autoShoot.Play();
    }

    static void PlaySprayShoot()
    {
        instance.sprayShoot.Play();
    }
}
