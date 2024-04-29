using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainGem : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D collision)
    {
        this.gameObject.SetActive(false);
    }
}
