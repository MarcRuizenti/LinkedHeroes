using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    PlayerController _playerController; //si no pones nada se pone private directamente
    private Color _defaultColor;



    //private void Start()
    //{
    //    _spriteRenderer = GetComponent<SpriteRenderer>();
    //    _playerController = GetComponent<PlayerController>();
    //    _defaultColor = _spriteRenderer.color;
    //}

    //public void ChangeCharacter()
    //{
    //    _playerController.ChangeCharacter();
    //    if(_spriteRenderer.color == _defaultColor)
    //    {
    //        _spriteRenderer.color = Color.cyan;
    //    }
    //    else
    //    {
    //        _spriteRenderer.color = _defaultColor;
    //    }
    //}
}
