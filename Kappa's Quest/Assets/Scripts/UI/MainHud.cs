using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainHud : MonoBehaviour
{
    [Header("Battle")]
    public Transform actionsTab;
    public ActionIcon[] actions;

    public GameObject damagePopUpPref;
    public Transform damagePopUpParent;
    //public Text damagePopUp;

    public Transform playerBar;
    public Transform enemyBar;

    public Text countdownTxt;
    public GameObject screenBlock;

    [Header("Post Battle - Choosing Skill")]
    //public GameObject choosingSkillWin;
    //Telinha de descrição com: Nome, Descrição, Dano, Cooldown, Botão de confirmar;
    public SkillDiscription skillDiscription_win;
    //Botão de fundo para cancelar ao clicar fora;
    public GameObject exitButton;

    public GameObject postBattleScreen;
    public Text resultTxt;
    //Tela só de vitória;
    public GameObject victory_win;
    public GameObject defeat_end;
    //Estou atualmente escolhendo uma skill pra substituir?
    [HideInInspector] bool choosingSkill = false;
    [HideInInspector] ChooseableSkill currentChooseableSkill;
    public bool ChoosingSkill { get { return choosingSkill; } }
    public ChooseableSkill CurrentChooseableSkill { get { return currentChooseableSkill; } }
    //Qual ID da skill escolhida?
    //int currentSKillID = -1;
    //Array das skills do player;
    public ChooseableSkill[] playerSkills = new ChooseableSkill[3];
    //Array das skills do inimigo;
    public ChooseableSkill[] enemySkills = new ChooseableSkill[3];

    [Header("Post Battle - Improving Stats")]
    //public GameObject improvingStatsWin;
    public Text availablePointsTxt;
    [HideInInspector] int availablePoints;
    public int AvailablePoints { get { return availablePoints; } }
    public Transform statsTab;
    public StatModfier[] statsMods = new StatModfier[5];
    public GameObject confirmStatsButton;

    private void Start()
    {
        actions = actionsTab.GetComponentsInChildren<ActionIcon>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && choosingSkill) CancelSkillChoosing();
    }

    public void SetSkillsIcon(Skill[] skills)
    {
        for (int i = 2; i < 5; i++)
        {
            actions[i].SetSprite(skills[i - 2].SkillSp);
        }
    }

    public void Countdown(float t)
    {
        screenBlock.SetActive(true);
        StartCoroutine(CountdownToStart(t));
    }

    IEnumerator CountdownToStart(float t)
    {
        while (t >= 1)
        {
            t -= Time.deltaTime;
            countdownTxt.text = Mathf.Floor(t).ToString("0");
            yield return new WaitForEndOfFrame();
        }

        countdownTxt.text = "Fight!";

        yield return new WaitForSeconds(1.0f);

        screenBlock.SetActive(false);
    }

    #region Post Battle
    #region Choosing Skill
    public void ShowPostBattleScreen(bool win)
    {
        postBattleScreen.SetActive(true);

        if (win)
        {
            resultTxt.text = "Victory";
            ShowVictoryScreen();
        }
        else
        {
            resultTxt.text = "Defeat";
            defeat_end.SetActive(true);
        }
    }

    public void ShowVictoryScreen()
    {
        victory_win.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            playerSkills[i].SetSkill(GameManager.instance.Player.CharToken.Skills[i]);
        }
        for (int i = 0; i < 3; i++)
        {
            enemySkills[i].SetSkill(GameManager.instance.Player.currentTarget.CharToken.Skills[i]);
        }
    }

    public void ShowSkillDiscription(ChooseableSkill cs)
    {
        exitButton.SetActive(true);

        skillDiscription_win.gameObject.SetActive(true);

        currentChooseableSkill = cs;
        skillDiscription_win.transform.position = cs.isPlayer ? playerSkills[cs.ID].transform.position : enemySkills[cs.ID].transform.position;

        skillDiscription_win.SetSkill(cs.Skill);

        if (!cs.isPlayer) choosingSkill = true;
    }

    public void CancelSkillChoosing()
    {
        //Fechar descrições e Cancelar se estiver escolhendo;
        exitButton.SetActive(false);

        skillDiscription_win.gameObject.SetActive(false);

        choosingSkill = false;
    }

    public void DisableOtherChoices()
    {
        for (int i = 0; i < 3; i++)
        {
            playerSkills[i].DisableButton();
        }
        for (int i = 0; i < 3; i++)
        {
            enemySkills[i].DisableButton();
        }
    }

    public void ConfirmSkillChoices()
    {
        for (int i = 0; i < 3; i++)
        {
            GameManager.instance.Player.ReplaceSkill(i, playerSkills[i].Skill);
        }

        SetStatsImproving();
    }

    #endregion
    #region Improving Stats
    public void SetStatsImproving()
    {
        statsMods = statsTab.GetComponentsInChildren<StatModfier>(true);

        CharacterStats playerStats = GameManager.instance.Player.CharStats;
        statsMods[0].Quant = playerStats.Vitality;
        statsMods[1].Quant = playerStats.Strength;
        statsMods[2].Quant = playerStats.Resistance;
        statsMods[3].Quant = playerStats.Intelligence;
        statsMods[4].Quant = playerStats.Agility;
        for (int i = 0; i < 5; i++)
        {
            statsMods[i].UpdateQuant();
        }
        availablePoints = GameManager.instance.Player.Stat_Balance;

        availablePointsTxt.text = availablePoints.ToString();
    }

    public void UpdateAvailablePoints(int v)
    {
        availablePoints -= v;

        availablePointsTxt.text = availablePoints.ToString();

        if (availablePoints == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                statsMods[i].HidePlusButton();
            }

            confirmStatsButton.SetActive(true);
        }
    }

    public void ConfirmStats()
    {
        CharacterStats newPlayerStats = new CharacterStats(statsMods[0].Quant, statsMods[1].Quant, statsMods[2].Quant, statsMods[3].Quant, statsMods[4].Quant);

        GameManager.instance.Player.CharStats = newPlayerStats;

        GameManager.instance.Player.SaveToken();

        //Vai pro próximo combate
        C_Jogo.instancia.Vencer();
    }
    #endregion
    #endregion

    #region Battle

    public void EnableAllActions(bool v, int exc = -1)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].SetButtonActive(v);
        }

        if (exc > -1) actions[exc].SetButtonActive(true);
    }

    public void CooldownAllActions(float t, int exc = -1, bool replace = true)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            if (i != exc) actions[i].StartCooldown(t, replace);
        }
    }

    public void ActionCooldown(int id, float t)
    {
        actions[id].StartCooldown(t);
    }

    public void UpdatePlayerBar(float lifeRate)
    {
        playerBar.localScale = new Vector3(lifeRate, 1, 1);
    }
    public void UpdateEnemyBar(float lifeRate)
    {
        enemyBar.localScale = new Vector3(lifeRate, 1, 1);
    }

    public void PopUpDamage(float d, Vector3 screenPos)
    {
        //StopAllCoroutines();
        GameObject g = Instantiate(damagePopUpPref, damagePopUpParent, false);
        Text txt = g.GetComponent<Text>();

        //damagePopUp.gameObject.SetActive(true);

        //damagePopUp.transform.position = screenPos;
        g.transform.position = screenPos;

        //damagePopUp.text = d.ToString("0");
        txt.text = d.ToString("0");
        txt.color *= 1 + d / 100;

        StartCoroutine(DamagePopUpAnim(g));
    }

    IEnumerator DamagePopUpAnim(GameObject popUp)
    {
        float timer = 0.0f;
        float t = 1.0f;
        float accel = -100.0f;
        float vel = 150.0f;

        Text text = popUp.GetComponent<Text>();
        Color c0 = text.color;

        while (timer <= t)
        {
            timer += Time.deltaTime;

            vel += accel * Time.deltaTime;
            popUp.transform.position += Vector3.up * vel * Time.deltaTime;
            text.color = Color.Lerp(c0, new Color(c0.r, c0.g, c0.b, 0), timer / t);
            yield return new WaitForEndOfFrame();
        }

        //damagePopUp.gameObject.SetActive(false);
        Destroy(popUp);
    }
    #endregion
}
