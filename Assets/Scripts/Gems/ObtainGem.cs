using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainGem : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(_particleSystem, transform.position, transform.rotation);

        this.gameObject.SetActive(false);

    }
}
