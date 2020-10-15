using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogoInicial : MonoBehaviour
{
    [SerializeField] List<string> mensagens = new List<string>();
    [SerializeField] TextMeshProUGUI texto;
    string mensagemAtual;
    [SerializeField]float velocidadeDeEscrita;
    bool DialogoFoiEscrito = false;
    int indexAtual;
    [SerializeField] int newScene;

    IEnumerator DesenharMensagem(TextMeshProUGUI texto)
    {
        foreach (char letra in mensagemAtual.ToCharArray())
        {
            texto.text += letra;
            yield return new WaitForSeconds(velocidadeDeEscrita * Time.deltaTime);
        }
        DialogoFoiEscrito = true;
        yield break;
    }

    private void Start()
    {
        texto.text = "";
        indexAtual = 0;
        mensagemAtual = mensagens[indexAtual];
        DialogoFoiEscrito = false;
        StartCoroutine(DesenharMensagem(texto));
    }

    void Avancar()
    {
        if (DialogoFoiEscrito)
        {
            indexAtual++;
            if (indexAtual < mensagens.Count)
            {
                texto.text = "";
                Debug.Log("Mostrando novo texto");
                StopAllCoroutines();
                DialogoFoiEscrito = false;
                mensagemAtual = mensagens[indexAtual];
                StartCoroutine(DesenharMensagem(texto));
            }
            else
            {
                Sair();
            }
        }
        else
        {
            StopAllCoroutines();
            texto.text = mensagemAtual;
            DialogoFoiEscrito = true;
        }
    }

    void Sair()
    {
        SceneManager.LoadScene(newScene);
    }


    void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Avancar();
            }
        }
        else
        {
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Avancar();
                }
            }
        }
    }
}
