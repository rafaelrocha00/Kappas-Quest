using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MudarFase : MonoBehaviour
{
    [SerializeField] int numeroDafase;
    Button butao;
    private void Start()
    {
        butao = GetComponent<Button>();
        butao.onClick.AddListener(Mudar);
    }
    public void Mudar()
    {
        SFX.instancia.MostrarSomDeBotao();
        SceneManager.LoadScene(numeroDafase);
    }
}
