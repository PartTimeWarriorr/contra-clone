using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public static event Action OnLevelComplete;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnLevelComplete?.Invoke();
            other.gameObject.GetComponent<PlayerController>().enabled = false;
            other.gameObject.GetComponent<PlayerHealth>().enabled = false;
        }
    }
}
