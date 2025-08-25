using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private static AudioManager instance;

    void Awake()
    {
        instance = this;
    }

    public static void PlayStageTheme(bool playing)
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

    public static void PlayTitleTheme()
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
