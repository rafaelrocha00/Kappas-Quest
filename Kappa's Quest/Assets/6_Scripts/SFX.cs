using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip botaoSelecionado;
    [SerializeField] AudioClip botaoVoltar;
    public AudioClip dano;
    public AudioClip ataque;

    public static SFX instancia;
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

    public void MostrarSomDeBotao()
    {
        source.clip = botaoSelecionado;
        source.Play();
    }

    public void MostrarSomDeVoltar()
    {
        source.clip = botaoVoltar;
        source.Play();
    }
    public void MostrarSom(AudioClip audio)
    {
        source.clip = audio;
        source.Play();
    }

    
}
