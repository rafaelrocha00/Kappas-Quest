using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseableSkill : MonoBehaviour
{
    [SerializeField] Skill skill = null;
    [SerializeField] int id = 0;

    public Skill Skill { get { return skill; } set { skill = value; } }
    public int ID { get { return id; } }

    public Image skillIcon;

    public bool isPlayer;

    public void SetSkill(Skill s)
    {
        if (skillIcon == null) skillIcon = GetComponent<Image>();

        skill = s;

        skillIcon.sprite = s.SkillSp;
    }

    public void ShowDiscription()
    {
        //Se eu tiver escolhendo uma skill pra substituir, substitiui por essa
        if (GameManager.instance.MainHud.ChoosingSkill && isPlayer)
        {
            Skill aux = skill;
            SetSkill(GameManager.instance.MainHud.CurrentChooseableSkill.Skill);
            GameManager.instance.MainHud.CurrentChooseableSkill.SetSkill(aux);

            GameManager.instance.MainHud.CancelSkillChoosing();

            GameManager.instance.MainHud.DisableOtherChoices();
            return;
        }

        //Se não, mostra descrição
        GameManager.instance.MainHud.ShowSkillDiscription(this);
        //Se for chooseable, aparece o botão de confirmação. Se não, não aparece;
    }

    public void DisableButton()
    {
        GetComponent<Button>().interactable = false;
    }
}
