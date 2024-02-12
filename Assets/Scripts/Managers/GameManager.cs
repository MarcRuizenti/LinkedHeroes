using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    public static GameManager Instance { get; private set; }

    public int Health;
    public bool PlayerDeath;

    public GameObject TextWin;


    public enum Character
    {
        AIKE,
        KROKUR
    }

    public Character _currentCharacter;

    private void Awake()
    {
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
        _currentCharacter = Character.AIKE;
    }

    // Update is called once per frame
    void Update()
    {

        if(_player != null)
        {
            
        }

        if(PlayerDeath)
        {
            Time.timeScale = 0f;
        }
        //UpdateCharacter(_currentCharacter);
        if(Input.GetKeyUp(KeyCode.Escape)) {
            Respawn();
        }
    }

    public void UpdatePlayerState()
    {
        if(!PlayerDeath)
        {
            PlayerDeath = true;
        }
        else
        {
            PlayerDeath = false;
        }
    }

    private void Respawn()
    {
        Time.timeScale = 1.0f;
        PlayerDeath = false;
        SceneManager.LoadScene("SampleScene");
    }

    private void UpdateCharacter(Character _character)
    {
        switch(_character)
        {
            case Character.AIKE:
                _player.GetComponent<SpriteRenderer>().color = Color.green;
                break; 
            case Character.KROKUR:
                _player.GetComponent<SpriteRenderer>().color = Color.cyan;
                break;
        }
    }

    public void ChangeCharacter()
    {
        switch (_currentCharacter)
        {
            case Character.KROKUR:
                _player.GetComponent<SpriteRenderer>().color = Color.green;
                _currentCharacter = Character.AIKE;
                break;
            case Character.AIKE:
                _player.GetComponent<SpriteRenderer>().color = Color.cyan;
                _currentCharacter = Character.KROKUR;
                break;
            default:
                break;
        }
    }

    public void Win()
    {
        TextWin.SetActive(true);
        Time.timeScale = 0f;
    }
}