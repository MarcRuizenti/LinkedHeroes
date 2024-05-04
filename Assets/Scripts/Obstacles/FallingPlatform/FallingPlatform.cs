using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    [SerializeField] private float _fallDelay = 1f;
    [SerializeField] private float _destroyDelay = 8f;

    private Rigidbody2D _rb;
    private Animator _animator;
    private ParticleSystem _particle;
    private Vector3 _originalPositon;
    private float _respawnTime;
    

    private IEnumerator Fall()
    {
        _animator.SetBool("Off", true);
        _particle.Stop();
        yield return new WaitForSeconds(_fallDelay);
        _rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(_destroyDelay);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(Fall());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _originalPositon = transform.position;
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _particle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnDisable()
    {
        Invoke("Respawn", _respawnTime);
        _rb.bodyType = RigidbodyType2D.Static;
    }

    private void Respawn() 
    {
        transform.position = _originalPositon;
        gameObject.SetActive(true);
        _animator.SetBool("Off", false);
        _particle.Play();
    }

    
}
