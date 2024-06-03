using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ObtainGem : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    private CollectionSound _sound;
    [SerializeField] private string _prefName;
    public bool IsBlueGem;

    private void Start()
    {
        _sound = GetComponent<CollectionSound>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (IsBlueGem)
        {
            if(GameManager.Instance._currentCharacter == GameManager.Character.AIKE)
            {
                _sound.PlayAikeCollection(1, 0.3f);
                PlayerPrefs.SetInt(_prefName, 1);
                Instantiate(_particleSystem, transform.position, transform.rotation);

                gameObject.SetActive(false);
            }
            
        }
        else
        {
            if (GameManager.Instance._currentCharacter == GameManager.Character.KROKUR)
            {
                _sound.PlayKrokurCollection(1, 0.3f);
                PlayerPrefs.SetInt(_prefName, 1);
                Instantiate(_particleSystem, transform.position, transform.rotation);

                gameObject.SetActive(false);
            }
            
        }
    }
}
