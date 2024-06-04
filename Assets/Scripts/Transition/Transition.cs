using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 5f;
    public int nextSceneLoad;

    public Animator blackTransition;

    public bool useBlackTransition;

    

    // Start is called before the first frame update
    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void LoadNextLevel()
    {
        
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

        if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", nextSceneLoad);
        }
        
    }

    public void LoadExtraLevels()
    {
        StartCoroutine(LoadLevel(10));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        if (useBlackTransition)
        {
            blackTransition.SetTrigger("Start");
        }
        else
        {
            transition.SetTrigger("Start");
        }

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    
}

