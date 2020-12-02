using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [HideInInspector] int stat_balance;
    public int Stat_Balance { get { return stat_balance; } set { stat_balance = value; } }

    private void Start()
    {
        if (GameManager.instance.playerToken == null) myToken = Instantiate(CharToken);
        else myToken = GameManager.instance.playerToken;

        myToken.Skills.CopyTo(skills, 0);

        charStats = new CharacterStats(myToken.Vitality, myToken.Strenght, myToken.Resistance, myToken.Intelligence, myToken.Agility);

        life = charStats.MaxLife;

        GameManager.instance.MainHud.SetSkillsIcon(skills);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) charStats.IncreaseStat(3);

        if (Input.GetKeyDown(KeyCode.A)) Attack();
    }

    public void Action(int id)
    {
        if (!GameManager.instance.BattleController.InBattle) return;

        if (id > 1) UseSkill(id);
        else if (id == 0) Attack();
        else if (id == 1) EnableDefense(!defending);
    }

    protected override void Attack()
    {
        base.Attack();
        GameManager.instance.MainHud.ActionCooldown(0, charStats.AttackCooldown + charStats.AttackCast + charStats.AttackLeftover);
        GameManager.instance.MainHud.CooldownAllActions(charStats.AttackCast + charStats.AttackLeftover, 0, false);
    }

    public override void UseSkill(int id)
    {
        base.UseSkill(id - 2);
        GameManager.instance.MainHud.ActionCooldown(id, skills[id - 2].Cooldown);
        GameManager.instance.MainHud.CooldownAllActions(skills[id - 2].CastTime + skills[id - 2].Leftover, id, false);
    }

    public override void ReceiveDamage(float damage)
    {
        base.ReceiveDamage(damage);
        if (defending)
        {
            GameManager.instance.MainHud.ActionCooldown(1, charStats.DefenseCooldown);
            EnableDefense(!defending, true);
        }
        GameManager.instance.MainHud.UpdatePlayerBar(charStats.GetLifeRate(life));
        if (life == 0)
        {
            animator.SetTrigger("Morreu");
            GameManager.instance.BattleController.Defeat();
            GameManager.instance.ResetToken();
        }
    }

    public override void EnableDefense(bool v, bool receivedDamage = false)
    {
        base.EnableDefense(v);

        GameManager.instance.MainHud.EnableAllActions(!v, 1);

        if (receivedDamage) return;

        GameManager.instance.MainHud.ActionCooldown(1, 0.5f);
    }

    public void ReplaceSkill(int id, Skill newSkill)
    {
        Debug.Log("Replacing skill with " + newSkill.Name);
        myToken.SetNewSkill(id, newSkill); 
    }

    public void SaveToken()
    {
        myToken.SetStats(charStats);

        GameManager.instance.SavePlayerToken(myToken);
    }
}
