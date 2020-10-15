using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class C_Jogo : MonoBehaviour
{
    public static C_Jogo instancia;
    public int camadaAtual = -1;
    GameObject objetoPai;
    [SerializeField] int SeletorDeFases;
    [SerializeField] int CenaFinal;
    public Fase faseAtual;

    public void SetarSaveFases(int camadaAtual, GameObject objetoPai)
    {
        this.objetoPai = objetoPai;
        this.camadaAtual = camadaAtual;
        DontDestroyOnLoad(objetoPai);
    }

    public void MudarDeFase(int fase, Fase faseAtual, CharacterToken inimigoStatus)
    {
        this.faseAtual = faseAtual;
        objetoPai.SetActive(false);
        StartCoroutine(MudarDeFaseYield(fase, faseAtual, inimigoStatus));
    }

    IEnumerator MudarDeFaseYield(int fase, Fase faseAtual, CharacterToken inimigoStatus)
    {
        SceneManager.LoadScene(fase);
        yield return new WaitForSeconds(0.1f);
        SetarInimigo Setar = GameObject.FindGameObjectWithTag("Setagem").GetComponent<SetarInimigo>();
        if (Setar == null)
        {
            Debug.Log("Setagem é nulla");
        }
        GameObject inimigo = Instantiate(faseAtual.tipoDeInimigo, Setar.transform);
        Character inimigoCharacter = inimigo.AddComponent<Enemy>();
        inimigoCharacter.CharToken = inimigoStatus;
        inimigoCharacter.currentTarget = Setar.personagemPrincipal;
        Setar.personagemPrincipal.currentTarget = inimigoCharacter;
        BattleController bc = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
        bc.StartCountdown();
    }

    public void Vencer()
    {
        camadaAtual++;
        SceneManager.LoadScene(SeletorDeFases);
        objetoPai.SetActive(true);
        faseAtual.Vencer();
    }

    public void VencerJogo()
    {
        Destroy(objetoPai);
        camadaAtual = -1;
        SceneManager.LoadScene(CenaFinal);
    }

    public void Perder()
    {
        Destroy(objetoPai);
        camadaAtual = -1;
        SceneManager.LoadScene(0);
    }

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(this);
        }
        else
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
