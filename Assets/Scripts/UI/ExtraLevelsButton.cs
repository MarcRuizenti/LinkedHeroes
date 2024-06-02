using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraLevelsButton : MonoBehaviour
{

    private void Update()
    {
        int bossDefeated = PlayerPrefs.GetInt("BossDefeated", 0);

        if (bossDefeated != 1)
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }
    }


}
