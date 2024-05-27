using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectBoxCamera : MonoBehaviour
{
    public float _speed;
    private bool follow;

    private void Start()
    {
        follow = true;
    }
    private void Update()
    {
        Vector3 offset = new Vector3(0, 0, -10);
        if (follow)
        {
            transform.position = Vector3.Lerp(transform.position, GameManager.Instance._player.transform.position + offset, _speed * Time.deltaTime);
        }
    }

    public void SetFollow(bool f)
    {
        follow = f;
    }
}
