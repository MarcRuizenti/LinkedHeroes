using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] LvlButtons;
    public GameObject[] LvlGems;

    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 2);

        for(int i = 0; i < LvlButtons.Length; i++)
        {
            if (i + 2 > levelAt) LvlButtons[i].interactable = false;
        }

        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) 
        {
            LvlButtons[0].Select();
            PlayerPrefs.DeleteKey("levelAt");
            PlayerPrefs.SetInt("BossDefeated", 0);
            int levelAt = PlayerPrefs.GetInt("levelAt", 2);
            for (int i = 0; i < LvlButtons.Length; i++)
            {
                if (i + 2 > levelAt) LvlButtons[i].interactable = false;
            }

            for (int i = 0; i < LvlGems.Length; i++)
            {
                LvlGems[i].GetComponent<LevelSelectionGems>().Lock(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            PlayerPrefs.SetInt("levelAt", 100);
            PlayerPrefs.SetInt("BossDefeated", 1);

            int levelAt = PlayerPrefs.GetInt("levelAt");

            for (int i = 0; i < LvlButtons.Length; i++)
            {
                LvlButtons[i].interactable = true;
            }

            for (int i = 0; i < LvlGems.Length; i++)
            {
                LvlGems[i].GetComponent<LevelSelectionGems>().Unlock(true);
            }
        }
    }
}
