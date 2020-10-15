using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] Player player;
    public Player Player { get { if (player != null) return player; else return player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); } }

    CharacterToken playerToken;

    [HideInInspector] MainHud mainHud;
    public MainHud MainHud { get { if (mainHud != null) return mainHud; else return mainHud = GameObject.Find("/MainHud").GetComponent<MainHud>(); } }

    [HideInInspector] BattleController battleController;

    public BattleController BattleController { get { return battleController; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        battleController = GetComponent<BattleController>();

        //Setar skill do player;
        if (GameManager.instance.playerToken != null) GameManager.instance.player.CharToken = GameManager.instance.playerToken;
    }

    public void SavePlayerToken(CharacterToken ct)
    {
        playerToken = ct;
    }
}
