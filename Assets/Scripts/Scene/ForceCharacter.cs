using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCharacter : MonoBehaviour
{
    public GameManager.Character character;

    private void Start()
    {
        GameManager.Instance._currentCharacter = character;

        GameManager.Instance._player.GetComponent<PlayerController>().UpdateAnimator();
    }
}
