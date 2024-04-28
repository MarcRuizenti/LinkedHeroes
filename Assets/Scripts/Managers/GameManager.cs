using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject _player;
    public static GameManager Instance { get; private set; }

    public bool PlayerDeath;

    public int health;

    public enum Character
    {
        AIKE,
        KROKUR
    }

    public Character _currentCharacter;

    private void Awake()
    {
        health = 3;

        if(Instance == null)
        {
            Instance =  this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this);
            Destroy(this.gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _player.GetComponent<PlayerController>().UpdateAnimator();
        //SceneManager.sceneLoaded += GetReferences;
    }

    //void GetReferences(Scene scene, LoadSceneMode mode)
    //{
    //    if (_player == null)
    //    {
    //        _player = GameObject.FindGameObjectWithTag("Player");
    //    }

    //    if(TextWin == null)
    //    {
    //        Canvas canvas = (Canvas)FindObjectOfType(typeof(Canvas));

    //        TextWin = canvas.transform.Find("GameWin").gameObject;
    //    }
    //}
    
    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Respawn();
            }
        }

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += GetPlayer;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= GetPlayer;
    }

    private void GetPlayer(Scene arg0, LoadSceneMode arg1)
    {
        _player = FindObjectOfType<PlayerController>().gameObject;
        health = 3;
    }

    public void UpdatePlayerState()
    {
      PlayerDeath = true;
    }

    private void Respawn()
    {
        PlayerDeath = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeCharacter()
    {
        switch (_currentCharacter)
        {
            case Character.KROKUR:
                _currentCharacter = Character.AIKE;
                break;
            case Character.AIKE:
                _currentCharacter = Character.KROKUR;
                break;
            default:
                break;
        }

        _player.GetComponent<PlayerController>().UpdateAnimator();
    }

    
}
