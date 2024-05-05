using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shadows : MonoBehaviour
{
    public static Shadows Instance;
    public GameObject Shadow;
    public List<GameObject> Pool = new List<GameObject>();
    private float _chronometer;
    public float Speed;
    public Color Color;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetShadows()
    {
        foreach(GameObject shadow in Pool) 
        {
            if (!shadow.activeInHierarchy)
            {
                shadow.SetActive(true);
                shadow.transform.position = transform.position;
                shadow.transform.rotation = transform.rotation;
                shadow.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
                shadow.GetComponent<Solid>()._color = Color;
                return shadow;
            }
        }

        GameObject obj = Instantiate(Shadow, transform.position, transform.rotation) as GameObject;
        obj.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        obj.GetComponent<Solid>()._color = Color;
        Pool.Add(obj);
        return obj;
    }

    public GameObject ShadowTrail()
    {
        _chronometer += Speed * Time.deltaTime;
        if(_chronometer > 2) 
        {
            GameObject shadow = GetShadows();
            _chronometer = 0;
            return shadow;
        }
        else
        {
            return null;
        }
    }

}
