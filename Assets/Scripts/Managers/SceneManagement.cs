using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _player;

    private bool _isPaused;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Fire2 = alt izquierdo
        if (Input.GetButtonDown("Fire2") && !_isPaused)
        {
            PauseGame();
        }else if(Input.GetButtonDown("Fire2") && _isPaused)
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        _pauseMenu.SetActive(true);
        _pauseMenu.transform.position = _player.transform.position;
        Time.timeScale = 0;
        _isPaused = true;
    }

    private void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }
}
