using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
    [SerializeField] float maxLife = 100.0f;
    [SerializeField] int endurance = 0;
    [SerializeField] int strength = 0;
    [SerializeField] int defense = 0;
    [SerializeField] int intelligence = 0;

    public float MaxLife { get { return maxLife; } }
    public int Strength { get { return strength; } }
    public int Defense { get { return defense; } }
    public int Intelligence { get { return intelligence; } }

    public const float normalAttackDamage = 10.0f;

    public CharacterStats()
    {
        maxLife = (1 + endurance / 10.0f) * 100.0f;
    }

    public float GetLifeRate(float life)
    {
        return life / maxLife;
    }

    public float GetAttackDamage()
    {
        return normalAttackDamage * (1 + strength / 10.0f);
    }

    //public float GetSkillDamage()
    //{

    //}

    public void IncreaseStat(int i)
    {
        switch (i)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }
}
