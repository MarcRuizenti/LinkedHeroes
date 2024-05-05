using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject TargetIndicatorPrefab;
    public Camera Camera;
    public float Offset;
    public string TargetTag;

    private List<IndicatorScript> TargetIndicators = new List<IndicatorScript>();

    // Start is called before the first frame update
    void Start()
    {
        FindAllTargets();
    }

    private void FindAllTargets()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag(TargetTag);
        

        foreach(GameObject enemyObject in enemyObjects)
        {
            Debug.Log(enemyObject.name);
            AddTarget(enemyObject.transform);
        }
    }

    private void AddTarget(Transform targetTransform)
    {
        if (!HasTargetIndicator(targetTransform))
        {
            GameObject indicatorObj = Instantiate(TargetIndicatorPrefab, targetTransform);

            IndicatorScript targetIndicator = indicatorObj.GetComponent<IndicatorScript>();

            targetIndicator.Initialize(targetTransform, Camera, Offset);

            TargetIndicators.Add(targetIndicator);
        }
    }

    private void RemoveTarget(Transform targetTransform)
    {
        IndicatorScript targetIndicator = TargetIndicators.Find(indicator => indicator.Target == targetTransform);

        if(targetIndicator != null)
        {
            TargetIndicators.Remove(targetIndicator);
            Destroy(targetIndicator.gameObject);
        }
    }

    private bool HasTargetIndicator(Transform transform)
    {
        return TargetIndicators.Exists(indicator => indicator.Target == transform);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var indicator in TargetIndicators)
        {
            indicator.UpdateTargetPosition();
        }
    }
}
