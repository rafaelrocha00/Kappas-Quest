using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeletorDeFases : MonoBehaviour
{
    private void Update()
    {
        HandleRaycast();
    }

    void HandleRaycast()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Raycast(Input.mousePosition);
            }
        }
        else
        {
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Raycast(Input.GetTouch(0).position);
                }
            }
        }
    }

    void Raycast(Vector3 position)
    {
      position = Camera.main.ScreenToWorldPoint(position);
        RaycastHit hit;
        if (Physics.Raycast(position, Vector3.down, out hit, 40f))
        {
            hit.collider.gameObject.GetComponent<Fase>().Selecionar();
            Debug.Log("Hit");
        }
    }


}
