using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    //Lista listeners suscrita a un evento
   private List<GameEventListener> listeners = new List<GameEventListener>();

    //Añadimos a la lista los listeners
   public void RegisterListener(GameEventListener listener)
    {
        if(!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    //Quitamos listeners de la lista
    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }

    //Cuando llamemos al evento recorre la lista para avisarle al listener y ejecute la respuesta
    public void Raise()
    {
        foreach (GameEventListener listener in listeners)
        {
            listener.OnEventRaised();
        }
    }
}
