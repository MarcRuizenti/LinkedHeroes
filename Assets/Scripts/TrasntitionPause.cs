using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrasntitionPause : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    
    public void DeactivatePauseMenu()
    {
        _pauseMenu.GetComponent<SceneManagement>()._isPaused = true;
    }

    public void ActivatePauseMenu()
    {
        _pauseMenu.GetComponent<SceneManagement>()._isPaused = false;
    }
}
