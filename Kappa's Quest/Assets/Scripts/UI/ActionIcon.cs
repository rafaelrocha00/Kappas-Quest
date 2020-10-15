using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionIcon : MonoBehaviour
{
    public Image cooldownI;
    Button button;

    public int actionID;
    //public Skill skill;
    Player player;

    float timer = 0.0f;
    float cdTime = 0.0f;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void SetSprite(Sprite s)
    {
        GetComponent<Image>().sprite = s;
    }

    public void SetButtonActive(bool v)
    {
        button.interactable = v;
    }

    public void Action()
    {
        if (player == null) player = GameManager.instance.Player;

        player.Action(actionID);
    }

    public void StartCooldown(float t, bool replace = true)
    {
        if (!replace && cooldownI.enabled)
        {
            if (cdTime - timer >= t) return;
        }

        StopAllCoroutines();
        StartCoroutine(Cooldown(t));
    }

    IEnumerator Cooldown(float t)
    {
        button.enabled = false;
        cooldownI.enabled = true;
        cdTime = t;
        timer = 0.0f;

        while (timer < t)
        {
            timer += Time.fixedDeltaTime;
            cooldownI.fillAmount = 1 - timer / t;
            yield return new WaitForFixedUpdate();
        }

        cooldownI.enabled = false;
        button.enabled = true;
    }
}
