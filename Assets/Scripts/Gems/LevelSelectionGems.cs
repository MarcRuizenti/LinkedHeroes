using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionGems : MonoBehaviour
{
    [SerializeField] private string _prefName;


    // Start is called before the first frame update
    void Start()
    {
        int gemObtained = PlayerPrefs.GetInt(_prefName, 0);

        switch (gemObtained)
        {
            case 0:
                Lock(false);
                break;
            case 1:
                Unlock(false);
                break;
            default: break;
        }
    }

    public void Unlock(bool isCheat)
    {
        Color32 newColor = new Color32(255, 255, 255, 255);
        GetComponent<Image>().color = newColor;

        if (isCheat) PlayerPrefs.SetInt(_prefName, 1);
    }

    public void Lock(bool isCheat)
    {
        Color32 newColor = new Color32(255, 255, 255, 128);
        GetComponent<Image>().color = newColor;

        if(isCheat) PlayerPrefs.SetInt(_prefName, 0);
    }
}
