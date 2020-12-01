using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Token", fileName = "New Character Token")]
public class CharacterToken : ScriptableObject
{
    [Header("Stats")]
    [SerializeField] int vitality = 1;
    [SerializeField] int strenght = 1;
    [SerializeField] int resistance = 1;
    [SerializeField] int intelligence = 1;
    [SerializeField] int agility = 1;

    [Header("Skills")]
    [SerializeField] Skill[] skills = new Skill[3];

    public int Vitality { get { return vitality; } }
    public int Strenght { get { return strenght; } }
    public int Resistance { get { return resistance; } }
    public int Intelligence { get { return intelligence; } }
    public int Agility { get { return agility; } }

    public Skill[] Skills { get { return skills; } }

    public void SetStats(CharacterStats cs)
    {
        vitality = cs.Vitality;
        strenght = cs.Strength;
        resistance = cs.Resistance;
        intelligence = cs.Intelligence;
        agility = cs.Agility;
    }

    public void AddStats(CharacterStats cs)
    {
        vitality += cs.Vitality;
        strenght += cs.Strength;
        resistance += cs.Resistance;
        intelligence += cs.Intelligence;
        agility += cs.Agility;
    }

    public void TakeStats(CharacterStats cs)
    {
        vitality -= cs.Vitality;
        strenght -= cs.Strength;
        resistance -= cs.Resistance;
        intelligence -= cs.Intelligence;
        agility -= cs.Agility;
    }

    public void SetNewSkill(int id, Skill sk)
    {
        skills[id] = sk;
    }
}
