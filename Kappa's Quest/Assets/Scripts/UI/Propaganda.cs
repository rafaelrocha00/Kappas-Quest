using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Propaganda : MonoBehaviour
{
    public Button butao;
    public float tempo = 3f;
    void Start()
    {
        butao.interactable = false;
        StartCoroutine(esperar());
    }

    IEnumerator esperar()
    {
        yield return new WaitForSeconds(3f);
        butao.interactable = true;

    }
}
