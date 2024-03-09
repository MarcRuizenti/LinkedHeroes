using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    //Evento al que nos suscribimos
    public GameEvent GameEvent;

    //Evento que lanzamos en respuesta
    public UnityEvent response;

    //Cuando el listener es activado se suscribe al evento
    private void OnEnable()
    {
        GameEvent.RegisterListener(this);
    }

    //Cuando el listener es desactivado se desuscribe
    private void OnDisable()
    {
        GameEvent.UnregisterListener(this);
    }

    //Cuando el evento suscrito ha sido lanzado invocamos la respuesta
    public void OnEventRaised()
    {
        response?.Invoke();
    }

    public void ChangeCharacter()
    {
        GameManager.Instance.ChangeCharacter();
    }
}
