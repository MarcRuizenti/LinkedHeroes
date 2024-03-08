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
        if(PlayerDeath)
        {
            Time.timeScale = 0f;
        }
        //UpdateCharacter(_currentCharacter);
        
        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Respawn();
        //}
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

    public void Win()
    {
        TextWin.SetActive(true);
        Time.timeScale = 0f;
    }
}
