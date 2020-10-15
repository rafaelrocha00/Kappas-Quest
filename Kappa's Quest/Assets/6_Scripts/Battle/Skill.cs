using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class Skill : ScriptableObject
{
    [Header("Description")]
    [SerializeField] string skill_name = "";
    [TextArea] public string skill_discription = "";

    [Header("Damage")]
    [SerializeField] float baseDamage = 100.0f;
    [SerializeField] float intelligenceMult = 1.0f;

    [Header("Cooldown")]
    [SerializeField] float cooldown = 10.0f;
    [SerializeField] float castTime = 0.5f;
    [SerializeField] float leftover = 0.5f;
    [HideInInspector] bool inCooldown = false;

    [Header("UI")]
    [SerializeField] Sprite skillSp = null;

    public string Name { get { return skill_name; } }
    //public string Description { get { return skill_description; } }

    public float BaseDamage { get { return baseDamage; } }
    public float IntelligenceMult { get { return intelligenceMult; } }

    public float Cooldown { get { return cooldown; } }
    public float CastTime { get { return castTime; } }
    public float Leftover { get { return leftover; } }
    public bool InCooldown { get { return inCooldown; } set { inCooldown = value; } }

    public Sprite SkillSp { get { return skillSp; } }

    public float GetSkillDamage(int intelligence)
    {
        return baseDamage + (intelligence * 10.0f * intelligenceMult);
    }

    public string GetFullDescription()
    {
        string desc = skill_discription;

        desc = desc.Replace("#", baseDamage.ToString());
        desc = desc.Replace("$", intelligenceMult.ToString());
        desc = desc.Replace("&", cooldown.ToString());

        return desc;
    }

    private void OnEnable()
    {
        inCooldown = false;
    }
}
