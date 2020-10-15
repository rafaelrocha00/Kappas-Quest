using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Fase : MonoBehaviour
{
    public bool ativo = false;
    public bool Estaselecionado = false;
    int camada;
    Fase conexao;
    LineRenderer line;
    public Action<Fase> selecionado;
    MeshRenderer render;
    public GameObject tipoDeInimigo;
    public int vitalidade = 2;
    public int forca = 2;
    public int resistencia = 2;
    public int inteligencia = 2;
    public int agilidade = 2;
    public CharacterStats status;
    public List<Skill> skills;
    public Color corAtivado;
    public string nome;
    public int cenario;
    public GameObject faseVencida;
    Gui_SelecaoDeFases gui;

    private void Start()
    {
        render = GetComponent<MeshRenderer>();
        line = GetComponent<LineRenderer>();
        SetarInimigo();
    }

    public void Ativar()
    {
        ativo = RandomizadorDeFases.rf.EFolha(this);
    }

    public void Vencer()
    {
        ativo = false;
        Estaselecionado = false;
        GameObject fv = Instantiate(faseVencida, transform);
        if(conexao != null)
        {
            conexao.ativo = true;
            RandomizadorDeFases.rf.Selecionar(conexao);
        }
        else
        {
            C_Jogo.instancia.VencerJogo();
        }
        
    }

    private void Update()
    {
        if(conexao != null)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, conexao.transform.position);
        }
        UpdateVisualizacao();
    }

    public void UpdateVisualizacao()
    {
        if (ativo)
        {
            render.material.SetColor("_Color", Color.white);
        }
        else
        {
            render.material.SetColor("_Color", Color.gray);
        }

        if (Estaselecionado)
        {
            render.material.SetColor("_Color", corAtivado);
        }
    }

    public void Selecionar()
    {
        if (ativo)
        {
            SFX.instancia.MostrarSomDeBotao();
            if(gui == null)
            {
                gui = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Gui_SelecaoDeFases>();
            }
            gui.UpdateHUD(this);
            RandomizadorDeFases.rf.Selecionar(this);
            Estaselecionado = true;
        }
    }

    public void Deselecionar()
    {
        Estaselecionado = false;
    }

    public void setCamada(int camada)
    {
        this.camada = camada;
    }
    
    public int getCamada()
    {
        return camada;
    }

    public bool SaoConjuntos(Fase fase)
    {
        if (this == fase) return true;
        if (conexao == fase) return true;
        if (fase.conexao == this) return true;
        return false;
    }

    public void setConexao(Fase conexao)
    {
        this.conexao = conexao;
    }

    public Fase getConexao()
    {
        return conexao;
    }

    public void SetarInimigo()
    {
        float pontosTotais = (Mathf.Abs(camada)+ 1) * 10;
        pontosTotais -= 10;
        for (int i = 0; i < pontosTotais; i++)
        {
            int escolha = Random.Range(1, 6);
            switch(escolha){
                case 1:
                    vitalidade++;
                    break;
                case 2:
                    forca++;
                    break;
                case 3:
                    resistencia++;
                    break;
                case 4:
                    inteligencia++;
                    break;
                case 5:
                    agilidade++;
                    break;
            }
        }

        status = new CharacterStats(vitalidade, forca, resistencia, inteligencia, agilidade);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(conexao != null)
        Gizmos.DrawLine(transform.position, conexao.transform.position);
    }
}
