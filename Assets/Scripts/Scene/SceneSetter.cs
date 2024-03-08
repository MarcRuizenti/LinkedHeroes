using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSetter : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private List<GameObject> _enemiesList;
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SetUpScene;
    }

    private void SetUpScene(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1.0f;

        if (_player != null) _player.UpdateAnimator();

        if(_enemiesList.Any()) EnableEnemies();
    }

    private void EnableEnemies()
    {
        foreach(GameObject enemy in _enemiesList) 
        {
            enemy.SetActive(true);
        }
    }
}
