using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ObtainGem : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    private CollectionSound _sound;
    private void Start()
    {
        _sound = GetComponent<CollectionSound>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(_particleSystem, transform.position, transform.rotation);

        this.gameObject.SetActive(false);
        if (gameObject.name == "collAike")
        {
            _sound.PlayAikeCollection(1, 0.3f);
        }
        else
        {
            _sound.PlayKrokurCollection(1, 0.3f);
        }
    }
}
