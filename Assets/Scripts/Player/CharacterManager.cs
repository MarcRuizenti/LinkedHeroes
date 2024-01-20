using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    //Para cambiar el personaje mas adelante
    private enum Character
    {
        AIKE,
        KROKUR
    }
    private Character _currentCharacter;

    //de momento
    private SpriteRenderer _spriteRenderer;
    private Color _defaultColor;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = _spriteRenderer.color;
    }

    public void ChangeCharacter()
    {
        if(_spriteRenderer.color == _defaultColor)
        {
            _spriteRenderer.color = Color.cyan;
        }
        else
        {
            _spriteRenderer.color = _defaultColor;
        }
    }
}
