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
    [SerializeField] private AudioClip changeSound;
    private AudioSource audioSource;
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SetUpScene;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void SetUpScene(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1.0f;

        if (_player == null) return;

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

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SetUpScene;
    }

    public void ChangeCharacter()
    {
        if(Time.deltaTime <= 0) { return; }

        audioSource.PlayOneShot(changeSound);
        audioSource.pitch = 1;
        audioSource.volume = 0.2f;
        GameManager.Instance.ChangeCharacter();
    }
}
