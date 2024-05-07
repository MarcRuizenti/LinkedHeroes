using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgresBar : MonoBehaviour
{
    private Image image;

    [SerializeField] private Transform bandera;
    private Transform playerTransform;

    private float totalDistance; 

    private void Start()
    {
        image = GetComponent<Image>();

        playerTransform = GameManager.Instance._player.transform;

        totalDistance = Vector3.Distance(playerTransform.position, bandera.position);
    }
    private void Update()
    {
        image.fillAmount = 1 - (Vector3.Distance(playerTransform.position, bandera.position) / totalDistance);
    }
}
