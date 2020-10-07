using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] CharacterStats charStats;

    public CharacterStats CharStats { get { return CharStats; } }

    private void Start()
    {
        charStats = new CharacterStats();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) IncreaseStrength();
    }

    public void IncreaseStrength()
    {
        charStats.IncreaseStat(1);
        Debug.Log("Dano do ataque: " + charStats.GetAttackDamage());
    }
}
