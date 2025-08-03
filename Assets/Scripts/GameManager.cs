using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<WeaponBehavior> weapons;
    // Start is called before the first frame update
    void Awake()
    {
        weapons = Resources.LoadAll<WeaponBehavior>("ScriptableObjects/Weapons").ToList();
    }

    public List<WeaponBehavior> GetWeaponsList()
    {
        return weapons;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
