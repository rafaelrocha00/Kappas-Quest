using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
    [SerializeField] float maxLife = 1000.0f;
    [SerializeField] float attackCooldown = 1.0f;
    [SerializeField] float defenseCooldown = 10.0f;
    [SerializeField] float attackCast = 0.5f;
    [SerializeField] float attackLeftover = 0.5f;

    [SerializeField] int vitality = 1;
    [SerializeField] int strength = 1;
    [SerializeField] int resistance = 1;
    [SerializeField] int intelligence = 1;
    [SerializeField] int agility = 1;

    public float MaxLife { get { return maxLife; } }
    public float AttackCooldown { get { return attackCooldown; } }
    public float DefenseCooldown { get { return defenseCooldown; } }
    public float AttackCast { get { return attackCast; } }
    public float AttackLeftover { get { return attackLeftover; } }

    public int Vitality { get { return vitality; } }
    public int Strength { get { return strength; } }
    public int Resistance { get { return resistance; } }
    public int Intelligence { get { return intelligence; } }
    public int Agility { get { return agility; } }

    public const float normalAttackDamage = 100.0f;

    public CharacterStats()
    {
        UpdateStats();
    }

    public CharacterStats(int v, int s, int r, int i, int a)
    {
        vitality = v;
        strength = s;
        resistance = r;
        intelligence = i;
        agility = a;

        UpdateStats();
    }

    public float GetLifeRate(float life)
    {
        return life / maxLife;
    }

    public float GetAttackDamage()
    {
        return normalAttackDamage * (1 + strength / 8.0f);
    }

    public float GetDamageReduction()
    {
        return Mathf.Log(resistance + 1, 2) * 0.058f;
    }

    public void IncreaseStat(int i)
    {
        switch (i)
        {
            case 0:
                vitality++;
                break;
            case 1:
                strength++;
                break;
            case 2:
                resistance++;
                break;
            case 3:
                intelligence++;
                break;
            case 4:
                agility++;
                break;
            default:
                break;
        }

        UpdateStats();
    }

    void UpdateStats()
    {
        maxLife = (1 + vitality / 10.0f) * 1000.0f;
        attackCooldown = 1.0f - Mathf.Log(agility, 2) * 0.083f;
        defenseCooldown = 10.0f - Mathf.Log(resistance, 2) * 0.029f;
    }
}