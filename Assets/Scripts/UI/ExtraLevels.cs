using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraLevels : MonoBehaviour
{
    [SerializeField] private string[] _gems;


    void Start()
    {
        int numberOfGems = 0;

        foreach(string gem in _gems)
        {
            if(PlayerPrefs.GetInt(gem) == 1)
            {
                numberOfGems++;
            }
        }

        if(numberOfGems >= 3)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }

    
}
