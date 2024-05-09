using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject TextWin;

    public bool _isPaused;

    public bool bossDefeated;

    void Update()
    {

        //Fire2 = alt izquierdo
        if (Input.GetKeyDown(KeyCode.Escape) && !_isPaused)
        {
            PauseGame();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && _isPaused)
        {
            ResumeGame();
        }

        
    }

    public void Retry()
    {
        GameManager.Instance.Respawn();
    }

    private void PauseGame()
    {
        if(GameManager.Instance.PlayerDeath) { return; }

        if(bossDefeated) { return; }
        
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

    public void BossDefeated()
    {
        bossDefeated = true;
    }
}
