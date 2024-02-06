using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Health;
    public bool PlayerDeath;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerDeath && Input.GetKeyUp(KeyCode.Escape)) {
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
        SceneManager.LoadScene("SampleScene");
    }
}
