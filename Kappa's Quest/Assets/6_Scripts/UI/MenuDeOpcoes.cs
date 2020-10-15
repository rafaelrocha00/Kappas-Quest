using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuDeOpcoes : MonoBehaviour
{
    [SerializeField]Slider musica;
    [SerializeField]Slider sfx;
    [SerializeField]AudioMixer Mixer;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;

    private void Start()
    {
        sfx.onValueChanged.AddListener(MudarSfx);
       
    }

    void AtivarMusica()
    {
        if (!source.isPlaying)
        {
            source.clip = clip;
            source.Play();
        }
    }

    private void Update()
    {
        MudarMusica();
    }

    void MudarMusica()
    {
        Mixer.SetFloat("volumeMusica", Mathf.Log10(musica.value) * 20);
    }

    void MudarSfx(float valor)
    {
        Mixer.SetFloat("volumeSfx", Mathf.Log10(sfx.value) * 20);
    }
}
