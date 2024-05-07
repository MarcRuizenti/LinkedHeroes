using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgreSlider : MonoBehaviour
{
    private Slider slider;

    [SerializeField] private Transform bandera;
    [SerializeField] private Transform GhostTransform;

    private float totalDistance;
    void Start()
    {
        slider = GetComponent<Slider>();
        totalDistance = Vector3.Distance(bandera.position, GhostTransform.position);
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = 1 - (Vector3.Distance(GhostTransform.position, bandera.position) / totalDistance);
    }
}
