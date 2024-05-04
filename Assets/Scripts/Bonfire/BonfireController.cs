using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BonfireController : MonoBehaviour
{
    private Collider2D _collider;
    private bool _playerInsideRadius;

    //Eventos necesarios para la fogata
    [SerializeField]private UnityEvent _onCharacterChange;
    [SerializeField]private UnityEvent _onBonfireEnter;
    [SerializeField] private UnityEvent _onBonfireExit;

    //particles
    [SerializeField] private ParticleSystem _particles;

    //X
    [SerializeField] private GameObject _UI;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (_playerInsideRadius)
        {
            _onBonfireEnter.Invoke();
        }
        else
        {
            _onBonfireExit.Invoke();
        }

        //si el jugador esta en el radio y pulsa la x
        if (_playerInsideRadius && Input.GetKeyDown(KeyCode.X))
        {
            _onCharacterChange.Invoke();
            _particles.Play();
        }   
    }

    //Detectamos si el player entra en el radio de la fogata
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            _playerInsideRadius = true;
            _UI.SetActive(true);
        }
        
    }

    //Detectamos si el player sale del radio de la fogata
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInsideRadius = false;
            _UI.SetActive(false);
        }
    }

    public void DeactivateAfterParticles()
    {
        var main = _particles.main;
        Invoke("Deactivate", main.duration - 0.4f);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
