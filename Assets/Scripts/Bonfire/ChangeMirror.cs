using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMirror : MonoBehaviour
{
    public Sprite krokur, aike;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    private void Update()
    {
        GameManager.Character personajeActual = gameManager._currentCharacter;
        if  (personajeActual == GameManager.Character.AIKE)
        {
            GetComponent<SpriteRenderer>().sprite = krokur;
        }
        if (personajeActual == GameManager.Character.KROKUR)
        {
            GetComponent<SpriteRenderer>().sprite = aike;
        }
    }
    
}
