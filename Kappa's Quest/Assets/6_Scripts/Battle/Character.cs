using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Character Token")]
    //public int default_vitality = 1;
    //public int default_strenght = 1;
    //public int default_defense = 1;
    //public int default_intelligence = 1;
    //public int default_agility = 1;
    public CharacterToken CharToken;
    protected CharacterToken myToken;

    //Efeitos
    protected Animator animator;
    [SerializeField] AudioSource Audio;

    [SerializeField] protected CharacterStats charStats;
    public CharacterStats CharStats { get { return charStats; } set { charStats = value; } }

    [Header("Battle")]
    public Character currentTarget;

    protected float life;

    [SerializeField] protected bool defending = false;
    public bool Defending { get { return defending; } }

    /*[SerializeField] */protected Skill[] skills = new Skill[3];
    //public Skill[] Skills { get { return skills; } }

    private void Start()
    {
        myToken = Instantiate(CharToken);

        charStats = new CharacterStats(myToken.Vitality, myToken.Strenght, myToken.Resistance, myToken.Intelligence, myToken.Agility);

        myToken.Skills.CopyTo(skills, 0);

        life = charStats.MaxLife;

        animator = GetComponent<Animator>();
        Audio = gameObject.AddComponent<AudioSource>();

        if(animator == null)
        {
            Debug.LogWarning("animator no personagem é nulo");
        }
    }

    protected virtual void Attack()
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
        animator.SetTrigger("Atacou");
        if (Audio == null)
        {
            Audio = gameObject.AddComponent<AudioSource>();
        }
        Audio.clip = SFX.instancia.ataque;
        Audio.Play();
        //currentTarget.ReceiveDamage(charStats.GetAttackDamage());
        StartCoroutine(ExecuteAttack(charStats.AttackCast));
    }

    public virtual void UseSkill(int id)
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        animator.SetTrigger("UsouSkill");
        StartCoroutine(ExecuteSkill(id, skills[id].CastTime));
        //currentTarget.ReceiveDamage(skills[id].GetSkillDamage(charStats.Intelligence));
    }

    IEnumerator ExecuteAttack(float castTime)
    {
        yield return new WaitForSeconds(castTime);
        currentTarget.ReceiveDamage(charStats.GetAttackDamage());
    }

    IEnumerator ExecuteSkill(int id, float castTime)
    {
        yield return new WaitForSeconds(castTime);
        currentTarget.ReceiveDamage(skills[id].GetSkillDamage(charStats.Intelligence));
    }

    public virtual void ReceiveDamage(float damage)
    {
        if (Audio == null)
        {
            Audio = gameObject.AddComponent<AudioSource>();
        }
        Audio.clip = SFX.instancia.dano;
        Audio.Play();
        damage -= damage * charStats.GetDamageReduction();

        if (defending)
        {
            damage -= damage * 0.7f;
        }

        GameManager.instance.MainHud.PopUpDamage(damage, Camera.main.WorldToScreenPoint(this.transform.position));

        life = Mathf.Clamp(life - damage, 0, charStats.MaxLife);
        //Debug.Log(life);
    }

    public virtual void EnableDefense(bool v, bool receivedDamage = false)
    {
        defending = v;
    }
}
