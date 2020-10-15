using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [Header("Enemy")]
    bool canAttack = true;
    bool canDefend = true;

    bool canAct = true;

    public float maxTimeDefending = 3.0f;
    float timeDefending = 0.0f;

    int willDefend = 0;

    private void FixedUpdate()
    {
        if (!GameManager.instance.BattleController.InBattle) return;

        ChooseAction();
    }

    void ChooseAction()
    {
        if (defending)
        {
            timeDefending += Time.fixedDeltaTime;

            if (timeDefending >= maxTimeDefending) { EnableDefense(false); Debug.Log("Cancelando defesa pq ficou mto tempo defendendo"); }
        }

        if (!canAct) return;

        bool willUseSkill = false;
        int skillToUse = Random.Range(0, skills.Length);

        for (int i = 0; i < 3; i++)
        {
            if (skills[skillToUse].InCooldown)
            {
                skillToUse++;
                if (skillToUse > 2) skillToUse = 0;
            }
            else
            {
                willUseSkill = true;
                break;
            }
        }

        if (willUseSkill)
        {
            UseSkill(skillToUse);
            StartCoroutine(PutSkillInCooldown(skillToUse));

            Debug.Log("Usando skill: " + skillToUse);
        }
        else if (canAttack && !defending)
        {
            Attack();
            willDefend = Random.Range(0, 2);
            StartCoroutine(PutAttackInCooldown());

            Debug.Log("Atacando");
        }
        else if (canDefend && !defending && willDefend == 1)
        {
            EnableDefense(true);

            Debug.Log("Defendendo");
        }
    }

    IEnumerator PutSkillInCooldown(int skillId)
    {
        skills[skillId].InCooldown = true;

        StartCoroutine(CanActCooldown(skills[skillId].CastTime + skills[skillId].Leftover));
        yield return new WaitForSeconds(skills[skillId].CastTime + skills[skillId].Leftover + skills[skillId].Cooldown);

        skills[skillId].InCooldown = false;
    }

    IEnumerator PutAttackInCooldown()
    {
        canAttack = false;

        StartCoroutine(CanActCooldown(charStats.AttackCast + charStats.AttackLeftover));
        yield return new WaitForSeconds(charStats.AttackCast + charStats.AttackLeftover + charStats.AttackCooldown);

        canAttack = true;
    }

    IEnumerator PutDefenseInCooldown()
    {
        canDefend = false;
        yield return new WaitForSeconds(charStats.DefenseCooldown);
        canDefend = true;
    }

    IEnumerator CanActCooldown(float timeToAct)
    {
        canAct = false;

        yield return new WaitForSeconds(timeToAct);

        canAct = true;
    }

    public override void ReceiveDamage(float damage)
    {
        base.ReceiveDamage(damage);
        GameManager.instance.MainHud.UpdateEnemyBar(charStats.GetLifeRate(life));

        if (defending)
        {
            canDefend = false;
            StartCoroutine(PutDefenseInCooldown());
            EnableDefense(false);
        }

        if (life == 0)
        {
            animator.SetTrigger("Morreu");
            GameManager.instance.BattleController.Victory();
        }
    }

    public override void EnableDefense(bool v, bool receivedDamage = false)
    {
        base.EnableDefense(v, receivedDamage);
        if (!v) timeDefending = 0.0f;
    }
}
