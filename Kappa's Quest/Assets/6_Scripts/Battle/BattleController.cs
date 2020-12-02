using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{

    [SerializeField] float countDown = 4;

    [SerializeField] int enemyToSpawn = 0;

    [HideInInspector] bool inBattle = false;

    public int EnemyToSpawn { set { value = enemyToSpawn; } }

    public bool InBattle { get { return inBattle; } }

    public float CountDown { get { return countDown; } }


    public void StartCountdown()
    {
        GameManager.instance.MainHud.Countdown(countDown);

        Invoke("StartBattle", countDown);
    }

    void StartBattle()
    {
        inBattle = true;
    }

    void EndBattle(bool win)
    {
        inBattle = false;

        StartCoroutine(DelayedEnd(win));
    }

    public void Victory()
    {
        //Player Continua na sequencia de batalha
        if (inBattle) {
            EndBattle(true);
            GameManager.instance.Player.Stat_Balance = 3;
        }
        
    }

    public void Defeat()
    {
        //Tela de game over e player volta pro início
        if(InBattle)
        EndBattle(false);
    }

    IEnumerator DelayedEnd(bool win)
    {
        Time.timeScale = 0.1f;

        yield return new WaitForSecondsRealtime(5.0f);

        Time.timeScale = 1.0f;

        GameManager.instance.MainHud.ShowPostBattleScreen(win);
    }
}
