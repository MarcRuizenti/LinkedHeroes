using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject TextWin;

    private bool _isPaused;

    void Update()
    {
        //Fire2 = alt izquierdo
        if (Input.GetButtonDown("Fire2") && !_isPaused)
        {
            PauseGame();
        }
        else if(Input.GetButtonDown("Fire2") && _isPaused)
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
        _isPaused = true;
    }

    public void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void OnPlayerDeath()
    {
        GameManager.Instance.UpdatePlayerState();
    }

    public void Win()
    {
        TextWin.SetActive(true);
        Time.timeScale = 0f;
    }

    public void NextLevel(string nextLevel)
    {
        SceneManager.LoadScene(nextLevel);
    }
}
