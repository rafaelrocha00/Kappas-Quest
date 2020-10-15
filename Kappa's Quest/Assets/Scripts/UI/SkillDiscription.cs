using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDiscription : MonoBehaviour
{
    public Text skill_name;
    public Text skill_discription;

    public void SetSkill(Skill skill)
    {
        skill_name.text = skill.Name;
        skill_discription.text = skill.GetFullDescription();
    }
}
