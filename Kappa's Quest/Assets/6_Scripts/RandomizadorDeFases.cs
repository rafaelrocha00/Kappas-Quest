using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class RandomizadorDeFases : MonoBehaviour
{
    [SerializeField] GameObject paiFases;
    [SerializeField] GameObject faseVencida;
    Fase faseSelecionada;
    [SerializeField]CharacterToken inimigoAtual;
    public static RandomizadorDeFases rf;
    public GameObject FasePrefab;
    public int numeroDeCamadas;
    public float offsetIndividual;
    public float RaioMaximo;
    public int NumeroTotalDeFases;
    public float distanciaMinima;
    public List<Fase> Fases;
    public Action FasesGeradas;
    [SerializeField] Color corAtivo;
    [SerializeField] List<string> nomes = new List<string>();
    [SerializeField] List<GameObject> inimigos = new List<GameObject>();
    [SerializeField] List<Skill> skillsExistentes = new List<Skill>();
    [SerializeField] int cenarioInicial;
    [SerializeField] int QuantidadeDeCenarios;

    private void Awake()
    {
        if(rf != null && rf != this)
        {
            Destroy(this);
        }
        else
        {
            rf = this;
        }
    }

    public void Batalhar()
    {
        SFX.instancia.MostrarSomDeBotao();
        inimigoAtual.SetStats(faseSelecionada.status);
        inimigoAtual.SetNewSkill(0, faseSelecionada.skills[0]);
        inimigoAtual.SetNewSkill(1, faseSelecionada.skills[1]);
        inimigoAtual.SetNewSkill(2, faseSelecionada.skills[2]);
        C_Jogo.instancia.MudarDeFase(faseSelecionada.cenario, faseSelecionada, inimigoAtual);
    }

    private void Start()
    {
        if(C_Jogo.instancia.camadaAtual == -1)
        {
            StartCoroutine(GerarFases());
            C_Jogo.instancia.camadaAtual = 1;
            C_Jogo.instancia.SetarSaveFases(1, paiFases);
        }
    }

 

    public IEnumerator GerarFases()
    {


        // Mestre
        Fase master = Instantiate(FasePrefab, Vector3.zero, Quaternion.identity).GetComponent<Fase>();
        master.gameObject.transform.parent = paiFases.transform;
        master.corAtivado = corAtivo;
        master.nome = nomes[Random.Range(0, nomes.Count)];
        master.cenario = Random.Range(cenarioInicial, cenarioInicial + QuantidadeDeCenarios);
        master.faseVencida = faseVencida;
        master.tipoDeInimigo = inimigos[Random.Range(0, inimigos.Count)];

        master.skills.Add(skillsExistentes[Random.Range(0, skillsExistentes.Count)]);
        master.skills.Add(skillsExistentes[Random.Range(0, skillsExistentes.Count)]);
        master.skills.Add(skillsExistentes[Random.Range(0, skillsExistentes.Count)]);
        master.setCamada(numeroDeCamadas);

        int numeroAtual = 1;
        int densidade = NumeroTotalDeFases / numeroDeCamadas;
        //Camadas
        for (int i = 0; i < numeroDeCamadas; i++)
        {
            for (int f = 0; f < densidade; f++)
            {
                Vector3 pos = PolarParaVetor(Random.Range(0f, 360f), (RaioMaximo / numeroAtual) + Random.Range(0f, offsetIndividual));
                while (!ChecarDistancia(pos, distanciaMinima))
                {
                    pos = PolarParaVetor(Random.Range(0f, 360f), (RaioMaximo / numeroAtual) + Random.Range(0f, offsetIndividual));
                    yield return new WaitForEndOfFrame();
                }
                Fase fase = Instantiate(FasePrefab, pos, Quaternion.identity).GetComponent<Fase>();
                fase.setCamada(numeroAtual);
                fase.corAtivado = corAtivo;
                fase.nome = nomes[Random.Range(0, nomes.Count)];
                fase.cenario = Random.Range(cenarioInicial, cenarioInicial + QuantidadeDeCenarios);
                fase.gameObject.transform.parent = paiFases.transform;
                fase.tipoDeInimigo = inimigos[Random.Range(0, inimigos.Count)];

                fase.skills.Add(skillsExistentes[Random.Range(0, skillsExistentes.Count)]);
                fase.skills.Add(skillsExistentes[Random.Range(0, skillsExistentes.Count)]);
                fase.skills.Add(skillsExistentes[Random.Range(0, skillsExistentes.Count)]);
                fase.faseVencida = faseVencida;
                Fases.Add(fase);
            }
            numeroAtual++;
        }
  
        Fases.Add(master);
        GerarConexoes();
        AtivarFolhas();
        yield break;
    }

    public bool EFolha(Fase fase)
    {
       return !Fases.Exists(x => x.getConexao() == fase);

    }

    public void GerarConexoes()
    {
        for (int i = 0; i < Fases.Count -1; i++)
        {
            if(Fases[i].getCamada() == numeroDeCamadas)
            {
                Fases[i].setConexao(Fases[Fases.Count - 1]);
            }
            else
            {
                Fases[i].setConexao(PegarFaseMaisProxima(Fases[i]));
            }
        }

        FasesGeradas?.Invoke();
    }

    public void AtivarFolhas()
    {
        for (int i = 0; i < Fases.Count; i++)
        {
            Fases[i].ativo = EFolha(Fases[i]);
        }
    }

    Vector3 PolarParaVetor(float delta, float raio)
    {
        float radiano = delta * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radiano) * raio, 0, Mathf.Cos(radiano) * raio);
    }

    public bool ChecarDistancia(Vector3 posicao, float distanciaMinima)
    {
        for (int i = 0; i < Fases.Count; i++)
        {
            if (Vector3.Distance(posicao, Fases[i].transform.position) < distanciaMinima)
            {
                return false;
            }
        }
        return true;
    }

    public Fase PegarFaseMaisProxima(Vector3 posicao)
    {
        float distanciaMin = 999;
        int idAtual = -1;

        for (int i = 0; i < Fases.Count; i++)
        {
            float distanciaAtual = Vector3.Distance(posicao, Fases[i].transform.position);
            if (distanciaAtual < distanciaMin)
            {
                idAtual = i;
                distanciaMin = Vector3.Distance(posicao, Fases[i].transform.position);
            }
        }

        return Fases[idAtual];
    }

    public Fase PegarFaseMaisProxima(Fase fase)
    {
        float distanciaMin = 999;
        int idAtual = -1;

        for (int i = 0; i < Fases.Count; i++)
        {
            float distanciaAtual = Vector3.Distance(fase.transform.position, Fases[i].transform.position);
            if (distanciaAtual < distanciaMin && !fase.SaoConjuntos(Fases[i]))
            {
                idAtual = i;
                distanciaMin = Vector3.Distance(fase.transform.position, Fases[i].transform.position);
            }
  
        }

        return Fases[idAtual];
    }

    public void SetarActionEmCasoDeInteracao(Action<Fase> action)
    {
        for (int i = 0; i < Fases.Count; i++)
        {
            Fases[i].selecionado += action;
        }
    }

    public void Selecionar(Fase fase)
    {
        if(faseSelecionada != null)
            faseSelecionada.Deselecionar();
        faseSelecionada = fase;
    }

    

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.zero, RaioMaximo);
    }
}
