using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _retryButton;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_retryButton);
    }
}
