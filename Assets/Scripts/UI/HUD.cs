using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite _aike;
    [SerializeField] private Sprite _krokur;
    [SerializeField] private Image _playerHead;

    [Header("Hearts")]
    [SerializeField] private Image[] _hearts;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Image img in _hearts)
        {
            img.sprite = _emptyHeart;
        }

        for(int i = 0; i < GameManager.Instance.health; i++)
        {
            _hearts[i].sprite = _fullHeart;
        }

        if(GameManager.Instance._currentCharacter == GameManager.Character.KROKUR) 
        {
            _playerHead.sprite = _krokur;
        }
        else
        {
            _playerHead.sprite = _aike;
        }
    }
}
